
using SSO_Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO_Test.Handles
{
    /// <summary>
    /// LoginOut 的摘要说明
    /// </summary>
    public class LoginOut : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string CMD = context.Request.Form["CMD"];
            if (CMD == "LoginOut")
            {
                string LoginID = context.Request.Form["LoginID"];

                if (!string.IsNullOrEmpty(LoginID))
                {
                    context.Response.Redirect("http://192.168.1.89:3001/SSO_Server/Handles/LoginOut_Handle.ashx?" + "loginID=" + LoginID + "&returnUrl=" +
                           context.Server.UrlEncode("http://192.168.1.89:3001/SSO_Test/Login2.aspx"));
                }
                else
                {
                    context.Response.Write(JsHelper.Serialize(new { Data = "用户未登录" }));
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}