namespace eLearning.Model.Entities
{
    using Common.Model;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ContentCategories")]
    public partial class ContentCategory : EntityBase
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool? ShowOnHome { get; set; }

        [StringLength(2)]
        public string Language { get; set; }

        [StringLength(250)]
        public string MetaCode { get; set; }
    }
}