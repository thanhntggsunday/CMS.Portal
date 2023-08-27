using System.Web.Mvc;

namespace Portal.ServerProvider
{
    public class HasCredentialAttribute : HasCredentialAttributeBase
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/_401.cshtml"
            };
        }
    }
}