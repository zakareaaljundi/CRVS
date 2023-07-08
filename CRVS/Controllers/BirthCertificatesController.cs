using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CRVS.Controllers
{
    public class BirthCertificatesController : Controller
    {
        private readonly IBaseRepository<BirthCertificate> _certificatesRepository;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public BirthCertificatesController(IBaseRepository<BirthCertificate> certificatesRepository,
                                            UserManager<IdentityUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            ApplicationDbContext context)
        {
            _certificatesRepository = certificatesRepository;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
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
            bool canApprove = rolePermissions != null && rolePermissions.ApprovalPermission;

            ViewBag.CanAdd = canAdd;
            ViewBag.CanEdit = canEdit;
            ViewBag.CanRead = canRead;
            ViewBag.CanDelete = canDelete;
            ViewBag.canApprove = canApprove;

            var certificates = await _certificatesRepository.GetAllAsync();
            return View(certificates);
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
            var unCompletedCertificates = certificates.Where(x => x.Approval == false).Where(x=>x.IsDeleted == false);
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
            var userId = _userManager.GetUserName(HttpContext.User);
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == userId);
            ViewBag.Governorate = currentUser!.Governorate;
            ViewBag.Directorate = currentUser!.Directorate;
            ViewBag.Judiciary = currentUser!.Judiciary;
            ViewBag.District = currentUser!.District;
            ViewBag.Village = currentUser!.Village;
            ViewBag.FacilityType = currentUser!.FacilityType;
            ViewBag.HealthInstitution = currentUser!.HealthInstitution;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BirthCertificate birthCertificate)
        {
            if (ModelState.IsValid)
            {
                birthCertificate.Creator = _userManager.GetUserId(HttpContext.User);
                birthCertificate.FirstStage = true;
                await _certificatesRepository.AddAsync(birthCertificate);
                return RedirectToAction("Create");
            }
            return View(birthCertificate);
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
