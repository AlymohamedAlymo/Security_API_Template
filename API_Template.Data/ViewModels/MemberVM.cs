﻿namespace API_Template.Data.ViewModels
{
    public class MemberDto
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
        public List<PhotoVM>? Photos { get; set; }
    }
}
