using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public  class SlideDto : EntityBaseDto
    {
        public int ID { get; set; }

        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

      
    }
}