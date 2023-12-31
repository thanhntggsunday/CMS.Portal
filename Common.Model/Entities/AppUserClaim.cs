namespace Common.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AppUserClaim
    {
        [Key]
        public string UserId { get; set; }

        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        [StringLength(128)]
        public string AppUser_Id { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
