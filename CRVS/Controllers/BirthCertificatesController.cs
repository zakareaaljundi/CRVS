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
                var admin = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var adminCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == admin!.HealthInstitution).ToList();
                return View(adminCertificates);

            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var adminCertificates = _context.BirthCertificates.Where(x => x.Creator == user!.UserId).ToList();
                return View(adminCertificates);
            }
        }
        public async Task<IActionResult> ToEdit()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var pendingCertificates = certificates.Where(x => x.ToEdit == true).Where(x => x.BiostatisticsStage == false).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(pendingCertificates);
        }
        /*
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
                }*/
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
        /*public async Task<IActionResult> Rejected()
        {
            var certificates = await _certificatesRepository.GetAllAsync();
            var rejectedCertificates = certificates.Where(x => x.FirstStage == false).Where(x => x.SecondStage == false).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(rejectedCertificates);
        }*//*
        public async Task<IActionResult> ToApprovalStage(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.SecondStage = true;
            _certificatesRepository.Update(certificate);
            return RedirectToAction("SecondStage");
        }*/
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
        }/*
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
        }*/
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
        public async Task<IActionResult> PreCreate()
        {
            ViewBag.Phones = new SelectList(_context.BirthCertificates.ToList(), "BirthCertificateId", "FatherMobile");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PreCreate(FindFamilyViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Phones = new SelectList(_context.BirthCertificates.ToList(), "BirthCertificateId", "FatherMobile");
                var BC = _context.BirthCertificates.FirstOrDefault(x => x.BirthCertificateId == model.FatherPhoneId);
                return RedirectToAction("Create");
            }
            return View(model);
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

            var isArabianGovernorate = _context.Governorates.FirstOrDefault(x => x.GovernorateName == currentUser.Governorate)!.IsArabian;
            if (isArabianGovernorate)
            {
                ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 != 0).ToList(), "JobId", "JobName");
                ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 != 0).ToList(), "ReligionId", "ReligionName");
                ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 == 0).ToList(), "JobId", "JobName");
                ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 == 0).ToList(), "ReligionId", "ReligionName");
            }
            else
            {
                ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
                ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
            }

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

                #region Handle-Father-Mother-info-and-Disablility

                ViewBag.Jobs = new SelectList(_context.Jobs.ToList(), "JobId", "JobName");
                ViewBag.Religions = new SelectList(_context.Religions.ToList(), "ReligionId", "ReligionName");
                ViewBag.Nationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
                ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");
                var disableTypeName = "Not Disable";
                if (birthCertificateViewModel.DisabledTypeId != null)
                {
                    var DisableType = _context.Disabilities.FirstOrDefault(x => x.Id == birthCertificateViewModel.DisabledTypeId);
                    disableTypeName = DisableType!.QName;
                }
                var fatherJobName = "";
                if (birthCertificateViewModel.FatherJobId != null)
                {
                    var fatherJob = _context.Jobs.FirstOrDefault(x => x.JobId == birthCertificateViewModel.FatherJobId);
                    fatherJobName = fatherJob!.JobName;
                }
                var fatherReligionName = "";
                if (birthCertificateViewModel.FatherReligionId != null)
                {
                    var fatherReligion = _context.Religions.FirstOrDefault(x => x.ReligionId == birthCertificateViewModel.FatherReligionId);
                    fatherReligionName = fatherReligion!.ReligionName;
                }
                var fatherNationalityName = "";
                if (birthCertificateViewModel.FatherNationalityId != null)
                {
                    var fatherNationality = _context.Nationalities.FirstOrDefault(x => x.NationalityId == birthCertificateViewModel.FatherNationalityId);
                    fatherNationalityName = fatherNationality!.NationalityName;
                }
                var motherJobName = "";
                if (birthCertificateViewModel.MotherJobId != null)
                {
                    var motherJob = _context.Jobs.FirstOrDefault(x => x.JobId == birthCertificateViewModel.MotherJobId);
                    motherJobName = motherJob!.JobName;
                }
                var motherReligionName = "";
                if (birthCertificateViewModel.MotherReligionId != null)
                {
                    var motherReligion = _context.Religions.FirstOrDefault(x => x.ReligionId == birthCertificateViewModel.MotherReligionId);
                    motherReligionName = motherReligion!.ReligionName;
                }
                var motherNationalityName = "";
                if (birthCertificateViewModel.MotherNationalityId != null)
                {
                    var motherNationality = _context.Nationalities.FirstOrDefault(x => x.NationalityId == birthCertificateViewModel.MotherNationalityId);
                    motherNationalityName = motherNationality!.NationalityName;
                }

                ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
                ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
                ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");

                var governorateName = "";
                if (birthCertificateViewModel.FamilyGovernorateId != null)
                {
                    var governorate = _context.Governorates.FirstOrDefault(x => x.GovernorateId == birthCertificateViewModel.FamilyGovernorateId);
                    governorateName = governorate!.GovernorateName;
                }
                var districtName = "";
                if (birthCertificateViewModel.FamilyDistrictId != null)
                {
                    var district = _context.Districts.FirstOrDefault(x => x.DistrictId == birthCertificateViewModel.FamilyDistrictId);
                    districtName = district!.DistrictName;
                }
                var nahiaName = "";
                if (birthCertificateViewModel.FamilyNahiaId != null)
                {
                    var nahia = _context.Nahias.FirstOrDefault(x => x.NahiaId == birthCertificateViewModel.FamilyNahiaId);
                    nahiaName = nahia!.NahiaName;
                }
                #endregion

                #region Handle-Null-Images

                string imgBirthCertificatePath = "";
                if (birthCertificateViewModel.ImageBirthCertificate != null)
                {
                    string imgBirthCertificate = FileUpload(birthCertificateViewModel.ImageBirthCertificate!, birthCertificateViewModel.ImageBirthCertificate!.FileName);
                    imgBirthCertificatePath = imgBirthCertificate;
                }
                string imgMarriageCertificatePath = "";
                if (birthCertificateViewModel.ImageMarriageCertificate != null)
                {
                    string imgMarriageCertificate = FileUpload(birthCertificateViewModel.ImageMarriageCertificate!, birthCertificateViewModel.ImageMarriageCertificate!.FileName);
                    imgMarriageCertificatePath = imgMarriageCertificate;
                }
                string imgFatherUnifiedNationalIdFrontPath = "";
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdFront != null)
                {
                    string imgFatherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!, birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!.FileName);
                    imgFatherUnifiedNationalIdFrontPath = imgFatherUnifiedNationalIdFront;
                }
                string imgFatherUnifiedNationalIdBackPath = "";
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdBack != null)
                {
                    string imgFatherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!, birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!.FileName);
                    imgFatherUnifiedNationalIdBackPath = imgFatherUnifiedNationalIdBack;
                }
                string imgMotherUnifiedNationalIdFrontPath = "";
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdFront != null)
                {
                    string imgMotherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!, birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!.FileName);
                    imgMotherUnifiedNationalIdFrontPath = imgMotherUnifiedNationalIdFront;
                }
                string imgMotherUnifiedNationalIdBackPath = "";
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdBack != null)
                {
                    string imgMotherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!, birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!.FileName);
                    imgMotherUnifiedNationalIdBackPath = imgMotherUnifiedNationalIdBack;
                }
                string imgResidencyCardFrontPath = "";
                if (birthCertificateViewModel.ImageResidencyCardFront != null)
                {
                    string imgResidencyCardFront = FileUpload(birthCertificateViewModel.ImageResidencyCardFront!, birthCertificateViewModel.ImageResidencyCardFront!.FileName);
                    imgResidencyCardFrontPath = imgResidencyCardFront;
                }
                string imgResidencyCardBackPath = "";
                if (birthCertificateViewModel.ImageResidencyCardBack != null)
                {
                    string imgResidencyCardBack = FileUpload(birthCertificateViewModel.ImageResidencyCardBack!, birthCertificateViewModel.ImageResidencyCardBack!.FileName);
                    imgResidencyCardBackPath = imgResidencyCardBack;
                }
                #endregion

                #region Passing-Info-To-BirthCertificate

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
                    FatherJob = fatherJobName,
                    FatherReligion = fatherReligionName,
                    FatherNationality = fatherNationalityName,
                    FatherMobile = birthCertificateViewModel.FatherMobile,
                    MotherFName = birthCertificateViewModel.MotherFName,
                    MotherMName = birthCertificateViewModel.MotherMName,
                    MotherLName = birthCertificateViewModel.MotherLName,
                    MotherDOB = birthCertificateViewModel.MotherDOB,
                    MotherAge = birthCertificateViewModel.MotherAge,
                    MotherJob = motherJobName,
                    MotherReligion = motherReligionName,
                    MotherNationality = motherNationalityName,
                    MotherMobile = birthCertificateViewModel.MotherMobile,
                    Relative = (BirthCertificate.Relatives)birthCertificateViewModel.Relative,
                    Alive = birthCertificateViewModel.Alive,
                    BornAliveThenDied = birthCertificateViewModel.BornAliveThenDied,
                    StillBirth = birthCertificateViewModel.StillBirth,
                    BornDisable = birthCertificateViewModel.BornDisable,
                    NoAbortion = birthCertificateViewModel.NoAbortion,
                    IsDisabled = (BirthCertificate.IsDisableds)birthCertificateViewModel.IsDisabled,
                    DisabledType = disableTypeName,
                    DurationOfPregnancy = birthCertificateViewModel.DurationOfPregnancy,
                    BabyWeight = birthCertificateViewModel.BabyWeight,
                    PlaceOfBirth = birthCertificateViewModel.PlaceOfBirth,
                    BirthOccurredBy = (BirthCertificate.BirthOccurredBys)birthCertificateViewModel.BirthOccurredBy,
                    LicenseNo = birthCertificateViewModel.LicenseNo,
                    LicenseYear = birthCertificateViewModel.LicenseYear,
                    FamilyGovernorate = governorateName,
                    FamilyDistrict = districtName,
                    FamilyNahia = nahiaName,
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
                    ImgBirthCertificate = imgBirthCertificatePath,
                    ImgMarriageCertificate = imgMarriageCertificatePath,
                    ImgFatherUnifiedNationalIdFront = imgFatherUnifiedNationalIdFrontPath,
                    ImgFatherUnifiedNationalIdBack = imgFatherUnifiedNationalIdBackPath,
                    ImgMotherUnifiedNationalIdFront = imgMotherUnifiedNationalIdFrontPath,
                    ImgMotherUnifiedNationalIdBack = imgMotherUnifiedNationalIdBackPath,
                    ImgResidencyCardFront = imgResidencyCardFrontPath,
                    ImgResidencyCardBack = imgResidencyCardBackPath,
                    ToEdit = true,
                    CreationDate = DateTime.Now,
                    Creator = currentUser!.Id,
                };
                #endregion

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
        }
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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var birthCertificate = await _certificatesRepository.GetByIdAsync(id);
            if (birthCertificate == null)
            {
                return NotFound();
            }
            #region Passing-Info-To-BirthCertificate
            
            BirthCertificateViewModel birthCertificateViewModel = new BirthCertificateViewModel
            {
                ChildName = birthCertificate.ChildName,
                Gender = (BirthCertificateViewModel.Genders)birthCertificate.Gender,
                BirthType = (BirthCertificateViewModel.BirthTypes)birthCertificate.BirthType,
                NumberOfBirth = (BirthCertificateViewModel.NumberOfBirths)birthCertificate.NumberOfBirth,
                BirthHour = birthCertificate.BirthHour,
                DOB = birthCertificate.DOB,
                DOBText = birthCertificate.DOBText,
                FatherFName = birthCertificate.FatherFName,
                FatherMName = birthCertificate.FatherMName,
                FatherLName = birthCertificate.FatherLName,
                FatherDOB = birthCertificate.FatherDOB,
                FatherAge = birthCertificate.FatherAge,/*
                FatherJobId = birthCertificate.FatherJob,
                FatherReligion = birthCertificate.FatherReligion,
                FatherNationality = birthCertificate.FatherNationality,*/
                FatherMobile = birthCertificate.FatherMobile,
                MotherFName = birthCertificate.MotherFName,
                MotherMName = birthCertificate.MotherMName,
                MotherLName = birthCertificate.MotherLName,
                MotherDOB = birthCertificate.MotherDOB,
                MotherAge = birthCertificate.MotherAge,
                /*
                MotherJob = birthCertificate.MotherJob,
                MotherReligion = birthCertificate.MotherReligion,
                MotherNationality = birthCertificate.MotherNationality,*/
                MotherMobile = birthCertificate.MotherMobile,
                Relative = (BirthCertificateViewModel.Relatives)birthCertificate.Relative,
                Alive = birthCertificate.Alive,
                BornAliveThenDied = birthCertificate.BornAliveThenDied,
                StillBirth = birthCertificate.StillBirth,
                BornDisable = birthCertificate.BornDisable,
                NoAbortion = birthCertificate.NoAbortion,
                IsDisabled = (BirthCertificateViewModel.IsDisableds)birthCertificate.IsDisabled,
                /*DisabledType = disableTypeName,*/
                DurationOfPregnancy = birthCertificate.DurationOfPregnancy,
                BabyWeight = birthCertificate.BabyWeight,
                PlaceOfBirth = birthCertificate.PlaceOfBirth,
                BirthOccurredBy = (BirthCertificateViewModel.BirthOccurredBys)birthCertificate.BirthOccurredBy,
                LicenseNo = birthCertificate.LicenseNo,
                LicenseYear = birthCertificate.LicenseYear,
                /*FamilyGovernorate = governorateName,
                FamilyDistrict = districtName,
                FamilyNahia = nahiaName,*/
                FamilyMahala = birthCertificate.FamilyMahala,
                FamilyDOH = birthCertificate.FamilyDOH,
                FamilySector = birthCertificate.FamilySector,
                FamilyPHC = birthCertificate.FamilyPHC,
                FamilyZigag = birthCertificate.FamilyZigag,
                FamilyHomeNo = birthCertificate.FamilyHomeNo,
                DocumentType = (BirthCertificateViewModel.DocumentTypes)birthCertificate.DocumentType,
                RecordNumber = birthCertificate.RecordNumber,
                PageNumber = birthCertificate.PageNumber,
                CivilStatusDirectorate = birthCertificate.CivilStatusDirectorate,
                GovernorateCivilStatusDirectorate = birthCertificate.GovernorateCivilStatusDirectorate,
                NationalIdFor = (BirthCertificateViewModel.NationalIdFors)birthCertificate.NationalIdFor,
                NationalId = birthCertificate.NationalId,
                PassportNo = birthCertificate.PassportNo,
                InformerName = birthCertificate.InformerName,
                InformerJobTitle = birthCertificate.InformerJobTitle,
                KinshipOfTheNewborn = birthCertificate.KinshipOfTheNewborn,
                BirthPerformerName = birthCertificate.BirthPerformerName,
                BirthPerformerWorkingAddress = birthCertificate.BirthPerformerWorkingAddress,
                HospitalManagerName = birthCertificate.HospitalManagerName,
                HospitalManagerSig = birthCertificate.HospitalManagerSig,
                RationCard = birthCertificate.RationCard,/*
                ImgBirthCertificate = imgBirthCertificatePath,
                ImgMarriageCertificate = imgMarriageCertificatePath,
                ImgFatherUnifiedNationalIdFront = imgFatherUnifiedNationalIdFrontPath,
                ImgFatherUnifiedNationalIdBack = imgFatherUnifiedNationalIdBackPath,
                ImgMotherUnifiedNationalIdFront = imgMotherUnifiedNationalIdFrontPath,
                ImgMotherUnifiedNationalIdBack = imgMotherUnifiedNationalIdBackPath,
                ImgResidencyCardFront = imgResidencyCardFrontPath,
                ImgResidencyCardBack = imgResidencyCardBackPath,*/
            };
            #endregion
            return View(birthCertificateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BirthCertificate birthCertificate)
        {
            if (ModelState.IsValid)
            {
                _certificatesRepository.Update(birthCertificate);
                return RedirectToAction("Index");
            }

            return View(birthCertificate);
        }
    }
}
