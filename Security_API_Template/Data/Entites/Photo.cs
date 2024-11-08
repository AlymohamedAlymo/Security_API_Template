﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Security_API_Template.Data.Entites
{
    [Table("UsersPhotos")]
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        //Navigation properties
        public int AppUser { get; set; }
        public AppUsers User { get; set; } = null!;
    }
}
