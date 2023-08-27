using Common.Dto;
using Common.ViewModel.Common;

namespace Common.BL.Interfaces
{
    public interface IUserRoleBusinessService
    {
        DataTableViewModel<AppUserRolesDto> GetAllUserRolesPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        AppUserDto GetById(string Id);

        AppRoleDto[] GetJsonAllRole();

        AppRoleDto[] GetJsonAllRoleOfUserByUserId(string userId);

        AppUserDto[] GetJsonAllUser();
    }
}