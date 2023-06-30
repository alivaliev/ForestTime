using Azure;
using ForestWebUi.Models;

namespace ForestWebUI.ViewModels
{
    public class HomeVM
    {
        public List<Article> HomeArticle { get; set; }
        public List<Tag> HomeTag { get; set; }
        public List<Category> HomeCategory { get; set; }
    }
}

