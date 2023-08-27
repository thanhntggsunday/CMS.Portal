using Common.Constants;
using Common.Dto;
using Common.Model.Entities;
using Common.ViewModel.Common;
using System.Web;
using System.Web.Mvc;

namespace Portal.ServerProvider
{
    public class HasCredentialAttributeBase : AuthorizeAttribute
    {
        public string Function;
        public string Action;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // var user = httpContext.User;

            // var lstUserPers = HttpContext.Current.Session[SessionConstants.USER_PERMISSION] as List<AllUserPermissionDTO>;
            PermissionHandle permissionHandle = new PermissionHandle();
            AppUser user = HttpContext.Current.Session[SessionConstants.USER_SESSION] as AppUser;
            var lstUserPers = new DataTableViewModel<AppAllUserPermissionDto>();

            if (user != null)
            {
                lstUserPers = permissionHandle.GetAllUserPermissionByUserId(user.Id);
            }

            if (lstUserPers == null || lstUserPers.data == null || lstUserPers.data.Length == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < lstUserPers.data.Length; i++)
                {
                    var item = lstUserPers.data[i];

                    if ((Function == FunctionConstants.PERMISSION)
                        && !string.IsNullOrEmpty(item.RoleName)
                        && item.RoleName.ToUpper() == RoleConstants.ADMIN)
                    {
                        return true;
                    }

                    if ((Function == FunctionConstants.HOME)
                        && !string.IsNullOrEmpty(item.RoleName)
                        && RoleConstants.SYSTEM_ADMIN_ROLES.ToUpper().Contains(item.RoleName.ToUpper()))
                    {
                        return true;
                    }

                    if ((Function == FunctionConstants.HOME) && Action == LevelPermissionsConstants.EDIT
                        && !string.IsNullOrEmpty(item.RoleName)
                        && item.RoleName.ToUpper() == RoleConstants.ADMIN)
                    {
                        return true;
                    }

                    if (!string.IsNullOrEmpty(item.FunctionId) && !string.IsNullOrEmpty(item.ActionId)
                                                               && item.FunctionId.ToUpper() == Function && item.ActionId.ToUpper() == Action)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}