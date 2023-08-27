
using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface ICourseStudentBusinessService
    {
        DataTableViewModel<CourseStudentDto> GetAll();

        void Create(ApiResult<CourseStudentDto> viewModel);

        void Delete(ApiResult<CourseStudentDto> viewModel);

        // DataTableViewModel<CourseStudentDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);
        DataTableViewModel<CourseStudentDto> GetAllStudentByCourseIDPaging(int courseID, int startIndex, int endIndex, int pageSize, int intDraw, string search);

        ApiResult<CourseStudentDto> GetById(CourseStudentDto dto);

        void Update(ApiResult<CourseStudentDto> viewModel);
    }
}