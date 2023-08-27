using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class ContentDto : EntityBaseDto
    {
        public long Id { get; set; }

        public string ContentName { get; set; }

        public string MetaTitle { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public long? CategoryID { get; set; }

        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescriptions { get; set; }
      
        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        public string Tags { get; set; }

        public string Language { get; set; }

        public int ItemType { get; set; }

        public string CategoryName { get; set; }
        public string MetaCode { get; set; }
    }
}