using CRVS.Core.IRepositories;
using CRVS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRVS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBaseRepository<User> _userRepository;

        public UsersController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }
    }
}
