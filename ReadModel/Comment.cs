namespace BlogApi.ReadModel
{
    public class CommentRM
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
