using ForestWebUi.Data;
using ForestWebUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ForestWebUi.ViewModels;

namespace ForestWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles.Include(x=>x.User).ToList();
            var categories = _context.Categories.ToList();
            var recentArticles = _context.Articles.OrderByDescending(x => x.CreatedDate).Take(3).ToList();
            var featuredArticles = _context.Articles.OrderByDescending(x => x.Views).Take(3).ToList();
            var articlePhotos = _context.Articles.OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            
            

            HomeVM vm = new()
            {
                HomeArticles = articles,
                HomeCategories = categories,
                RecentArticles = recentArticles,
                FeaturedtArticles = featuredArticles,
                ArticlePhotos = articlePhotos,
            };
            return View(vm);
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