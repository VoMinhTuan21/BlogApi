namespace BlogApi.Data
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public Category()
        {
            this.Blogs = new List<Blog>();
        }
    }
}
