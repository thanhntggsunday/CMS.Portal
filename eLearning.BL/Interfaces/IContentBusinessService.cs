using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModel.Common;
using eLearning.Common.Constants;
using eLearning.Common.Dto;
using eLearning.Common.Enums;

namespace eLearning.BL.Interfaces
{
    public interface IContentBusinessService
    {
        DataTableViewModel<ContentDto> GetAll();

        DataTableViewModel<ContentDto> GetTopTen();

        void Create(ApiResult<ContentDto> viewModel);

        void Delete(ApiResult<ContentDto> viewModel);

        DataTableViewModel<ContentDto> GetAllPaging(int startIndex, int endIndex, int intDraw, string search);

        ApiResult<ContentDto> GetById(ContentDto dto);

        void Update(ApiResult<ContentDto> viewModel);
    }
}