using System.Text.RegularExpressions;

namespace ForestWebUi.Helpers
{
    public class SeoHelper
    {
        public string SeoUrlCreater(string url)
        {
            string result = "";

            string pattern = @"[a-zA-Z0-9]";

            Regex regex = new(pattern);

            result = result.ToLower().Replace(" ", "-");

            Regex.Replace(result, "[^a-z0-9]", "-");
            


            return result;
        }
    }
}
