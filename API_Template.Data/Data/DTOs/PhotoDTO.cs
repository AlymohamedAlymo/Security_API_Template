using Security_API_Template.Data.Entites;

namespace API_Template.Data.Data.DTOs
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsMain { get; set; }

    }
}