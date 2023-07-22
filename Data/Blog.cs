namespace BlogApi.Data
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumOfViews { get; set; }
        public string Content { get; set; }
        public int NumOfFavorite { get; set; }
        public string Status { get; set; }

        // Author
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

        //Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        // Images
        public ICollection<BlogImage> BlogImages { get; set; }

        // Comments
        public ICollection<Comment> Comments { get; set; }

        public Blog()
        {
            this.BlogImages = new List<BlogImage>();
            this.Comments = new List<Comment>();
        }
    }
}
