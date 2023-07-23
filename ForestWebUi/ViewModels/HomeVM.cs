using ForestWebUi.Models;

namespace ForestWebUi.ViewModels
{
    public class HomeVM
    {
        public List<Article> HomeArticles { get; set; }
        public List<Tag> HomeTags { get; set; }
        public List<Category> HomeCategories { get; set; }
        public List<Article> RecentArticles { get; set; }
        
    }
}

