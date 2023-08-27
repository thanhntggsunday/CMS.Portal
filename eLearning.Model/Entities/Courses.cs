

using Common.Model;

namespace eLearning.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Courses : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Courses()
        {
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        public string Content { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? TrainerId { get; set; }

        public string Overview { get; set; }
        public string Requirements { get; set; }


        public virtual CourseCategory CourseCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseLessons> CourseLessons { get; set; } = new HashSet<CourseLessons>();

        public virtual Trainners Trainners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoursesStudents> CoursesStudents { get; set; } = new HashSet<CoursesStudents>();
    }
}