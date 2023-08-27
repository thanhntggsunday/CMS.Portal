

using Common.Model;

namespace eLearning.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CourseLessons : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseLessons()
        {
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string VideoPath { get; set; }

        [StringLength(250)]
        public string SlidePath { get; set; }

        [StringLength(250)]
        public string Attachment { get; set; }

        public int? SortOrder { get; set; }

       
        public int? CourseId { get; set; }

       
        public virtual Courses Courses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonComments> LessonComments { get; set; } = new HashSet<LessonComments>();
    }
}