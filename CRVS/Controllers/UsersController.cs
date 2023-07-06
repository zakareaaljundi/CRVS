using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CRVS.EF;
using Microsoft.EntityFrameworkCore;

namespace CRVS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBaseRepository<User> _userRepository;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;

        public UsersController(IBaseRepository<User> userRepository, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }
        public async Task<IActionResult> Profile()
        {
            var user = _userManager.GetUserName(HttpContext.User);
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user);
            return View(currentUser);
        }
    }
}
