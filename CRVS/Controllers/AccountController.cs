using CRVS.EF;
using CRVS.Core.Models;
using CRVS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CRVS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Configuration

        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private IWebHostEnvironment _webHostEnvironment;

        public AccountController(UserManager<IdentityUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ApplicationDbContext context,
                                 IConfiguration configuration,
                                 JwtSettings jwtSettings,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _webHostEnvironment = webHostEnvironment;
        }


        #endregion

        #region Users

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
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
                string ImgName = FileUpload(model);
                User newUser = new User
                {
                    UserId = user.Id,
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    Phone = model.Phone,
                    Img = ImgName
                };
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    _context.Add(newUser);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Account");
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email!);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
                {
                    /*var token = GenerateToken(user);*/
                    var claims = new List<Claim>                 //  Claims represent pieces of information about the authenticated user
                    {
                        new Claim(ClaimTypes.Name, user.UserName!),
                    };
                    var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme); //  This identity represents the user's identity within the system
                    var principal = new ClaimsPrincipal(identity);                 // which encapsulates the user's identity and allows it to be associated with an authenticated request.
                    await HttpContext.SignInAsync("Identity.Application", principal);       // It creates an authentication cookie for the user with the specified authentication scheme ("Identity.Application") and the principal (user's identity).


                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid user or password");
            }
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return RedirectToAction("Index", "Home");
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
    }
}
