namespace Blog.Client.WebSocket
{
    public  class CreatedPostDto
    {
        public int PostId { get; set; }
        public string Author { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
    }
}