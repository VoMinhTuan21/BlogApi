namespace BlogApi.ReadModel
{
    public class BlogRM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthorRM Author { get; set; }
        public int NumOfViews { get; set; }
        public string Content { get; set; }
        public CategoryRM Category { get; set; }
        public int NumbOfavorite { get; set; }
        public List<BlogImageRM> Images { get; set; }
    }
}
