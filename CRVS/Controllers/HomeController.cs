using CRVS.EF;
using CRVS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index()
        {
            var certificateData = _context.BirthCertificates.Select(c => new{CreationDate1 = c.CreationDate,}).GroupBy(c => c.CreationDate1)
                .Select(g => new{
                Date = g.Key,
                    Count = g.Count(),
                })
                .OrderBy(g => g.Date)
                .ToList();

            ViewBag.CertificateData = Newtonsoft.Json.JsonConvert.SerializeObject(certificateData);

            ViewBag.AllCerCount = _context.BirthCertificates.Count();
            ViewBag.PendingCerCount = _context.BirthCertificates.Where(x => x.BiostatisticsStage == true && x.Approval == false && x.IsRejected == false && x.IsDeleted == false).Count();
            ViewBag.ApprovedCerCount = _context.BirthCertificates.Where(x => x.BiostatisticsStage == true && x.Approval == true && x.IsRejected == false && x.IsDeleted == false).Count();
            ViewBag.RejectedCerCount = _context.BirthCertificates.Where(x => x.BiostatisticsStage == false && x.Approval == false && x.IsRejected == true && x.IsDeleted == false).Count();
            ViewBag.DeletedCerCount = _context.BirthCertificates.Where(x => x.IsDeleted == true).Count();

            double totalCerCount = (double)ViewBag.AllCerCount;
            ViewBag.PendingPercentage = (double)ViewBag.PendingCerCount / totalCerCount * 100;
            ViewBag.ApprovedPercentage = (double)ViewBag.ApprovedCerCount / totalCerCount * 100;
            ViewBag.RejectedPercentage = (double)ViewBag.RejectedCerCount / totalCerCount * 100;
            ViewBag.DeletedPercentage = (double)ViewBag.DeletedCerCount / totalCerCount * 100;

            var governorates = _context.Governorates.ToList();
            var certificateCounts = new List<int>();

            foreach (var gov in governorates)
            {
                var count = _context.BirthCertificates.Count(x => x.Governorate == gov.GovernorateName);
                certificateCounts.Add(count);
            }

            ViewBag.Governorates = governorates.Select(gov => gov.GovernorateName).ToList();
            ViewBag.CertificateCounts = certificateCounts;

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