
namespace API_Template.Data.DataBase.Entites
{
    public class AppUsers
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public required string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastAction { get; set; } = DateTime.UtcNow;
        public string? Indroduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<Photo> Photos { get; set; } = [];

        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}

        //public required string Role { get; set; }
        //public required string Email { get; set; }
        //public required string Mobile { get; set; }
        //public required string Address { get; set; }
        //public required string FullName { get; set; }
        //public required string Gender { get; set; }
        //public required string Status { get; set; }
        //public required DateTime CreatedOn { get; set; }
        //public required int CreatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }
        //public int? UpdatedBy { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime? DeletedOn { get; set; }
        //public int? DeletedBy { get; set; }
        //public DateTime? LockedOn { get; set; }
        //public int? LockedBy { get; set; }
        //public bool IsLocked { get; set; }
    }
}
