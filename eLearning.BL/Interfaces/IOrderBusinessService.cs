
using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface IOrderBusinessService
    {
        DataTableViewModel<OrderDto> GetAll();

        void Create(ApiResult<OrderDto> viewModel);

        void Delete(ApiResult<OrderDto> viewModel);

        DataTableViewModel<OrderDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        ApiResult<OrderDto> GetById(OrderDto dto);

        void Update(ApiResult<OrderDto> viewModel);
    }
}