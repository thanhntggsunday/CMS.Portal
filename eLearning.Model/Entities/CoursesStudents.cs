

using Common.Model;

namespace eLearning.Model.Entities
{
    public partial class CoursesStudents : EntityBase
    {
        public int Id { get; set; }

        public int? CourseId { get; set; }

        public string AppUserId { get; set; }

        public decimal? Price { get; set; }
        public virtual Courses Courses { get; set; }
    }
}