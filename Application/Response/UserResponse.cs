namespace Application.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
