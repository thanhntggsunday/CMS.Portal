

using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface ICourseLessonBusinessService
    {
        DataTableViewModel<CourseLessonDto> GetAll();

        void Create(ApiResult<CourseLessonDto> viewModel);

        void Delete(ApiResult<CourseLessonDto> viewModel);

        // DataTableViewModel<CourseLessonDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);
        DataTableViewModel<CourseLessonDto> GetAllCouresLessonByCourseIDPaging(int courseID, int startIndex, int endIndex, int pageSize, int intDraw, string search);

        ApiResult<CourseLessonDto> GetById(CourseLessonDto dto);

        void Update(ApiResult<CourseLessonDto> viewModel);
    }
}