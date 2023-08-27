namespace eLearning.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class AppUser
    {
        public string Id { get; set; }

        [StringLength(256)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { get; set; }

        public bool? Status { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(300)]
        public string Department { get; set; }

        [StringLength(300)]
        public string Position { get; set; }

        [StringLength(200)]
        public string Country { get; set; }

        [StringLength(50)]
        public string CountryRegionCode { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(10)]
        public string Postcode { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        [StringLength(50)]
        public string FileContentType { get; set; }
    }
}