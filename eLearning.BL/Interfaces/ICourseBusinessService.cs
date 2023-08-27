
using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface ICourseBusinessService
    {
        DataTableViewModel<CourseDto> GetAll();

        void Create(ApiResult<CourseDto> viewModel);

        void Delete(ApiResult<CourseDto> viewModel);

        DataTableViewModel<CourseDto> GetAllPaging(int startIndex, int endIndex, int intDraw, string search);

        ApiResult<CourseDto> GetById(CourseDto dto);

        void Update(ApiResult<CourseDto> viewModel);
    }
}