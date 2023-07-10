using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRVS.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private ApplicationDbContext _db;
        private UserManager<IdentityUser> _userManager;

        public NotificationViewComponent(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var currentUserEmail = _userManager.GetUserName(HttpContext.User);
            var currentUser = _db.Users.FirstOrDefault(x=>x.Email == currentUserEmail);
            var notification = _db.Notifications.Where(x=>x.CurrentUser == currentUser!.UserId).OrderByDescending(x=>x.DAT).ToList();
            ViewBag.notificationCount = _db.Notifications.Where(x=>x.IsRead == false).Where(x => x.CurrentUser == currentUser!.UserId).Count();
            return View(notification);
        }
    }
}
