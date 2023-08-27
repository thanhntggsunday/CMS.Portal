

using Common.Model;

namespace eLearning.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class LessonComments : EntityBase
    {
        public int Id { get; set; }

        public Guid? UserId { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        public int? LessonId { get; set; }

        public int? Report { get; set; }

        public virtual CourseLessons CourseLessons { get; set; }
    }
}