using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRVS.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private object ControllerContext;

        public UserNameViewComponent(ApplicationDbContext db, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IViewComponentResult Invoke()
        {
            var currentUser = _userManager.GetUserName(HttpContext.User);
            var user = _db.Users.FirstOrDefault(x => x.Email == currentUser);
            if (user != null)
            {
                ViewData["FullName"] = user.FName +" "+ user.LName;
            }
            return View();
        }
    }
}
