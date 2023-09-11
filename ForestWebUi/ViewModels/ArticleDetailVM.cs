using ForestWebUi.Models;

namespace ForestWebUi.ViewModels
{
    public class ArticleDetailVM
    {
        public Article Article { get; set; }
        public List<Category> CategoryCount { get; set; }
        public List<Article> RecentArticles { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Article> SimilarArticles { get; set; }


    }
}
