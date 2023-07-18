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
        public async Task<IActionResult> Profile(string? id)
        {
            if (id == null)
            {
                var user = await _userManager.GetUserAsync(User);
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user!.Id);
                return View(currentUser);
            }
            else
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                return View(currentUser);
            }
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
