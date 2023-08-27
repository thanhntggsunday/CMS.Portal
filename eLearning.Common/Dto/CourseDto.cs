using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class CourseDto : EntityBaseDto
    {
        public int Id { get; set; }

        public string CourseName { get; set; }
        public string CatagoryName { get; set; }
        public string TrainerName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? TrainerId { get; set; }
        
        public string CreatorId { get; set; }
        public string EditorId { get; set; }
    }
}