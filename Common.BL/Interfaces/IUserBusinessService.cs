using Common.Dto;
using Common.ViewModel.Common;

namespace Common.BL.Interfaces
{
    public interface IUserBusinessService
    {
        DataTableViewModel<AppUserDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        void UpdateUserAvatar(AppUserDto dto);
    }
}