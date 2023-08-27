namespace Common.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Function_Action : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Function_Action()
        {
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ActionId { get; set; }

        [Required]
        [StringLength(100)]
        public string FunctionId { get; set; }

        public virtual Action Action { get; set; }

        public virtual Function Function { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role_Permission> Role_Permission { get; set; } = new HashSet<Role_Permission>();
    }
}
