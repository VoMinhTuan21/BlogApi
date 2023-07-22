namespace BlogApi.Data
{
    public class BlogImage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Source { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
