using Common.Dto;
using Common.ViewModel.Common;

namespace Common.BL.Interfaces
{
    public interface IFunctionBusinessService
    {
        DataTableViewModel<AppFunctionDto> GetAll();
    }
}