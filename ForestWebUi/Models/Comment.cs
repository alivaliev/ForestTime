namespace ForestWebUi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelete { get; set; }
        public virtual string UserId { get; set; }
        public User User { get; set; }
    }
}
