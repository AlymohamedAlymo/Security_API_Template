using Security_API_Template.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Template.Data.Data.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int Age { get; set; }
        public string? PhotoUrl { get; set; }
        public string? KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastAction { get; set; }
        public string? Gender { get; set; }
        public string? Indroduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public List<PhotoDTO>? Photos { get; set; }
    }
}
