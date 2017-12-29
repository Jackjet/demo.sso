using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SSO_Test.Common
{
    public static class JsHelper
    {
        public static string Serialize(object obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
    }
}