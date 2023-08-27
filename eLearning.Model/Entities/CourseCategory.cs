

using Common.Model;

namespace eLearning.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CourseCategories")]
    public partial class CourseCategory : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseCategory()
        {
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? SortOrder { get; set; }

        [StringLength(250)]
        public string SeoAlias { get; set; }

        [StringLength(158)]
        public string SeoMetaKeywords { get; set; }

        [StringLength(158)]
        public string SeoMetaDescription { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public int? ParentId { get; set; }

        [StringLength(250)]
        public string MetaCode { get; set; }

        public virtual ICollection<Courses> Courses { get; set; } = new HashSet<Courses>();
    }
}