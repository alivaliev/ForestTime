﻿namespace ForestWebUi.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }

        public List<Article> Articles { get; set; }
    }
}
