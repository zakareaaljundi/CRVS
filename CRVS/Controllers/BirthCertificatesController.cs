using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using CRVS.Core.Models.ViewModels;
using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace CRVS.Controllers
{
    [Authorize]
    public class BirthCertificatesController : Controller
    {
        private readonly IBaseRepository<BirthCertificate> _certificatesRepository;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public BirthCertificatesController(IBaseRepository<BirthCertificate> certificatesRepository,
                                            UserManager<IdentityUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            ApplicationDbContext context,
                                            IWebHostEnvironment webHostEnvironment)
        {
            _certificatesRepository = certificatesRepository;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var currentRoles = await _userManager.GetRolesAsync(currentUser!);
            var roleId = "";
            if (currentRoles.Count > 0)
            {
                var roleName = currentRoles[0];
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role != null)
                {
                    roleId = role.Id;
                }
            }
            var controllerName = this.ControllerContext.RouteData.Values["controller"]!.ToString();

            var rolePermissions = await _context.RolePermissions.FirstOrDefaultAsync(p => p.RoleId! == roleId && p.TableName == controllerName);

            bool canAdd = rolePermissions != null && rolePermissions.AddPermission;
            bool canEdit = rolePermissions != null && rolePermissions.EditPermission;
            bool canRead = rolePermissions != null && rolePermissions.ReadPermission;
            bool canDelete = rolePermissions != null && rolePermissions.DeletePermission;
            bool canApprove = rolePermissions != null && rolePermissions.ApprovalPermission;

            ViewBag.CanAdd = canAdd;
            ViewBag.CanEdit = canEdit;
            ViewBag.CanRead = canRead;
            ViewBag.CanDelete = canDelete;
            ViewBag.canApprove = canApprove;

            bool IsAdmin = await _userManager.IsInRoleAsync(currentUser!, "Admin");
            if (IsAdmin)
            {
                var admin = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser.Id);
                var adminCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == admin!.HealthInstitution).ToList();
                return View(adminCertificates);

            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser.Id);
                var adminCertificates = _context.BirthCertificates.Where(x => x.Creator == user!.UserId).ToList();
                return View(adminCertificates);
            }/*
            var admin = _context.Users.FirstOrDefault(e => e.UserId == CurrentUser!.Id);
            var certificates = await _certificatesRepository.GetAllAsync();
            return View(certificates);*/
        }
        public async Task<IActionResult> SecondStage()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var pendingCertificates = certificates.Where(x => x.FirstStage == true).Where(x => x.SecondStage == false).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(pendingCertificates);
        }
        public async Task<IActionResult> ApprovalStage()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var pendingCertificates = certificates.Where(x => x.FirstStage == true).Where(x => x.SecondStage == true).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(pendingCertificates);
        }
        public async Task<IActionResult> Approved()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var approvedCertificates = certificates.Where(x => x.FirstStage == true).Where(x => x.SecondStage == true).Where(x => x.Approval == true).Where(x => x.IsDeleted == false);
            return View(approvedCertificates);
        }
        public async Task<IActionResult> UnCompleted()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var unCompletedCertificates = certificates.Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(unCompletedCertificates);
        }
        public async Task<IActionResult> Deleted()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var unCompletedCertificates = certificates.Where(x => x.IsDeleted == true);
            return View(unCompletedCertificates);
        }
        public async Task<IActionResult> Rejected()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var rejectedCertificates = certificates.Where(x => x.FirstStage == false).Where(x => x.SecondStage == false).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(rejectedCertificates);
        }
        public async Task<IActionResult> ToApprovalStage(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.SecondStage = true;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("SecondStage");
        }
        public async Task<IActionResult> Approve(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.Approval = true;
            _certificatesRepository.Update(certificate);
            var forNotification = _context.Notifications.FirstOrDefault(x => x.CertificateId == certificate.BirthCertificateId);
            if (forNotification != null)
            {
                var userForNotification = _context.Users.FirstOrDefault(x => x.UserId == certificate.Creator);
                Notification notification = new Notification
                {
                    HeadLine = "تم قبول الشهادة",
                    Description = "لقد تم قبول شهادة الميلاد, من قِبل مكتب التسجيل.",
                    CurrentUser = userForNotification!.UserId,
                    CertificateId = certificate.BirthCertificateId,
                    DAT = DateTime.Now,
                    IsGoodFeedBack = true
                };
                _context.Notifications.Add(notification);
                _context.SaveChanges();
            }
            return RedirectToAction("ApprovalStage");
        }
        public async Task<IActionResult> SecondStageReject(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.FirstStage = false;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("SecondStage");
        }
        public async Task<IActionResult> Reject(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.FirstStage = false;
            certificate.SecondStage = false;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("ApprovalStage");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.IsDeleted = true;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("ApprovalStage");
        }
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            _certificatesRepository.DeleteAsync(id);
            return RedirectToAction("Deleted");
        }
        public async Task<IActionResult> Recover(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.IsDeleted = false;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("Deleted");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.HId = Guid.NewGuid();
            var userId = _userManager.GetUserName(HttpContext.User);
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == userId);
            ViewBag.Governorate = currentUser!.Governorate;
            ViewBag.Doh = currentUser!.Doh;
            ViewBag.District = currentUser!.District;
            ViewBag.Nahia = currentUser!.Nahia;
            ViewBag.Village = currentUser!.Village;
            ViewBag.FacilityType = currentUser!.FacilityType;
            ViewBag.HealthInstitution = currentUser!.HealthInstitution;

            ViewBag.Jobs = new SelectList(_context.Jobs.ToList(), "JobId", "JobName");
            ViewBag.Religions = new SelectList(_context.Religions.ToList(), "ReligionId", "ReligionName");
            ViewBag.Nationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
            ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");

            ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
            ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
            ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");

            return View();
        }
        public ActionResult GetDistricts(int familyGovernorateId)
        {
            var filteredDistricts = _context.Districts
                .Where(d => d.GovernorateId == familyGovernorateId)
                .Select(d => new { districtId = d.DistrictId, districtName = d.DistrictName });
            return Json(filteredDistricts);
        }
        public ActionResult GetNahias(int familyDistrictId, int familyGovernorateId)
        {
            var filteredNahias = _context.Nahias
                .Where(d => d.DistrictId == familyDistrictId && d.GovernorateId == familyGovernorateId)
                .Select(d => new { nahiaId = d.NahiaId, nahiaName = d.NahiaName });

            return Json(filteredNahias);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BirthCertificateViewModel birthCertificateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                ViewBag.Jobs = new SelectList(_context.Jobs.ToList(), "JobId", "JobName");
                ViewBag.Religions = new SelectList(_context.Religions.ToList(), "ReligionId", "ReligionName");
                ViewBag.Nationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
                ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");
                var fatherJob = _context.Jobs.FirstOrDefault(x => x.JobId == birthCertificateViewModel.FatherJobId);
                var fatherReligion = _context.Religions.FirstOrDefault(x => x.ReligionId == birthCertificateViewModel.FatherReligionId);
                var fatherNationality = _context.Nationalities.FirstOrDefault(x => x.NationalityId == birthCertificateViewModel.FatherNationalityId);
                var motherJob = _context.Jobs.FirstOrDefault(x => x.JobId == birthCertificateViewModel.MotherJobId);
                var motherReligion = _context.Religions.FirstOrDefault(x => x.ReligionId == birthCertificateViewModel.MotherReligionId);
                var motherNationality = _context.Nationalities.FirstOrDefault(x => x.NationalityId == birthCertificateViewModel.MotherNationalityId);
                var DisableType = _context.Disabilities.FirstOrDefault(x => x.Id == birthCertificateViewModel.DisabledTypeId);

                ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
                ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
                ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");
                var governorate = _context.Governorates.FirstOrDefault(x => x.GovernorateId == birthCertificateViewModel.FamilyGovernorateId);
                var district = _context.Districts.FirstOrDefault(x => x.DistrictId == birthCertificateViewModel.FamilyDistrictId);
                var nahia = _context.Nahias.FirstOrDefault(x => x.NahiaId == birthCertificateViewModel.FamilyNahiaId);

                /*string ImgName = FileUpload(birthCertificateViewModel);*/
                string imgBirthCertificate = FileUpload(birthCertificateViewModel.ImageBirthCertificate!, birthCertificateViewModel.ImageBirthCertificate!.FileName);
                string imgMarriageCertificate = FileUpload(birthCertificateViewModel.ImageMarriageCertificate!, birthCertificateViewModel.ImageMarriageCertificate!.FileName);
                string imgFatherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!, birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!.FileName);
                string imgFatherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!, birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!.FileName);
                string imgMotherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!, birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!.FileName);
                string imgMotherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!, birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!.FileName);
                string imgResidencyCardFront = FileUpload(birthCertificateViewModel.ImageResidencyCardFront!, birthCertificateViewModel.ImageResidencyCardFront!.FileName);
                string imgResidencyCardBack = FileUpload(birthCertificateViewModel.ImageResidencyCardBack!, birthCertificateViewModel.ImageResidencyCardBack!.FileName);

                BirthCertificate birthCertificate = new BirthCertificate
                {
                    BirthCertificateId = birthCertificateViewModel.BirthCertificateId,
                    HealthId = birthCertificateViewModel.HealthId,
                    ChildName = birthCertificateViewModel.ChildName,
                    Gender = (BirthCertificate.Genders)birthCertificateViewModel.Gender,
                    Governorate = birthCertificateViewModel.Governorate,
                    Doh = birthCertificateViewModel.Doh,
                    District = birthCertificateViewModel.District,
                    Nahia = birthCertificateViewModel.Nahia,
                    Village = birthCertificateViewModel.Village,
                    FacilityType = birthCertificateViewModel.FacilityType,
                    HealthInstitution = birthCertificateViewModel.HealthInstitution,
                    BirthType = (BirthCertificate.BirthTypes)birthCertificateViewModel.BirthType,
                    NumberOfBirth = (BirthCertificate.NumberOfBirths)birthCertificateViewModel.NumberOfBirth,
                    BirthHour = birthCertificateViewModel.BirthHour,
                    DOB = birthCertificateViewModel.DOB,
                    DOBText = birthCertificateViewModel.DOBText,
                    FatherFName = birthCertificateViewModel.FatherFName,
                    FatherMName = birthCertificateViewModel.FatherMName,
                    FatherLName = birthCertificateViewModel.FatherLName,
                    FatherDOB = birthCertificateViewModel.FatherDOB,
                    FatherAge = birthCertificateViewModel.FatherAge,
                    FatherJob = fatherJob!.JobName,
                    FatherReligion = fatherReligion!.ReligionName,
                    FatherNationality = fatherNationality!.NationalityName,
                    FatherMobile = birthCertificateViewModel.FatherMobile,
                    MotherFName = birthCertificateViewModel.MotherFName,
                    MotherMName = birthCertificateViewModel.MotherMName,
                    MotherLName = birthCertificateViewModel.MotherLName,
                    MotherDOB = birthCertificateViewModel.MotherDOB,
                    MotherAge = birthCertificateViewModel.MotherAge,
                    MotherJob = motherJob!.JobName,
                    MotherReligion = motherReligion!.ReligionName,
                    MotherNationality = motherNationality!.NationalityName,
                    MotherMobile = birthCertificateViewModel.MotherMobile,
                    Relative = (BirthCertificate.Relatives)birthCertificateViewModel.Relative,
                    Alive = birthCertificateViewModel.Alive,
                    BornAliveThenDied = birthCertificateViewModel.BornAliveThenDied,
                    StillBirth = birthCertificateViewModel.StillBirth,
                    BornDisable = birthCertificateViewModel.BornDisable,
                    NoAbortion = birthCertificateViewModel.NoAbortion,
                    IsDisabled = (BirthCertificate.IsDisableds)birthCertificateViewModel.IsDisabled,
                    DisabledType = DisableType!.QName,
                    DurationOfPregnancy = birthCertificateViewModel.DurationOfPregnancy,
                    BabyWeight = birthCertificateViewModel.BabyWeight,
                    PlaceOfBirth = birthCertificateViewModel.PlaceOfBirth,
                    BirthOccurredBy = (BirthCertificate.BirthOccurredBys)birthCertificateViewModel.BirthOccurredBy,
                    KabilaName = birthCertificateViewModel.KabilaName,
                    LicenseNo = birthCertificateViewModel.LicenseNo,
                    LicenseYear = birthCertificateViewModel.LicenseYear,
                    FamilyGovernorate = governorate!.GovernorateName,
                    FamilyDistrict = district!.DistrictName,
                    FamilyNahia = nahia!.NahiaName,
                    FamilyMahala = birthCertificateViewModel.FamilyMahala,
                    FamilyDOH = birthCertificateViewModel.FamilyDOH,
                    FamilySector = birthCertificateViewModel.FamilySector,
                    FamilyPHC = birthCertificateViewModel.FamilyPHC,
                    FamilyZigag = birthCertificateViewModel.FamilyZigag,
                    FamilyHomeNo = birthCertificateViewModel.FamilyHomeNo,
                    DocumentType = (BirthCertificate.DocumentTypes)birthCertificateViewModel.DocumentType,
                    RecordNumber = birthCertificateViewModel.RecordNumber,
                    PageNumber = birthCertificateViewModel.PageNumber,
                    CivilStatusDirectorate = birthCertificateViewModel.CivilStatusDirectorate,
                    GovernorateCivilStatusDirectorate = birthCertificateViewModel.GovernorateCivilStatusDirectorate,
                    NationalIdFor = (BirthCertificate.NationalIdFors)birthCertificateViewModel.NationalIdFor,
                    NationalId = birthCertificateViewModel.NationalId,
                    PassportNo = birthCertificateViewModel.PassportNo,
                    InformerName = birthCertificateViewModel.InformerName,
                    InformerJobTitle = birthCertificateViewModel.InformerJobTitle,
                    KinshipOfTheNewborn = birthCertificateViewModel.KinshipOfTheNewborn,
                    BirthPerformerName = birthCertificateViewModel.BirthPerformerName,
                    BirthPerformerWorkingAddress = birthCertificateViewModel.BirthPerformerWorkingAddress,
                    HospitalManagerName = birthCertificateViewModel.HospitalManagerName,
                    HospitalManagerSig = birthCertificateViewModel.HospitalManagerSig,
                    RationCard = birthCertificateViewModel.RationCard,
                    ImgBirthCertificate = imgBirthCertificate,
                    ImgMarriageCertificate = imgMarriageCertificate,
                    ImgFatherUnifiedNationalIdFront = imgFatherUnifiedNationalIdFront,
                    ImgFatherUnifiedNationalIdBack = imgFatherUnifiedNationalIdBack,
                    ImgMotherUnifiedNationalIdFront = imgMotherUnifiedNationalIdFront,
                    ImgMotherUnifiedNationalIdBack = imgMotherUnifiedNationalIdBack,
                    ImgResidencyCardFront = imgResidencyCardFront,
                    ImgResidencyCardBack = imgResidencyCardBack,
                    FirstStage = true,
                    CreationDate = DateTime.Now,
                    Creator = currentUser!.Id,
                };
                await _certificatesRepository.AddAsync(birthCertificate);
                Notification notification = new Notification
                {
                    HeadLine = "تمت العملية بنجاح",
                    Description = "لقد قمت بإضافة شهادة ميلاد جديدة, يرجى إنتظار الموافقة.",
                    CurrentUser = currentUser!.Id,
                    CertificateId = birthCertificate.BirthCertificateId,
                    DAT = DateTime.Now,
                    IsGoodFeedBack = true
                };
                _context.Add(notification);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }
            return View(birthCertificateViewModel);
        }/*
        public string FileUpload(BirthCertificateViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.ImageBirthCertificate!.FileName);
            string newImgName = model.BirthCertificateId + "_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.ImageBirthCertificate!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.ImageBirthCertificate.CopyTo(fileStream);
            };
            return "\\Images\\" + newImgName;
        }*/
        public string FileUpload(IFormFile file, string desiredFileName)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(desiredFileName);
            string newImgName = fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(desiredFileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return "\\Images\\" + newImgName;
        }

    }
    /*
    public IActionResult Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var currentRoles = await _userManager.GetRolesAsync(currentUser);
        var roleId = "";
        if (currentRoles.Count > 0)
        {
            var roleName = currentRoles[0];
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                roleId = role.Id;
            }
        }

        var controllerName = this.ControllerContext.RouteData.Values["controller"]!.ToString();

        var rolePermissions = await _context.RolePermissions.FirstOrDefaultAsync(p => p.RoleId! == roleId && p.TableName == controllerName);

        bool canAdd = rolePermissions != null && rolePermissions.AddPermission;
        bool canEdit = rolePermissions != null && rolePermissions.EditPermission;
        bool canRead = rolePermissions != null && rolePermissions.ReadPermission;
        bool canDelete = rolePermissions != null && rolePermissions.DeletePermission;
        bool canDelete = rolePermissions != null && rolePermissions.DeletePermission;

        ViewBag.CanAdd = canAdd;
        ViewBag.CanEdit = canEdit;
        ViewBag.CanRead = canRead;
        ViewBag.CanDelete = canDelete;
        ViewBag.CanDelete = canDelete;
        return View();
    }
        */

}
