using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;

namespace eLearning.Common.Dto
{
    public class CourseStudentDto : EntityBaseDto
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public string AppUserId { get; set; }
        public string  UserName { get; set; }
        public string  FullName { get; set; }
        public string  Email { get; set; }
        public string  CourseName { get; set; }
        public decimal? Price { get; set; }
        public string CreatorId { get; set; }
        public string EditorId { get; set; }
    }
}
