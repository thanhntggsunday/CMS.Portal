using Common.Dto;
using Common.Model.Entities;
using Common.ViewModel;
using Common.ViewModel.Common;
using System.Collections.Generic;

namespace Common.BL.Interfaces
{
    public interface IPermissionBusinessService
    {
        void BulkInsert(List<Role_Permission> permissions, string[] arrRolesId, string[] arrFunctionActionId, out TransactionalInformation transactional);

        DataTableViewModel<AppPermissionDto> GetAll();

        DataTableViewModel<AppPermissionDto> GetAllPaging(int startIndex, int endIndex, string search);

        DataTableViewModel<AppAllUserPermissionDto> GetAllUserPermissionsByUserId(string userId);

        void Insert(Role_Permission permission, out TransactionalInformation transactional);
    }
}