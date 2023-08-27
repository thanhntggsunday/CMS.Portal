
using Common.BL.BusinessImp;
using Common.Dto;
using Common.ViewModel.Common;

namespace Portal.ServerProvider
{
    public class PermissionHandle
    {
        private PermissionBusinessService permissionBusinessService = new PermissionBusinessService();

        public DataTableViewModel<AppAllUserPermissionDto> GetAllUserPermissionByUserId(string uid)
        {
            var lstUserPers = permissionBusinessService.GetAllUserPermissionsByUserId(uid);
            return lstUserPers;
        }
    }
}