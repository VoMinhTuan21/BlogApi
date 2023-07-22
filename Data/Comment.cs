namespace BlogApi.Data
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        // Blog
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
