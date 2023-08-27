using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class ContentCategoryDto : EntityBaseDto
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        public string SeoTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescriptions { get; set; }
      
        public bool? ShowOnHome { get; set; }

        public string Language { get; set; }
    }
}