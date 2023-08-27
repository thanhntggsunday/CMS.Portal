using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.ServerProvider
{
    public class AppJsonResult : JsonResult
    {
        public bool IsAccessDenied { get; set; }
    }
}