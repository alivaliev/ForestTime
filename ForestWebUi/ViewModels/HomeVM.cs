using Azure;
using ForestWebUi.Models;

namespace ForestWebUI.ViewModels
{
    public class HomeVM
    {
        public List<Article> HomeArticles { get; set; }
        public List<Tag> HomeTags { get; set; }
        public List<Category> HomeCategories { get; set; }
    }
}

