

using Common.Model;

namespace eLearning.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class Trainners : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainners()
        {
            // Courses = new HashSet<Courses>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Avatar { get; set; }

        [StringLength(500)]
        public string Bio { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        // public virtual ICollection<Courses> Courses { get; set; }
    }
}