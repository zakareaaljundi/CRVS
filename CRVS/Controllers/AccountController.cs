using CRVS.EF;
using CRVS.Core.Models;
using CRVS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Data;


namespace CRVS.Controllers
{
    //[Authorize]

    public class AccountController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ApplicationDbContext context,
                                 IConfiguration configuration,
                                 JwtSettings jwtSettings,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _webHostEnvironment = webHostEnvironment;
        }


        #endregion

        #region Users

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var user = await _userManager.GetUserAsync(User);/*
            var currentRole = await _userManager.GetRolesAsync(user!);*/
            bool isAdmin = await _userManager.IsInRoleAsync(user!, "Admin");
            ViewBag.IsAdmin = !isAdmin;
            /*  ViewBag.userId = user!.Id;*/
            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            if (!isAdmin)
            {
                ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
                ViewBag.Dohs = new SelectList(_context.Dohs.ToList(), "DohId", "DohName");
                ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
                ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");
                ViewBag.FacilityTypes = new SelectList(_context.FacilityTypes.ToList(), "FacilityTypeId", "FacilityTypeName");
                ViewBag.HealthInstitutions = new SelectList(_context.HealthInstitutions.ToList(), "HealthInstitutionId", "HealthInstitutionName");
            }
            return View();
        }
        public ActionResult GetDohs(int governorateId)
        {
            var filteredDohs = _context.Dohs
                .Where(d => d.GovernorateId == governorateId)
                .Select(d => new { dohId = d.DohId, dohName = d.DohName });
            return Json(filteredDohs);
        }
        public ActionResult GetDistricts(int dohId)
        {
            var filteredDistricts = _context.Districts
                .Where(d => d.DohId == dohId)
                .Select(d => new { districtId = d.DistrictId, districtName = d.DistrictName });
            return Json(filteredDistricts);
        }
        public ActionResult GetNahias(int districtId, int dohId, int governorateId)
        {
            var filteredNahias = _context.Nahias
                .Where(d => d.DistrictId == districtId && d.DohId == dohId && d.GovernorateId == governorateId)
                .Select(d => new { nahiaId = d.NahiaId, nahiaName = d.NahiaName });

            return Json(filteredNahias);
        }

        public ActionResult GetHealthInstitutions(int facilityTypeId, int dohId, int governorateId)
        {
            var filteredHealthInstitutions = _context.HealthInstitutions
                .Where(d => d.FacilityTypeId == facilityTypeId && d.DohId == dohId && d.GovernorateId == governorateId)
                .Select(d => new { healthInstitutionId = d.HealthInstitutionId, healthInstitutionName = d.HealthInstitutionName });
            return Json(filteredHealthInstitutions);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                };
                var CurrentUser = await _userManager.GetUserAsync(User);
                bool IsAdmin = await _userManager.IsInRoleAsync(CurrentUser!, "Admin");

                var admin = _context.Users.FirstOrDefault(e => e.UserId == CurrentUser!.Id);

                var GovernorateName = "";
                var DohName = "";
                var DistrictName = "";
                var NahiaName = "";
                var VillageName = "";
                var FacilityTypeName = "";
                var HealthInstitutionName = "";
                if (IsAdmin)
                {
                    GovernorateName = admin!.Governorate;
                    DohName = admin.Doh;
                    DistrictName = admin.District;
                    NahiaName = admin.Nahia;
                    VillageName = admin.Village;
                    FacilityTypeName = admin.FacilityType;
                    HealthInstitutionName = admin.HealthInstitution;
                }
                else
                {
                    ViewBag.Governorates = new SelectList(_context.Governorates.ToList(), "GovernorateId", "GovernorateName");
                    ViewBag.Dohs = new SelectList(_context.Dohs.ToList(), "DohId", "DohName");
                    ViewBag.Districts = new SelectList(_context.Districts.ToList(), "DistrictId", "DistrictName");
                    ViewBag.Nahias = new SelectList(_context.Nahias.ToList(), "NahiaId", "NahiaName");
                    ViewBag.FacilityTypes = new SelectList(_context.FacilityTypes.ToList(), "FacilityTypeId", "FacilityTypeName");
                    ViewBag.HealthInstitutions = new SelectList(_context.HealthInstitutions.ToList(), "HealthInstitutionId", "HealthInstitutionName");

                    GovernorateName = _context.Governorates.Find(model.GovernorateId)!.GovernorateName;
                    DohName = _context.Dohs.Find(model.DohId)!.DohName;
                    DistrictName = _context.Districts.FirstOrDefault(x => x.DistrictId == model.DistrictId && x.DohId == model.DohId && x.GovernorateId == model.GovernorateId)!.DistrictName;
                    NahiaName = _context.Nahias.FirstOrDefault(x => x.NahiaId == model.NahiaId && x.DistrictId == model.DistrictId && x.DohId == model.DohId && x.GovernorateId == model.GovernorateId)!.NahiaName;
                    VillageName = model.Village;
                    FacilityTypeName = _context.FacilityTypes.Find(model.FacilityTypeId)!.FacilityTypeName;
                    HealthInstitutionName = _context.HealthInstitutions.FirstOrDefault(x => x.HealthInstitutionId == model.HealthInstitutionId && x.FacilityTypeId == model.FacilityTypeId && x.DohId == model.DohId && x.GovernorateId == model.GovernorateId)!.HealthInstitutionName;
                }
                string ImgName = FileUpload(model);
                User newUser = new User
                {
                    UserId = user.Id,
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    Phone = model.Phone,
                    Img = ImgName,
                    RegisterDate = DateTime.Now,
                    Governorate = GovernorateName,
                    Doh = DohName,
                    District = DistrictName,
                    Nahia = NahiaName,
                    Village = VillageName,
                    FacilityType = FacilityTypeName,
                    HealthInstitution = HealthInstitutionName
                };
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
                    var role = await _roleManager.FindByIdAsync(model.Roles!);
                    newUser.RoleName = role!.Name;
                    await _userManager.AddToRoleAsync(user, role!.Name!);
                    string description = $"لقد قمت بإضافة {model.FName} كموظف جديد";
                    Notification notification = new Notification
                    {
                        HeadLine = "تمت العملية بنجاح",
                        Description = description,
                        CurrentUser = CurrentUser!.Id,
                        NewUserId = user!.Id,
                        DAT = DateTime.Now,
                        IsGoodFeedBack = true
                    };
                    _context.Add(newUser);
                    _context.Add(notification);
                    _context.SaveChanges();
                    return RedirectToAction("Register", "Account");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        /*  public async Task<IActionResult> Login(LoginViewModel model)
          {
              if (ModelState.IsValid)
              {
                  var user = await _userManager.FindByEmailAsync(model.Email!);
                  if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
                  {
                      var roles = await _userManager.GetRolesAsync(user);
                      var claims = new List<Claim>        //  Claims represent pieces of information about the authenticated user
                      {
                          new Claim(ClaimTypes.Name, user.UserName!),
                      };
                      foreach (var role in roles)
                      {
                          claims.Add(new Claim(ClaimTypes.Role, role));   // Adding the current user's role as a claim to be recognized as a part of this role
                      }
                      var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);      //  This identity represents the user's identity within the system
                      var principal = new ClaimsPrincipal(identity);      // which encapsulates the user's identity and allows it to be associated with an authenticated request.

                      await HttpContext.SignInAsync("Identity.Application", principal);           // It creates an authentication cookie for the user with the specified authentication scheme ("Identity.Application") and the principal (user's identity).
                      return RedirectToAction("Index", "Home");
                  }
                  ModelState.AddModelError("", "Invalid user or password");
              }
              return BadRequest(ModelState);
          }*/

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid user or password");
                return View(model);
            }
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {/*
                var CurrentUser = await _userManager.GetUserAsync(User);*/
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return BadRequest();
                }
                var result = await _userManager.ChangePasswordAsync(user!, model.OldPassword!, model.NewPassword!);
                if (result.Succeeded)
                {
                    TempData["PassMessage"] = "Your Password Changed successfully.";
                    Notification notification = new Notification
                    {
                        HeadLine = "تمت العملية بنجاح",
                        Description = "لقد قمت بتحديث كلمة السر",
                        CurrentUser = user!.Id,
                        DAT = DateTime.Now,
                        IsGoodFeedBack = true
                    };
                    _context.Add(notification);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        public string FileUpload(RegisterViewModel model)
        {
            if (model.Img == null || model.Img.Length == 0)
            {
                return "/Images/GoldenBidPerson1_TestPicture.png";
            }
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
            string newImgName = "GoldenBid" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Img!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            };
            return "\\Images\\" + newImgName;
        }
        #endregion
        #region Roles

        /*This way should allow only users that in "Admin" Or "Registrar" role to access
        [Authorize(Roles = "Admin, Registrar")]*/

        /*This way should allow only users that in "Admin" And "Registrar" role to access 
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Registrar")]*/

        [HttpGet]
        [Authorize(Roles = "الادارة العليا")]
        public IActionResult CreateRole()
        {/*
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }*/
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "الادارة العليا")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "الادارة العليا")]
        public async Task<IActionResult> RolesList()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesWithUserCounts = new List<(IdentityRole Role, int UserCount)>();

            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
                rolesWithUserCounts.Add((Role: role, UserCount: usersInRole.Count));
            }

            return View(rolesWithUserCounts);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "الادارة العليا")]
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    model.Users!.Add(user.UserName!);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId!);
                if (role == null)
                {
                    return RedirectToAction(nameof(ErrorPage));

                }
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ModifyUsersInRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction(nameof(ErrorPage));
            }
            List<UserRoleViewModel> models = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                UserRoleViewModel userRole = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }
                models.Add(userRole);
            }
            return View(models);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyUsersInRole(string id, List<UserRoleViewModel> models)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction(nameof(ErrorPage));
            }
            IdentityResult result = new IdentityResult();
            for (int i = 0; i < models.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(models[i].UserId!);
                if (models[i].IsSelected && (!await _userManager.IsInRoleAsync(user!, role.Name!)))
                {
                    result = await _userManager.AddToRoleAsync(user!, role.Name!);
                }
                else if (!models[i].IsSelected && (await _userManager.IsInRoleAsync(user!, role.Name!)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user!, role.Name!);
                }
            }
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(RolesList));
            }
            return View(models);

        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                return View("Error");
            }

            var rolePermissions = await _context.RolePermissions.Where(p => p.RoleId == id).ToListAsync();
            _context.RolePermissions.RemoveRange(rolePermissions);
            await _context.SaveChangesAsync();

            return RedirectToAction("RolesList");
        }
        [HttpGet]
        [Authorize(Roles = "الادارة العليا")]
        public IActionResult Permissions(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }

            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }

            var classes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .Where(type => type.Name != "AccountController")
                .Where(type => type.Name != "NotificationController")
                .Where(type => type.Name != "HomeController")
                .Where(type => type.Name != "DashboardController")
                .Select(type => type.Name.Replace("Controller", ""))
                .ToList();

            var permissionsModel = new RolePermissionsViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = new Dictionary<string, bool>(),
                ReadPermissions = new Dictionary<string, bool>(),
                AddPermissions = new Dictionary<string, bool>(),
                EditPermissions = new Dictionary<string, bool>(),
                DeletePermissions = new Dictionary<string, bool>(),
                ApprovalPermissions = new Dictionary<string, bool>()
            };

            foreach (var classType in classes)
            {
                var existingPermission = _context.RolePermissions.FirstOrDefault(p => p.RoleId == role.Id && p.TableName == classType);

                if (existingPermission != null)
                {
                    permissionsModel.Permissions[classType] = existingPermission.ReadPermission;
                    permissionsModel.AddPermissions[classType] = existingPermission.AddPermission;
                    permissionsModel.EditPermissions[classType] = existingPermission.EditPermission;
                    permissionsModel.DeletePermissions[classType] = existingPermission.DeletePermission;
                    permissionsModel.ApprovalPermissions[classType] = existingPermission.ApprovalPermission;
                }
                else
                {
                    permissionsModel.Permissions[classType] = false;
                    permissionsModel.AddPermissions[classType] = false;
                    permissionsModel.EditPermissions[classType] = false;
                    permissionsModel.DeletePermissions[classType] = false;
                    permissionsModel.ApprovalPermissions[classType] = false;
                }
            }

            return View(permissionsModel);
        }

        [HttpPost]
        [Authorize(Roles = "الادارة العليا")]
        public async Task<IActionResult> Permissions(RolePermissionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var role = await _roleManager.FindByIdAsync(model.RoleId!);
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }

            foreach (var tableName in model.Permissions!.Keys)
            {
                var existingPermission = await _context.RolePermissions.FirstOrDefaultAsync(p => p.RoleId == model.RoleId && p.TableName == tableName);

                if (existingPermission != null)
                {
                    existingPermission.ReadPermission = model.Permissions[tableName];
                    existingPermission.AddPermission = model.AddPermissions![tableName];
                    existingPermission.EditPermission = model.EditPermissions![tableName];
                    existingPermission.DeletePermission = model.DeletePermissions![tableName];
                    existingPermission.ApprovalPermission = model.ApprovalPermissions![tableName];

                    _context.RolePermissions.Update(existingPermission);
                }
                else
                {
                    var permission = new RolePermission
                    {
                        RoleId = model.RoleId,
                        TableName = tableName,
                        ReadPermission = model.Permissions[tableName],
                        AddPermission = model.AddPermissions![tableName],
                        EditPermission = model.EditPermissions![tableName],
                        DeletePermission = model.DeletePermissions![tableName],
                        ApprovalPermission = model.ApprovalPermissions![tableName]
                    };

                    _context.RolePermissions.Add(permission);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("RolesList");
        }
        #endregion
    }
}
