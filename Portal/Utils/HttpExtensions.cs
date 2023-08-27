using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Utils
{
    public static class HttpExtensions
    {
        public static string GetFullDomain(this HttpRequestBase request)
        {
            if (request.Url != null)
            {
                return $"{request.Url.Scheme}{System.Uri.SchemeDelimiter}{request.Url.Authority}";
            }
            else
            {
                return String.Empty;
            }
               
        }
    }
}