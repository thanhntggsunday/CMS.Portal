

using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface ICourseCategoryBusinessService
    {
        DataTableViewModel<CourseCategoryDto> GetAll();

        void Create(ApiResult<CourseCategoryDto> viewModel);

        void Delete(ApiResult<CourseCategoryDto> viewModel);

        DataTableViewModel<CourseCategoryDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        ApiResult<CourseCategoryDto> GetById(CourseCategoryDto dto);

        void Update(ApiResult<CourseCategoryDto> viewModel);
    }
}