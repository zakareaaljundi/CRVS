using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRVS.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;
        public UserNameViewComponent(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
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
