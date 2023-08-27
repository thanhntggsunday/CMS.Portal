using Common.Dto;
using Common.ViewModel.Common;

namespace Common.BL.Interfaces
{
    public interface ILevelPermissionBusinessService
    {
        DataTableViewModel<AppLevelPermissionDto> GetAll();

        DataTableViewModel<AppLevelPermissionDto> GetAllLevelPermissionByFunctionId(string functionId);
    }
}