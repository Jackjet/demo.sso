using SSO_DaL;
using SSO_DaL.Entity;
using SSO_Entity;
using SSO_Security;
using SSO_Server.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace SSO_Server.Handles
{
    /// <summary>
    /// SSO_ServerCenter 的摘要说明
    /// </summary>
    public class SSO_ServerCenter : IHttpHandler, IRequiresSessionState
    {
        #region 接口统一中心

        public void ProcessRequest(HttpContext context)
        {
            string Func = context.Request["Func"];
            //方法名称
            switch (Func)
            {
                //登陆
                case "Login":
                    this.Login(context);
                    break;
                //注销
                case "LoginOut":
                    this.LoginOut(context);
                    break;
                //获取令牌ID
                case "GetTokenID":
                    this.GetTokenID(context);
                    break;
                //通过令牌获取用户信息
                case "GetUserInfoByToken":
                    this.GetUserInfoByToken(context);
                    break;
                //令牌验证
                case "ValidateToken":
                    this.ValidateToken(context);
                    break;
                default:
                    break;
            }
        }

        #region 必要属性

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region 用户登录

        public void Login(HttpContext context)
        {
            //响应者
            HttpResponse Response = context.Response;
            //请求者
            HttpRequest Request = context.Request;
            //往返路径
            string returnUrl = Request["returnUrl"];

            //如果没有回调链接,该SSO不起作用
            if (string.IsNullOrEmpty(returnUrl))
            {
                return;
            }
            else
            {
                //判断returnUrl是否为信任的Domain
            }
            if (Request["UserName"] != "" && Request["password"] != "")
            {
                var pp = context.User.Identity.Name;

                string userName = Request["UserName"];
                string passWord = Request["password"];

                bool result = SmartAuthenticate.AuthenticateUser(userName, passWord, true);

                //SSOToken token = null;
                ////判断当前是否登录（）
                //if (SmartAuthenticate.LoginUser != null)//未登录（生成token【携带用户信息】,并加入到集合里去）
                //{
                //    SmartAuthenticate.LoginUser.UserName = userName;
                //    token = TokenManage.SetToken(context.Session.SessionID);
                //}


                SSOToken token = null;
                //判断当前是否登录（）
                if (!string.IsNullOrEmpty(userName))//未登录（生成token【携带用户信息】,并加入到集合里去）
                {
                    //SmartAuthenticate.LoginUser.UserName = userName;
                    token = TokenManage.SetToken(context.Session.SessionID, userName,passWord);
                }

                //拼接返回的url，参数中带Tip
                string spliter = returnUrl.Contains('?') ? "&" : "?";
                if (token != null)
                {
                    //将TIP返回
                    returnUrl = returnUrl + spliter + "token=" + token.ID + "&IsSuccessed=" + "true";
                }
                else
                {
                    //将TIP返回
                    returnUrl = returnUrl + spliter + "&IsSuccessed=" + "false";
                }

                string callback = context.Request["jsoncallback"];
                Response.Write(callback + "({\"result\":\"" + token.ID + "\"})");

                //跳转到访问该SSO的初始页面
                //Response.Redirect(returnUrl);
            }
        }

        #endregion

        #region 用户注销

        public void LoginOut(HttpContext context)
        {
            //响应者
            HttpResponse Response = context.Response;
            //请求者
            HttpRequest Request = context.Request;
            //往返路径
            string returnUrl = Request["returnUrl"];

            //如果没有回调链接,该SSO不起作用
            if (string.IsNullOrEmpty(returnUrl))
            {
                return;
            }
            else
            {
                //判断returnUrl是否为信任的Domain
            }
            if (Request["loginID"] != null)
            {
                //注销登录
                SmartAuthenticate.SignOut();
                //删除相关的SSOToken
                int count = SSOToken.SSOTokenList.RemoveAll(m => m.LoginID == Request["loginID"]);

                string result = "false";
                if (count > 0)
                {
                    result = "true";
                }

                //拼接返回的url，参数中带Tip
                string spliter = returnUrl.Contains('?') ? "&" : "?";
                //将TIP返回
                returnUrl = returnUrl + spliter + "IsLoginOut=" + result;
                //跳转到访问该SSO的初始页面
                Response.Redirect(returnUrl);
            }
        }

        #endregion

        #region 获取TokenID

        /// <summary>
        /// 获取TokenID
        /// </summary>
        /// <param name="context"></param>
        public void GetTokenID(HttpContext context)
        {
            //响应者
            HttpResponse Response = context.Response;
            //请求者
            HttpRequest Request = context.Request;
            //往返路径
            string returnUrl = Request["returnUrl"];

            //如果没有回调链接,该SSO不起作用
            if (string.IsNullOrEmpty(returnUrl))
            {
                return;
            }
            else
            {
                //判断returnUrl是否为信任的Domain
            }
            //判断当前是否登录（）
            //if (SmartAuthenticate.LoginUser != null)//未登录（生成token【携带用户信息】,并加入到集合里去）
            //{
            //    SSOToken token = TokenManage.SetToken(context.Session.SessionID);

            //    //拼接返回的url，参数中带TokenID
            //    string spliter = returnUrl.Contains('?') ? "&" : "?";
            //    //将tokenID返回
            //    returnUrl = returnUrl + spliter + "token=" + token.ID + "&Tip=" + "true";
            //    //跳转到访问该SSO的初始页面
            //    Response.Redirect(returnUrl);
            //}
            //else
            //{
            //    //拼接返回的url，参数中带Tip
            //    string spliter = returnUrl.Contains('?') ? "&" : "?";
            //    //将TIP返回
            //    returnUrl = returnUrl + spliter + "Tip=" + "false";
            //    //跳转到访问该SSO的初始页面
            //    Response.Redirect(returnUrl);
            //}
        }

        #endregion

        #region 通过TokenID获取用户信息

        public void GetUserInfoByToken(HttpContext context)
        {
            //响应者
            HttpResponse Response = context.Response;
            //请求者
            HttpRequest Request = context.Request;
            //往返路径
            string returnUrl = Request["returnUrl"];

            JsonModel jsonModel = new JsonModel();
           
            if (Request["tokenID"] != string.Empty)
            {
                string tokenID = Request["tokenID"];
                if (KeepToken(tokenID))
                {
                    SSOToken token = KeepToken_Valied(tokenID);
                    if (token != null)
                    {
                        Sys_UserInfo userInfo = UserManage.GetUserInfo(token.User.UserName,token.User.PassWord);
                        if (userInfo != null)
                        {
                            jsonModel.retData = userInfo;
                            jsonModel.errNum = 0;
                        }
                        else
                        {
                            jsonModel.errMsg = "用户名密码错误";
                            jsonModel.errNum = 999;
                        }
                    }
                    else
                    {
                        jsonModel.errMsg = "获取用户信息失败";
                        jsonModel.errNum =222;
                    }
                }
                else
                {
                    jsonModel.errMsg = "无效令牌";
                    jsonModel.errNum = 333;
                }
            }
            else
            {
                jsonModel.errMsg = "未获取到令牌";
                jsonModel.errNum = 666;
            }
            //Response.Write(JsHelper.Serialize(new { Data = returnData }));

            string callback = context.Request["jsoncallback"];
            Response.Write(callback + "({\"result\":" + JsHelper.Serialize(jsonModel) + "})");

        }

        #endregion

        #region 验证Token有效性（根据IP和时间作为限制条件）

        /// <summary>
        /// 根据tokenID获取token令牌
        /// </summary>
        /// <param name="tokenID"></param>
        /// <returns></returns>

        public void ValidateToken(HttpContext context)
        {
            //响应者
            HttpResponse Response = context.Response;
            //请求者
            HttpRequest Request = context.Request;
            //往返路径
            string returnUrl = Request["returnUrl"];
            if (Request["tokenID"] != string.Empty)
            {
                bool isValid = false;
                string tokenID = Request["tokenID"];
                if (KeepToken(tokenID))
                {
                    var token = SSOToken.SSOTokenList.Find(m => m.ID == tokenID);
                    if (token != null)
                    {
                        isValid = true;
                    }
                }
            }
        }

        /// <summary>
        /// 验证当前token是否有效
        /// </summary>
        /// <param name="tokenID"></param>
        /// <returns></returns>
        public bool KeepToken(string tokenID)
        {
            var token = SSOToken.SSOTokenList.Find(m => m.ID == tokenID);
            if (token == null)
                return false;
            if (token.IsTimeOut())
                return false;

            token.AuthTime = DateTime.Now;
            return true;
        }

        /// <summary>
        /// 验证当前token是否有效
        /// </summary>
        /// <param name="tokenID"></param>
        /// <returns></returns>
        public SSOToken KeepToken_Valied(string tokenID)
        {
            SSOToken ssotoken = null;
            ssotoken = SSOToken.SSOTokenList.Find(m => m.ID == tokenID);
            if (ssotoken != null)
            {
                if (!ssotoken.IsTimeOut())
                {
                    ssotoken.AuthTime = DateTime.Now;
                }
                else
                {
                    ssotoken = null;
                }
            }
            return ssotoken;
        }

        #endregion
    }
}