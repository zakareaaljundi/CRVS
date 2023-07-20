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

            bool IsOfficeUser = await _userManager.IsInRoleAsync(currentUser!, "مكتب تسجيل المواليد والوفيات");
            if (IsOfficeUser)
            {
                var officeUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var officeUserCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == officeUser!.HealthInstitution && x.IsDeleted == false && x.IsRejected == false).ToList();
                return View(officeUserCertificates);

            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var userCertificates = _context.BirthCertificates.Where(x => x.Creator == user!.UserId).Where(x => x.IsDeleted == false).ToList();
                return View(userCertificates);
            }
        }
        public async Task<IActionResult> BiostatisticsStage()
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

            var certificates = await _certificatesRepository.GetAllAsync();
            var pendingCertificates = certificates.OrderByDescending(x => x.CreationDate).Where(x => x.BiostatisticsStage == false).Where(x => x.IsRejected == false).Where(x => x.Approval == false).Where(x => x.IsDeleted == false);
            return View(pendingCertificates);
        }

        public async Task<IActionResult> ApprovalStage()
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

            var officeUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
            var officeUserCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == officeUser!.HealthInstitution && x.BiostatisticsStage == true && x.Approval == false && x.IsDeleted == false && x.IsRejected == false).ToList();
            return View(officeUserCertificates);
        }
        public async Task<IActionResult> Approved()
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

            bool IsOfficeUser = await _userManager.IsInRoleAsync(currentUser!, "مكتب تسجيل المواليد والوفيات");
            if (IsOfficeUser)
            {
                var officeUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var officeUserCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == officeUser!.HealthInstitution && x.BiostatisticsStage == true && x.Approval == true && x.IsDeleted == false && x.IsRejected == false).ToList();
                return View(officeUserCertificates);
            }
            else
            {
                var certificates = await _certificatesRepository.GetAllAsync();
                var approvedCertificates = certificates.Where(x => x.BiostatisticsStage == true).Where(x => x.IsRejected == false).Where(x => x.Approval == true).Where(x => x.IsDeleted == false);
                return View(approvedCertificates);
            }
        }
        public async Task<IActionResult> Deleted()
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

            bool IsOfficeUser = await _userManager.IsInRoleAsync(currentUser!, "مكتب تسجيل المواليد والوفيات");
            if (IsOfficeUser)
            {
                var officeUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var officeUserCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == officeUser!.HealthInstitution && x.IsDeleted == true).ToList();
                return View(officeUserCertificates);
            }
            else
            {
                var certificates = await _certificatesRepository.GetAllAsync();
                var deletedCertificates = certificates.Where(x => x.IsDeleted == true);
                return View(deletedCertificates);
            }
        }
        public async Task<IActionResult> Rejected()
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

            bool IsBioUser = await _userManager.IsInRoleAsync(currentUser!, "شعبة الإحصاء");
            bool IsOfficeUser = await _userManager.IsInRoleAsync(currentUser!, "مكتب تسجيل المواليد والوفيات");
            ViewBag.BioUser = IsBioUser;
            if (IsOfficeUser)
            {
                var officeUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var officeUserCertificates = _context.BirthCertificates.Where(x => x.HealthInstitution == officeUser!.HealthInstitution && x.IsRejected == true && x.Approval == false && x.BiostatisticsStage == false && x.IsDeleted == false).ToList();
                return View(officeUserCertificates);
            }
            else if (IsBioUser)
            {
                var bioUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == currentUser!.Id);
                var bioUserCertificates = _context.BirthCertificates.Where(x => x.Creator == bioUser!.UserId && x.IsRejected == true && x.Approval == false && x.BiostatisticsStage == false && x.IsDeleted == true).ToList();
                return View(bioUserCertificates);
            }
            else
            {
                var certificates = await _certificatesRepository.GetAllAsync();
                var rejectedCertificates = certificates.Where(x => x.IsRejected == true).Where(x => x.Approval == false).Where(x => x.BiostatisticsStage == false).Where(x => x.IsDeleted == false);
                return View(rejectedCertificates);
            }
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
            string description = $"لقد تم قبول شهادة ميلاد {certificate.ChildName}, من قِبل مكتب التسجيل.";
                Notification notification = new Notification
                {
                    HeadLine = "تم قبول الشهادة",
                    Description = description,
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
        public async Task<IActionResult> Reject(int id, string feedback)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.IsRejected = true;
            certificate.BiostatisticsStage = false;
            certificate.Feedback = feedback;
            var userForNotification = _context.Users.FirstOrDefault(x => x.UserId == certificate.Creator);
            string description = $"لقد تم رفض شهادة ميلاد {certificate.ChildName}, من قِبل مكتب التسجيل.";
            Notification notification = new Notification
            {
                HeadLine = "تم رفض الشهادة",
                Description = description,
                CurrentUser = userForNotification!.UserId,
                CertificateId = certificate.BirthCertificateId,
                DAT = DateTime.Now,
                IsGoodFeedBack = false,
            };
            _context.Notifications.Add(notification);
            _certificatesRepository.Update(certificate);
            return RedirectToAction("ApprovalStage");
        }
        public async Task<IActionResult> Delete(int id, string returnUrl)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.IsDeleted = true;
            await _certificatesRepository.UpdateAsync(certificate);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _certificatesRepository.DeleteAsync(id);
            return RedirectToAction("Deleted");
        }
        public async Task<IActionResult> Recover(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            certificate.IsDeleted = false;
            await _certificatesRepository.UpdateAsync(certificate);
            return RedirectToAction("Deleted");
        }
        public async Task<IActionResult> PreCreate()
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

            ViewBag.FatherPhones = new SelectList(_context.BirthCertificates.ToList(), "BirthCertificateId", "FatherMobile");
            ViewBag.MotherPhones = new SelectList(_context.BirthCertificates.ToList(), "BirthCertificateId", "MotherMobile");
            var certificates = _certificatesRepository.GetAll();
            return View(certificates);
        }
        public IActionResult SecondCreate(int id)
        {
            var certificate = _certificatesRepository.GetById(id);
            return RedirectToAction("Create", new
            {
                oldInformations = true,
                oldFatherFName = certificate.FatherFName,
                oldFatherMName = certificate.FatherMName,
                oldFatherLName = certificate.FatherLName,
                oldFatherAge = certificate.FatherAge,
                oldFatherDOB = certificate.FatherDOB,
                oldFatherJob = certificate.FatherJob,
                oldFatherMobile = certificate.FatherMobile,
                oldFatherReligion = certificate.FatherReligion,
                oldFatherNationality = certificate.FatherNationality,
                oldMotherFName = certificate.MotherFName,
                oldMotherMName = certificate.MotherMName,
                oldMotherLName = certificate.MotherLName,
                oldMotherAge = certificate.MotherAge,
                oldMotherDOB = certificate.MotherDOB,
                oldMotherJob = certificate.MotherJob,
                oldMotherMobile = certificate.MotherMobile,
                oldMotherReligion = certificate.MotherReligion,
                oldMotherNationality = certificate.MotherNationality,
            });
        }
        [HttpGet]
        public async Task<IActionResult> Create(bool? oldInformations, string? oldFatherFName, string? oldFatherMName, string? oldFatherLName, string? oldFatherAge, DateTime? oldFatherDOB, string? oldFatherMobile, string? oldFatherJob, string? oldFatherReligion, string? oldFatherNationality
                                                                , string? oldMotherFName, string? oldMotherMName, string? oldMotherLName, string? oldMotherAge, DateTime? oldMotherDOB, string? oldMotherMobile, string? oldMotherJob, string? oldMotherReligion, string? oldMotherNationality)
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

            BirthCertificateViewModel birthCertificateViewModel = new BirthCertificateViewModel
            {
                FatherFName = oldFatherFName,
                FatherMName = oldFatherMName,
                FatherLName = oldFatherLName,
                FatherAge = oldFatherAge,
                FatherDOB = oldFatherDOB,
                FatherMobile = oldFatherMobile,
                MotherFName = oldMotherFName,
                MotherMName = oldMotherMName,
                MotherLName = oldMotherLName,
                MotherAge = oldMotherAge,
                MotherDOB = oldMotherDOB,
                MotherMobile = oldMotherMobile,
            };

            var isArabianGovernorate = _context.Governorates.FirstOrDefault(x => x.GovernorateName == currentUser.Governorate)!.IsArabian;
            if (isArabianGovernorate)
            {
                if (oldFatherJob != "" && oldInformations == true)
                {
                    var fatherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == oldFatherJob)!.JobId;
                    ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 != 0).ToList(), "JobId", "JobName", fatherJobId);
                }
                else
                {
                    ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 != 0).ToList(), "JobId", "JobName");
                }
                if (oldFatherReligion != "" && oldInformations == true)
                {
                    var fatherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == oldFatherReligion)!.ReligionId;
                    ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 != 0).ToList(), "ReligionId", "ReligionName", fatherReligionId);
                }
                else
                {
                    ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 != 0).ToList(), "ReligionId", "ReligionName");
                }
                if (oldMotherJob != "" && oldInformations == true)
                {
                    var motherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == oldMotherJob)!.JobId;
                    ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 == 0).ToList(), "JobId", "JobName", motherJobId);
                }
                else
                {
                    ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 == 0).ToList(), "JobId", "JobName");
                }
                if (oldMotherReligion != "" && oldInformations == true)
                {
                    var motherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == oldMotherReligion)!.ReligionId;
                    ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 == 0).ToList(), "ReligionId", "ReligionName", motherReligionId);
                }
                else
                {
                    ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 == 0).ToList(), "ReligionId", "ReligionName");
                }
            }
            else
            {
                ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
                ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
            }

            if (oldFatherNationality != "" && oldInformations == true)
            {
                var fatherNationalityId = _context.Nationalities.FirstOrDefault(x => x.NationalityName == oldFatherNationality)!.NationalityId;
                ViewBag.FatherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName", fatherNationalityId);
            }
            else
            {
                ViewBag.FatherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
            }
            if (oldMotherNationality != "" && oldInformations == true)
            {
                var motherNationalityId = _context.Nationalities.FirstOrDefault(x => x.NationalityName == oldMotherNationality)!.NationalityId;
                ViewBag.MotherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName", motherNationalityId);
            }
            else
            {
                ViewBag.MotherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
            }

            ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");

            ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
            ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
            ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");

            return View(birthCertificateViewModel);
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
        public async Task<IActionResult> Create(BirthCertificateViewModel birthCertificateViewModel, string SaveBtn)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                #region Handle-Father-Mother-info-and-Disablility

                ViewBag.Jobs = new SelectList(_context.Jobs.ToList(), "JobId", "JobName");
                ViewBag.Religions = new SelectList(_context.Religions.ToList(), "ReligionId", "ReligionName");
                ViewBag.Nationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
                ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");
                var disableTypeName = "غير مصاب";
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

                string imgBirthCertificatePath = null;
                if (birthCertificateViewModel.ImageBirthCertificate != null)
                {
                    string imgBirthCertificate = FileUpload(birthCertificateViewModel.ImageBirthCertificate!, birthCertificateViewModel.ImageBirthCertificate!.FileName);
                    imgBirthCertificatePath = imgBirthCertificate;
                }
                string imgMarriageCertificatePath = null;
                if (birthCertificateViewModel.ImageMarriageCertificate != null)
                {
                    string imgMarriageCertificate = FileUpload(birthCertificateViewModel.ImageMarriageCertificate!, birthCertificateViewModel.ImageMarriageCertificate!.FileName);
                    imgMarriageCertificatePath = imgMarriageCertificate;
                }
                string imgFatherUnifiedNationalIdFrontPath = null;
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdFront != null)
                {
                    string imgFatherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!, birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!.FileName);
                    imgFatherUnifiedNationalIdFrontPath = imgFatherUnifiedNationalIdFront;
                }
                string imgFatherUnifiedNationalIdBackPath = null;
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdBack != null)
                {
                    string imgFatherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!, birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!.FileName);
                    imgFatherUnifiedNationalIdBackPath = imgFatherUnifiedNationalIdBack;
                }
                string imgMotherUnifiedNationalIdFrontPath = null;
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdFront != null)
                {
                    string imgMotherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!, birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!.FileName);
                    imgMotherUnifiedNationalIdFrontPath = imgMotherUnifiedNationalIdFront;
                }
                string imgMotherUnifiedNationalIdBackPath = null;
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdBack != null)
                {
                    string imgMotherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!, birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!.FileName);
                    imgMotherUnifiedNationalIdBackPath = imgMotherUnifiedNationalIdBack;
                }
                string imgResidencyCardFrontPath = null;
                if (birthCertificateViewModel.ImageResidencyCardFront != null)
                {
                    string imgResidencyCardFront = FileUpload(birthCertificateViewModel.ImageResidencyCardFront!, birthCertificateViewModel.ImageResidencyCardFront!.FileName);
                    imgResidencyCardFrontPath = imgResidencyCardFront;
                }
                string imgResidencyCardBackPath = null;
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
                    BiostatisticsStage = true,
                    CreationDate = DateTime.Now,
                    Creator = currentUser!.Id,
                };
                if (SaveBtn == "Save")
                {
                    birthCertificate.BiostatisticsStage = false;
                }
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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var birthCertificate = await _certificatesRepository.GetByIdAsync(id);
            if (birthCertificate == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserName(HttpContext.User);
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == userId);

            #region Handle-Father-Mother-info-and-Disablility-and-Family-Place

            var isArabianGovernorate = _context.Governorates.FirstOrDefault(x => x.GovernorateName == currentUser!.Governorate)!.IsArabian;
            if (isArabianGovernorate)
            {
                ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 != 0).ToList(), "JobId", "JobName");
                ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 != 0).ToList(), "ReligionId", "ReligionName");
                ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 == 0).ToList(), "JobId", "JobName");
                ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 == 0).ToList(), "ReligionId", "ReligionName");
                if (birthCertificate.FatherJob != "")
                {
                    var fatherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == birthCertificate.FatherJob)!.JobId;
                    ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 != 0).ToList(), "JobId", "JobName", fatherJobId);
                }
                if (birthCertificate.FatherReligion != "")
                {
                    var fatherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == birthCertificate.FatherReligion)!.ReligionId;
                    ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 != 0).ToList(), "ReligionId", "ReligionName", fatherReligionId);
                }
                if (birthCertificate.MotherJob != "")
                {
                    var motherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == birthCertificate.MotherJob)!.JobId;
                    ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic && x.JobId % 2 == 0).ToList(), "JobId", "JobName", motherJobId);
                }
                if (birthCertificate.MotherReligion != "")
                {
                    var motherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == birthCertificate.MotherReligion)!.ReligionId;
                    ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic && x.ReligionId % 2 == 0).ToList(), "ReligionId", "ReligionName", motherReligionId);
                }
            }
            else
            {
                ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
                ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName");
                ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName");
                if (birthCertificate.FatherJob != "")
                {
                    var fatherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == birthCertificate.FatherJob)!.JobId;
                    ViewBag.MaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName", fatherJobId);
                }
                if (birthCertificate.FatherReligion != "")
                {
                    var fatherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == birthCertificate.FatherReligion)!.ReligionId;
                    ViewBag.MaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName", fatherReligionId);
                }
                if (birthCertificate.MotherJob != "")
                {
                    var motherJobId = _context.Jobs.FirstOrDefault(x => x.JobName == birthCertificate.MotherJob)!.JobId;
                    ViewBag.FemaleJobs = new SelectList(_context.Jobs.Where(x => x.IsArabic == false).ToList(), "JobId", "JobName", motherJobId);
                }
                if (birthCertificate.FatherReligion != "")
                {
                    var motherReligionId = _context.Religions.FirstOrDefault(x => x.ReligionName == birthCertificate.FatherReligion)!.ReligionId;
                    ViewBag.FemaleReligions = new SelectList(_context.Religions.Where(x => x.IsArabic == false).ToList(), "ReligionId", "ReligionName", motherReligionId);
                }
            }

            ViewBag.FatherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
            ViewBag.MotherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
            if (birthCertificate.FatherNationality != "")
            {
                var fatherNationalityId = _context.Nationalities.FirstOrDefault(x => x.NationalityName == birthCertificate.FatherNationality)!.NationalityId;
                ViewBag.FatherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName", fatherNationalityId);
            }
            if (birthCertificate.MotherNationality != "")
            {
                var motherNationalityId = _context.Nationalities.FirstOrDefault(x => x.NationalityName == birthCertificate.MotherNationality)!.NationalityId;
                ViewBag.MotherNationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName", motherNationalityId);
            }

            ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");
            if (birthCertificate.DisabledType != "غير مصاب")
            {
                var disabledTypeId = _context.Disabilities.FirstOrDefault(x => x.QName == birthCertificate.DisabledType)!.Id;
                ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName", disabledTypeId);
            }

            ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
            ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
            ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");
            if (birthCertificate.FamilyGovernorate != "")
            {
                var familyGovernorateId = _context.Governorates.FirstOrDefault(x => x.GovernorateName == birthCertificate.FamilyGovernorate)!.GovernorateId;
                ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName", familyGovernorateId);
            }
            if (birthCertificate.FamilyDistrict != "")
            {
                var familyDistrictId = _context.Districts.FirstOrDefault(x => x.DistrictName == birthCertificate.FamilyDistrict)!.DistrictId;
                ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName", familyDistrictId);
            }
            if (birthCertificate.FamilyNahia != "")
            {
                var familyNahiad = _context.Nahias.FirstOrDefault(x => x.NahiaName == birthCertificate.FamilyNahia)!.NahiaId;
                ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName", familyNahiad);
            }

            ViewBag.PlaceOfBirth = birthCertificate.PlaceOfBirth;
            #endregion

            ViewBag.imgBirthCertificate = birthCertificate.ImgBirthCertificate;
            ViewBag.imgMarriageCertificate = birthCertificate.ImgMarriageCertificate;
            ViewBag.imgFatherUnifiedNationalIdFront = birthCertificate.ImgFatherUnifiedNationalIdFront;
            ViewBag.imgFatherUnifiedNationalIdBack = birthCertificate.ImgFatherUnifiedNationalIdBack;
            ViewBag.imgMotherUnifiedNationalIdFront = birthCertificate.ImgMotherUnifiedNationalIdFront;
            ViewBag.imgMotherUnifiedNationalIdBack = birthCertificate.ImgMotherUnifiedNationalIdBack;
            ViewBag.imgResidencyCardFront = birthCertificate.ImgResidencyCardFront;
            ViewBag.imgResidencyCardBack = birthCertificate.ImgResidencyCardBack;

            #region Passing-Info-To-BirthCertificate

            BirthCertificateViewModel birthCertificateViewModel = new BirthCertificateViewModel
            {
                BirthCertificateId = birthCertificate.BirthCertificateId,
                ChildName = birthCertificate.ChildName,
                HealthId = birthCertificate.HealthId,
                Gender = (BirthCertificateViewModel.Genders)birthCertificate.Gender,
                /*     Governorate = birthCertificate.Governorate,
                     Doh = birthCertificate.Doh,
                     District = birthCertificate.District,
                     Nahia = birthCertificate.Nahia,
                     Village = birthCertificate.Village,
                     FacilityType = birthCertificate.FacilityType,
                     HealthInstitution = birthCertificate.HealthInstitution,*/
                BirthType = (BirthCertificateViewModel.BirthTypes)birthCertificate.BirthType,
                NumberOfBirth = (BirthCertificateViewModel.NumberOfBirths)birthCertificate.NumberOfBirth,
                BirthHour = birthCertificate.BirthHour,
                DOB = birthCertificate.DOB,
                DOBText = birthCertificate.DOBText,
                FatherFName = birthCertificate.FatherFName,
                FatherMName = birthCertificate.FatherMName,
                FatherLName = birthCertificate.FatherLName,
                FatherDOB = birthCertificate.FatherDOB,
                FatherAge = birthCertificate.FatherAge,
                /*
                                FatherJob = birthCertificate.FatherJob,
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
                RationCard = birthCertificate.RationCard,
            };
            #endregion

            return View(birthCertificateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BirthCertificateViewModel birthCertificateViewModel, string SaveBtn)
        {
            if (ModelState.IsValid)
            {
                #region Handle-Father-Mother-info-and-Disablility

                ViewBag.Jobs = new SelectList(_context.Jobs.ToList(), "JobId", "JobName");
                ViewBag.Religions = new SelectList(_context.Religions.ToList(), "ReligionId", "ReligionName");
                ViewBag.Nationalities = new SelectList(_context.Nationalities.ToList(), "NationalityId", "NationalityName");
                ViewBag.Disabilities = new SelectList(_context.Disabilities.ToList(), "Id", "QName");
                var disableTypeName = "غير مصاب";
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

                var birthCertificateOld = await _certificatesRepository.GetByIdAsync(birthCertificateViewModel.BirthCertificateId);
                string imgBirthCertificatePath = null;
                if (birthCertificateViewModel.ImageBirthCertificate != null)
                {
                    string imgBirthCertificate = FileUpload(birthCertificateViewModel.ImageBirthCertificate!, birthCertificateViewModel.ImageBirthCertificate!.FileName);
                    imgBirthCertificatePath = imgBirthCertificate;
                }
                else
                {
                    imgBirthCertificatePath = birthCertificateOld.ImgBirthCertificate!;

                }
                string imgMarriageCertificatePath = null;
                if (birthCertificateViewModel.ImageMarriageCertificate != null)
                {
                    string imgMarriageCertificate = FileUpload(birthCertificateViewModel.ImageMarriageCertificate!, birthCertificateViewModel.ImageMarriageCertificate!.FileName);
                    imgMarriageCertificatePath = imgMarriageCertificate;
                }
                else
                {
                    imgMarriageCertificatePath = birthCertificateOld.ImgMarriageCertificate!;

                }
                string imgFatherUnifiedNationalIdFrontPath = null;
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdFront != null)
                {
                    string imgFatherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!, birthCertificateViewModel.ImageFatherUnifiedNationalIdFront!.FileName);
                    imgFatherUnifiedNationalIdFrontPath = imgFatherUnifiedNationalIdFront;
                }
                else
                {
                    imgFatherUnifiedNationalIdFrontPath = birthCertificateOld.ImgFatherUnifiedNationalIdFront!;

                }
                string imgFatherUnifiedNationalIdBackPath = null;
                if (birthCertificateViewModel.ImageFatherUnifiedNationalIdBack != null)
                {
                    string imgFatherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!, birthCertificateViewModel.ImageFatherUnifiedNationalIdBack!.FileName);
                    imgFatherUnifiedNationalIdBackPath = imgFatherUnifiedNationalIdBack;
                }
                else
                {
                    imgFatherUnifiedNationalIdBackPath = birthCertificateOld.ImgFatherUnifiedNationalIdBack!;

                }
                string imgMotherUnifiedNationalIdFrontPath = null;
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdFront != null)
                {
                    string imgMotherUnifiedNationalIdFront = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!, birthCertificateViewModel.ImageMotherUnifiedNationalIdFront!.FileName);
                    imgMotherUnifiedNationalIdFrontPath = imgMotherUnifiedNationalIdFront;
                }
                else
                {
                    imgMotherUnifiedNationalIdFrontPath = birthCertificateOld.ImgMotherUnifiedNationalIdFront!;

                }
                string imgMotherUnifiedNationalIdBackPath = null;
                if (birthCertificateViewModel.ImageMotherUnifiedNationalIdBack != null)
                {
                    string imgMotherUnifiedNationalIdBack = FileUpload(birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!, birthCertificateViewModel.ImageMotherUnifiedNationalIdBack!.FileName);
                    imgMotherUnifiedNationalIdBackPath = imgMotherUnifiedNationalIdBack;
                }
                else
                {
                    imgMotherUnifiedNationalIdBackPath = birthCertificateOld.ImgMotherUnifiedNationalIdBack!;

                }
                string imgResidencyCardFrontPath = null;
                if (birthCertificateViewModel.ImageResidencyCardFront != null)
                {
                    string imgResidencyCardFront = FileUpload(birthCertificateViewModel.ImageResidencyCardFront!, birthCertificateViewModel.ImageResidencyCardFront!.FileName);
                    imgResidencyCardFrontPath = imgResidencyCardFront;
                }
                else
                {
                    imgResidencyCardFrontPath = birthCertificateOld.ImgResidencyCardFront!;

                }
                string imgResidencyCardBackPath = null;
                if (birthCertificateViewModel.ImageResidencyCardBack != null)
                {
                    string imgResidencyCardBack = FileUpload(birthCertificateViewModel.ImageResidencyCardBack!, birthCertificateViewModel.ImageResidencyCardBack!.FileName);
                    imgResidencyCardBackPath = imgResidencyCardBack;
                }
                else
                {
                    imgResidencyCardBackPath = birthCertificateOld.ImgResidencyCardBack!;

                }
                #endregion

                #region Passing-Info-To-BirthCertificate

                /*birthCertificateOld = new BirthCertificate
                {*/
                birthCertificateOld.BirthCertificateId = birthCertificateViewModel.BirthCertificateId;
                birthCertificateOld.ChildName = birthCertificateViewModel.ChildName;
                birthCertificateOld.Gender = (BirthCertificate.Genders)birthCertificateViewModel.Gender;
                birthCertificateOld.BirthType = (BirthCertificate.BirthTypes)birthCertificateViewModel.BirthType;
                birthCertificateOld.NumberOfBirth = (BirthCertificate.NumberOfBirths)birthCertificateViewModel.NumberOfBirth;
                birthCertificateOld.BirthHour = birthCertificateViewModel.BirthHour;
                birthCertificateOld.DOB = birthCertificateViewModel.DOB;
                birthCertificateOld.DOBText = birthCertificateViewModel.DOBText;
                birthCertificateOld.FatherFName = birthCertificateViewModel.FatherFName;
                birthCertificateOld.FatherMName = birthCertificateViewModel.FatherMName;
                birthCertificateOld.FatherLName = birthCertificateViewModel.FatherLName;
                birthCertificateOld.FatherDOB = birthCertificateViewModel.FatherDOB;
                birthCertificateOld.FatherAge = birthCertificateViewModel.FatherAge;
                birthCertificateOld.FatherJob = fatherJobName;
                birthCertificateOld.FatherReligion = fatherReligionName;
                birthCertificateOld.FatherNationality = fatherNationalityName;
                birthCertificateOld.FatherMobile = birthCertificateViewModel.FatherMobile;
                birthCertificateOld.MotherFName = birthCertificateViewModel.MotherFName;
                birthCertificateOld.MotherMName = birthCertificateViewModel.MotherMName;
                birthCertificateOld.MotherLName = birthCertificateViewModel.MotherLName;
                birthCertificateOld.MotherDOB = birthCertificateViewModel.MotherDOB;
                birthCertificateOld.MotherAge = birthCertificateViewModel.MotherAge;
                birthCertificateOld.MotherJob = motherJobName;
                birthCertificateOld.MotherReligion = motherReligionName;
                birthCertificateOld.MotherNationality = motherNationalityName;
                birthCertificateOld.MotherMobile = birthCertificateViewModel.MotherMobile;
                birthCertificateOld.Relative = (BirthCertificate.Relatives)birthCertificateViewModel.Relative;
                birthCertificateOld.Alive = birthCertificateViewModel.Alive;
                birthCertificateOld.BornAliveThenDied = birthCertificateViewModel.BornAliveThenDied;
                birthCertificateOld.StillBirth = birthCertificateViewModel.StillBirth;
                birthCertificateOld.BornDisable = birthCertificateViewModel.BornDisable;
                birthCertificateOld.NoAbortion = birthCertificateViewModel.NoAbortion;
                birthCertificateOld.IsDisabled = (BirthCertificate.IsDisableds)birthCertificateViewModel.IsDisabled;
                birthCertificateOld.DisabledType = disableTypeName;
                birthCertificateOld.DurationOfPregnancy = birthCertificateViewModel.DurationOfPregnancy;
                birthCertificateOld.BabyWeight = birthCertificateViewModel.BabyWeight;
                birthCertificateOld.PlaceOfBirth = birthCertificateViewModel.PlaceOfBirth;
                birthCertificateOld.BirthOccurredBy = (BirthCertificate.BirthOccurredBys)birthCertificateViewModel.BirthOccurredBy;
                birthCertificateOld.LicenseNo = birthCertificateViewModel.LicenseNo;
                birthCertificateOld.LicenseYear = birthCertificateViewModel.LicenseYear;
                birthCertificateOld.FamilyGovernorate = governorateName;
                birthCertificateOld.FamilyDistrict = districtName;
                birthCertificateOld.FamilyNahia = nahiaName;
                birthCertificateOld.FamilyMahala = birthCertificateViewModel.FamilyMahala;
                birthCertificateOld.FamilyDOH = birthCertificateViewModel.FamilyDOH;
                birthCertificateOld.FamilySector = birthCertificateViewModel.FamilySector;
                birthCertificateOld.FamilyPHC = birthCertificateViewModel.FamilyPHC;
                birthCertificateOld.FamilyZigag = birthCertificateViewModel.FamilyZigag;
                birthCertificateOld.FamilyHomeNo = birthCertificateViewModel.FamilyHomeNo;
                birthCertificateOld.DocumentType = (BirthCertificate.DocumentTypes)birthCertificateViewModel.DocumentType;
                birthCertificateOld.RecordNumber = birthCertificateViewModel.RecordNumber;
                birthCertificateOld.PageNumber = birthCertificateViewModel.PageNumber;
                birthCertificateOld.CivilStatusDirectorate = birthCertificateViewModel.CivilStatusDirectorate;
                birthCertificateOld.GovernorateCivilStatusDirectorate = birthCertificateViewModel.GovernorateCivilStatusDirectorate;
                birthCertificateOld.NationalIdFor = (BirthCertificate.NationalIdFors)birthCertificateViewModel.NationalIdFor;
                birthCertificateOld.NationalId = birthCertificateViewModel.NationalId;
                birthCertificateOld.PassportNo = birthCertificateViewModel.PassportNo;
                birthCertificateOld.InformerName = birthCertificateViewModel.InformerName;
                birthCertificateOld.InformerJobTitle = birthCertificateViewModel.InformerJobTitle;
                birthCertificateOld.KinshipOfTheNewborn = birthCertificateViewModel.KinshipOfTheNewborn;
                birthCertificateOld.BirthPerformerName = birthCertificateViewModel.BirthPerformerName;
                birthCertificateOld.BirthPerformerWorkingAddress = birthCertificateViewModel.BirthPerformerWorkingAddress;
                birthCertificateOld.HospitalManagerName = birthCertificateViewModel.HospitalManagerName;
                birthCertificateOld.HospitalManagerSig = birthCertificateViewModel.HospitalManagerSig;
                birthCertificateOld.RationCard = birthCertificateViewModel.RationCard;
                birthCertificateOld.ImgBirthCertificate = imgBirthCertificatePath;
                birthCertificateOld.ImgMarriageCertificate = imgMarriageCertificatePath;
                birthCertificateOld.ImgFatherUnifiedNationalIdFront = imgFatherUnifiedNationalIdFrontPath;
                birthCertificateOld.ImgFatherUnifiedNationalIdBack = imgFatherUnifiedNationalIdBackPath;
                birthCertificateOld.ImgMotherUnifiedNationalIdFront = imgMotherUnifiedNationalIdFrontPath;
                birthCertificateOld.ImgMotherUnifiedNationalIdBack = imgMotherUnifiedNationalIdBackPath;
                birthCertificateOld.ImgResidencyCardFront = imgResidencyCardFrontPath;
                birthCertificateOld.ImgResidencyCardBack = imgResidencyCardBackPath;
                birthCertificateOld.BiostatisticsStage = true;
                birthCertificateOld.IsRejected = false;
                if (SaveBtn == "Save")
                {
                    birthCertificateOld.BiostatisticsStage = false;
                }
                #endregion

                _certificatesRepository.Update(birthCertificateOld);
                return RedirectToAction("BiostatisticsStage");
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
        public async Task<IActionResult> Details(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            return View(certificate);
        }
        public async Task<IActionResult> Print(int id)
        {
            var certificate = await _certificatesRepository.GetByIdAsync(id);
            return View(certificate);
        }
    }
}
