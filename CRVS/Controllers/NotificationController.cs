using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using CRVS.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRVS.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IBaseRepository<Notification> _notificationsRepository;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public NotificationController(IBaseRepository<Notification> notificationsRepository,
                                            UserManager<IdentityUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            ApplicationDbContext context)
        {
            _notificationsRepository = notificationsRepository;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var notifications = _context.Notifications.Where(x=>x.CurrentUser == currentUser!.Id).ToList();
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
           
            }
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public IActionResult GetNotificationCount()
        {
            var currentUserEmail = _userManager.GetUserName(HttpContext.User);
            var currentUser = _context.Users.FirstOrDefault(x => x.Email == currentUserEmail);
            ViewBag.notificationCount = _context.Notifications.Count(x => x.IsRead == false && x.CurrentUser == currentUser.UserId);
             /*= _context.Notifications.Where(x => x.IsRead == false).Where(x => x.CurrentUser == currentUser!.UserId).Count();*/
            return Ok();
        }


    }
}
