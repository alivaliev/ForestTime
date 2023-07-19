using ForestWebUi.Data;
using ForestWebUi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForestWebUi.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
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
            var articles = _context.Articles.Include(x => x.User).Include(z => z.ArticleTags).ThenInclude(y => y.Tag).Include(s => s.Category).ToList();
            return View(articles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var tagList = _context.Tags.ToList();
            var categoryList = _context.Categories.ToList();
            ViewBag.Tags = new SelectList(tagList, "Id", "TagName");
            ViewBag.Categories = new SelectList(categoryList, "Id", "CategoryName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article article, List<int> tagIds)
        {
            try
            {
                article.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                article.SeoUrl = "qwerty";
                article.CreatedDate = DateTime.Now;
                article.UpdatedDate = DateTime.Now;
                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                for (int i = 0; i < tagIds.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        TagId = tagIds[i],
                        ArticleId = article.Id
                    };
                    await _context.ArticlesTags.AddAsync(articleTag);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                return View(article);
            }

        }
        public IActionResult Edit(int id)
        {
            var article = _context.Articles.Include(x => x.ArticleTags).SingleOrDefault(a => a.Id == id);

            if (article == null || id == null)
            {
                return NotFound();
            }
            var tags = _context.Tags.ToList();
            ViewData["tagList"] = tags;
            var categories = _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            return View(article);
        }
        [HttpPost]
        public IActionResult Edit(Article article, List<int> tagIds)
        {
            article.UpdatedDate = DateTime.Now;
            article.UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            article.SeoUrl = "asas";
            _context.Articles.Update(article);
            _context.SaveChanges();
            var articleTag = _context.ArticlesTags.Where(a => a.ArticleId == article.Id).ToList();
            _context.ArticlesTags.RemoveRange(articleTag);
            List<ArticleTag> tagList = new();
            for (int i = 0; i < tagIds.Count; i++)
            {
                ArticleTag aTag = new()
                {
                    TagId = tagIds[i],
                    ArticleId = article.Id
                };
                tagList.Add(aTag);
            }
            _context.ArticlesTags.AddRange(tagList);
            _context.Articles.Update(article);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
