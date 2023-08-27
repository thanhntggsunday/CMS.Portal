using System.Web.Mvc;

namespace Portal.ServerProvider
{
    public class AjaxHasCredentialAttribute : HasCredentialAttributeBase
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var result = new AppJsonResult();
            result.Data = "ACCESS_DENIED";
            result.IsAccessDenied = false;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            filterContext.Result = result;
        }
    }
}