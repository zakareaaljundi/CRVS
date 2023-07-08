using CRVS.EF;
using CRVS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRVS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
                                 UserManager<IdentityUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {/*
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

            bool canAdd = rolePermissions != null && rolePermissions.ReadPermission;

            ViewBag.CanAdd = canAdd;*/
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}