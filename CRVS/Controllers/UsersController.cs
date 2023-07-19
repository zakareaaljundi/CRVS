using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using CRVS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CRVS.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace CRVS.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IBaseRepository<User> _userRepository;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public UsersController(IBaseRepository<User> userRepository, UserManager<IdentityUser> userManager, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            bool IsAdmin = await _userManager.IsInRoleAsync(user!, "مكتب تسجيل المواليد والوفيات");
            if (IsAdmin)
            {
                var admin = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user!.Id);
                var users = await _userRepository.GetAllAsync();
                var unBlockedUsers = users.Where(x => x.IsBlocked == false && x.HealthInstitution == admin!.HealthInstitution);
                return View(unBlockedUsers);
            }
            else
            {
                var users = await _userRepository.GetAllAsync();
                var unBlockedUsers = users.Where(x => x.IsBlocked == false);
                return View(unBlockedUsers);
            }
        }
        public async Task<IActionResult> Blocked()
        {
            var users = await _userRepository.GetAllAsync();
            var unBlockedUsers = users.Where(x => x.IsBlocked == true);
            /*.Where(x=>x.IsDeleted == false);*/
            return View(unBlockedUsers);
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string? id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(User);
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user!.Id);
                if (currentUser!.RoleName == "مكتب تسجيل المواليد والوفيات")
                {
                    ViewBag.CerCount = _context.BirthCertificates.Where(x => x.HealthInstitution == currentUser.HealthInstitution).Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.HealthInstitution == currentUser.HealthInstitution && x.Approval == true).Count();
                }
                else if (currentUser.RoleName == "شعبة الاحصاء")
                {
                    ViewBag.CerCount = _context.BirthCertificates.Where(x => x.Creator == currentUser.UserId).Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.Creator == currentUser.UserId && x.Approval == true).Count();
                }
                else
                {
                    ViewBag.CerCount = _context.BirthCertificates.Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.Approval == true).Count();
                }

                UpdateProfileViewModel updateProfileViewModel = new UpdateProfileViewModel
                {
                    UserId = currentUser.UserId,
                    FName = currentUser.FName,
                    LName = currentUser.LName,
                    Email = currentUser.Email,
                    Phone = currentUser.Phone,
                    Governorate = currentUser.Governorate,
                    Doh = currentUser.Doh,
                    District = currentUser.District,
                    Nahia = currentUser.Nahia,
                    Village = currentUser.Village,
                    FacilityType = currentUser.FacilityType,
                    HealthInstitution = currentUser.HealthInstitution,
                    RoleName = currentUser.RoleName,
                    OldImg = currentUser.Img,
                };
                ViewBag.HisProfile = true;
                return View(updateProfileViewModel);
            }
            else
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (currentUser!.RoleName == "مكتب تسجيل المواليد والوفيات")
                {
                    ViewBag.CerCount = _context.BirthCertificates.Where(x => x.HealthInstitution == currentUser.HealthInstitution).Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.HealthInstitution == currentUser.HealthInstitution && x.Approval == true).Count();
                }
                else if (currentUser.RoleName == "شعبة الاحصاء")
                {
                    ViewBag.CerCount = _context.BirthCertificates.Where(x => x.Creator == currentUser.UserId).Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.Creator == currentUser.UserId && x.Approval == true).Count();
                }
                else
                {
                    ViewBag.CerCount = _context.BirthCertificates.Count();
                    ViewBag.AppCerCount = _context.BirthCertificates.Where(x => x.Approval == true).Count();
                }
                UpdateProfileViewModel updateProfileViewModel = new UpdateProfileViewModel
                {
                    UserId = currentUser.UserId,
                    FName = currentUser.FName,
                    LName = currentUser.LName,
                    Phone = currentUser.Phone,
                    Email = currentUser.Email,
                    Governorate = currentUser.Governorate,
                    Doh = currentUser.Doh,
                    District = currentUser.District,
                    Nahia = currentUser.Nahia,
                    Village = currentUser.Village,
                    FacilityType = currentUser.FacilityType,
                    HealthInstitution = currentUser.HealthInstitution,
                    RoleName = currentUser.RoleName,
                    OldImg = currentUser.Img,
                };
                bool isSuperAdmin = currentUser.RoleName == "الادارة العليا" ? true : false;
                ViewBag.HisProfile = !isSuperAdmin;
                return View(updateProfileViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UpdateProfileViewModel model, string? id)
        {
            if (ModelState.IsValid)
            {
                string imgName;
                if (model.Img == null)
                {
                    var userImg = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.UserId == id);
                    imgName = userImg!.Img!;
                }
                else
                {
                    imgName = FileUpload(model);
                }
                User user = new User
                {
                    UserId = id,
                    FName = model.FName,
                    LName = model.LName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Governorate = model.Governorate,
                    Doh = model.Doh,
                    District = model.District,
                    Nahia = model.Nahia,
                    Village = model.Village,
                    FacilityType = model.FacilityType,
                    HealthInstitution = model.HealthInstitution,
                    RoleName = model.RoleName,
                    Img = imgName,
                };
                var description = $"مرحبا {model.FName}, لقد قمت بتعديل بعض معلوماتك الشخصية.";
                Notification notification = new Notification
                {
                    HeadLine = "تمت العملية بنجاح",
                    Description = description,
                    NewUserId = id,
                    CurrentUser = id,
                    DAT = DateTime.Now,
                    IsGoodFeedBack = true
                };
                _context.Add(notification);
                await _userRepository.UpdateAsync(user);
                return RedirectToAction("Profile");
            }
            return View(model);
        }
        public string FileUpload(UpdateProfileViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "CRVS" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Img!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            };
            return "\\Images\\" + newImgName;
        }
        [HttpGet]
        public IActionResult ChangePass(string? id)
        {
            var user = _context.Users.Find(id);
            var fullName = user!.FName + user.LName;
            /*var userPass = await _userManager.FindByIdAsync(id!);*/
            ChangePassViewModel changePassViewModel = new ChangePassViewModel
            {
                UserId = id,
                UserName = fullName,
                /*OldPassword = userPass!.PasswordHash*/
            };
            return View(changePassViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var user = await _userManager.FindByIdAsync(model.UserId!);
                if (user == null)
                {
                    return BadRequest();
                }
                /*var result = await _userManager.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);*/
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword!);

                if (result.Succeeded)
                {
                    var description = $"لقد قمت بتحديث كلمة سر {model.UserName}";
                    Notification notification = new Notification
                    {
                        HeadLine = "تمت العملية بنجاح",
                        Description = description,
                        NewUserId = model.UserId,
                        CurrentUser = currentUser!.Id,
                        DAT = DateTime.Now,
                        IsGoodFeedBack = true
                    };
                    _context.Add(notification);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Block(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsBlocked = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UnBlock(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsBlocked = false;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Blocked");
        }
    }
}
