
using SSO_Entity;
using SSO_Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SSO_Server.Common
{
    public static class TokenManage
    {
        public static SSOToken SetToken(string loginID, string pswd)
        {
            //生成Token，并持久化Token
            SSOToken token = new SSOToken();

            //token携带的用户信息
            token.User = new SSOUser();
            //token携带的用户名称
            token.User.UserName = SmartAuthenticate.LoginUser.UserName;
            token.User.PassWord = pswd;
            //登录ID
            token.LoginID = loginID;
            //token集合添加token
            SSOToken.SSOTokenList.Add(token);

            return token;
        }

        public static SSOToken SetToken(string loginID,string userName,string pswd)
        {
            //生成Token，并持久化Token
            SSOToken token = new SSOToken();

            //token携带的用户信息
            token.User = new SSOUser();
            //token携带的用户名称
            token.User.UserName = userName;
            token.User.PassWord = pswd;
            //登录ID
            token.LoginID = loginID;
            //token集合添加token
            SSOToken.SSOTokenList.Add(token);

            return token;
        }
    }
}