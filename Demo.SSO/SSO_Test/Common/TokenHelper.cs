using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO_Test.Common
{
    public static class TokenHelper
    {
        #region 捕获token

        public static string TokenGet()
        {
            string tokenID = string.Empty;

            HttpRequest request = HttpContext.Current.Request;
            //判断跳转过来的地址中是否携带token
            if (!string.IsNullOrEmpty(request["token"]))
            {
                //从sso页面跳转过来的地址中获取token信息
                tokenID = request["token"];
                //将token信息存储到cookie里去
                HttpCookie tokenCookie = new HttpCookie("token", tokenID);
                //允许通过客户端脚本进行访问
                tokenCookie.HttpOnly = true;
                //cookies写入
                request.Cookies.Set(tokenCookie);               
            }
            else if (request.Cookies["token"] != null)
            {
                //从浏览器里读取token
                tokenID = request.Cookies["token"].Value;
            }
            return tokenID;
        }

        #endregion
    }
}