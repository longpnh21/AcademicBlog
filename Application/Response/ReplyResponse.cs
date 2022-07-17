namespace Application.Response
{
    public class ReplyResponse
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public UserResponse User { get; set; }

    }
}
