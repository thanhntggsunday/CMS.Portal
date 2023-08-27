using Common.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface IContentCategoryBusinessService
    {
        DataTableViewModel<ContentCategoryDto> GetAll();

        void Create(ApiResult<ContentCategoryDto> viewModel);

        void Delete(ApiResult<ContentCategoryDto> viewModel);

        DataTableViewModel<ContentCategoryDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        ApiResult<ContentCategoryDto> GetById(ContentCategoryDto dto);

        void Update(ApiResult<ContentCategoryDto> viewModel);
    }
}
