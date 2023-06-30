using ForestWebUi.Data;
using ForestWebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForestWebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            try
            {
                tag.CreatedDate = DateTime.Now;
                tag.UpdatedDate = DateTime.Now;
                _context.Tags.Add(tag);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View("Create");
            }

        }
    }
}
