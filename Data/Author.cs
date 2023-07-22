namespace BlogApi.Data
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public Author()
        {
            this.Blogs = new List<Blog>();
        }
    }
}
