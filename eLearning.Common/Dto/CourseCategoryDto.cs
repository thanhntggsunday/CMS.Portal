using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class CourseCategoryDto : EntityBaseDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? SortOrder { get; set; }

        public string SeoAlias { get; set; }

        public string SeoMetaKeywords { get; set; }

        public string SeoMetaDescription { get; set; }

        public string SeoTitle { get; set; }

        public int? ParentId { get; set; }

        public string CreatorId { get; set; }
        public string EditorId { get; set; }
    }
}