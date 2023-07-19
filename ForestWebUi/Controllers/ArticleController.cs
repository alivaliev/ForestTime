using ForestWebUi.Data;
using ForestWebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForestWebUi.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context; 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            try
            {
                var article = _context.Articles.Include(x=>x.User).Include(a=>a.Category).Include(y=>y.ArticleTags).ThenInclude(z=>z.Tag).SingleOrDefault(x => x.Id == id);
                var categories = _context.Categories.ToList();
                if (article == null)
                {
                    return NotFound();
                }
                DetailVM vm = new()
                {
                    Article = article
                    

                };
                return View(vm);
            }
            catch (Exception ex)
            {
                return NotFound();
                
            }
            
            
        }
    }
}
