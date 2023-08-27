using Common.Dto;
using Common.ViewModel.Common;
using Common.ViewModel.System;

namespace Common.BL.Interfaces
{
    public interface IRoleBusinessService
    {
        DataTableViewModel<AppRoleDto> GetAll();

        void Create(RoleViewModel viewModel);

        void Delete(RoleViewModel viewModel);

        DataTableViewModel<AppRoleDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);

        RoleViewModel GetById(RoleViewModel viewModel);

        void Update(RoleViewModel viewModel);
    }
}