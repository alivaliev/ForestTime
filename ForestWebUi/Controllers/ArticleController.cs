using ForestWebUi.Data;
using ForestWebUi.Models;
using ForestWebUi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForestWebUi.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComment(Comment articleComment, int articleId)
        {
            try
            {
                articleComment.ArticleId = articleId;
                articleComment.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                articleComment.CreatedDate = DateTime.Now;

                _context.Comments.Add(articleComment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }
        }
        public IActionResult Detail(int id)
        {
            try
            {
                var article = _context.Articles.Include(x => x.User).Include(a => a.Category).Include(y => y.ArticleTags).ThenInclude(z => z.Tag).SingleOrDefault(x => x.Id == id);
                var categories = _context.Categories.Include(x => x.Articles).ToList();
                var recentArticles = _context.Articles.OrderByDescending(x => x.CreatedDate).Take(3).ToList();
                var random = new Random();
                var randomArticles = _context.Articles.OrderByDescending(x => random).Take(2).ToList();

                if (article == null)
                {
                    return NotFound();
                }
                var articleComments = _context.Comments.Include(x => x.User).Where(x => x.ArticleId == id).ToList();
                ArticleDetailVM vm = new()
                {
                    Article = article,
                    CategoryCount = categories,
                    RecentArticles = recentArticles,
                    Comments = articleComments,
                    RandomArticles = randomArticles,


                };
                article.Views += 1;
                _context.Articles.Update(article);
                _context.SaveChanges();
                return View(vm);
            }
            catch (Exception ex)
            {
                return NotFound();

            }


        }
    }
}
