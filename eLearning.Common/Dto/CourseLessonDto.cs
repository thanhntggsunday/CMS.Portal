using System;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class CourseLessonDto : EntityBaseDto
    {
        public int Id { get; set; }

        public string CourseName { get; set; }
        public string LessonName { get; set; }

        public string VideoPath { get; set; }

        public string SlidePath { get; set; }

        public string Attachment { get; set; }

        public int? SortOrder { get; set; }

        public int? CourseId { get; set; }
     
        public string CreatorId { get; set; }
        public string EditorId { get; set; }
    }
}