namespace Security_API_Template.Data.DTOs
{
    public class UserTokenDto
    {
        public required string UserName { get; set; }

        public required string Token { get; set; }
        public required string Error { get; set; }


    }
}
