namespace Common.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_Permission : EntityBase
    {
        public int ID { get; set; }

        public int Function_ActionID { get; set; }

        [Required]
        [StringLength(128)]
        public string AppRoleId { get; set; }

        public virtual AppRole AppRole { get; set; }

        public virtual Function_Action Function_Action { get; set; }
    }
}
