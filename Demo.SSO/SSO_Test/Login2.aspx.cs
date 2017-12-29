
using SSO_Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSO_Test
{
    public partial class Login2 : System.Web.UI.Page
    {
        public string LoginID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            var tokenID = TokenHelper.TokenGet();

            if (Request["IsLoginOut"] != null)
            {
                Response.Clear();
                Response.Write(JsHelper.Serialize(new { Data = Request["IsLoginOut"] }));
                Response.End();
            }

            if (Request["Tip"] != null)
            {
                if (Request["Tip"] == "false")
                {
                    Response.Clear();
                    Response.Write(JsHelper.Serialize(new { Data = Request["Tip"] }));
                    Response.End();
                }
            }


            //没有tokenID则条用接口进行获取
            if (!string.IsNullOrEmpty(tokenID))
            {

                //获取token
                var token = Constant.client.ValidateToken(tokenID);
                if (token != null)
                {
                    LoginID = token.LoginID;

                    ////登录成功标识（加载注销页面）
                    //string LoginMessage = "登录成功，登录用户："
                    //          + token.User.UserName;

                    //bool result = Constant.client.LoginOut(LoginID);

                    //Response.Clear();
                    //Response.Write(JsHelper.Serialize(new { Data = LoginMessage }));
                    //Response.End();


                }
                else
                {
                    //到sso去返回tokenID  进入到server ---Login【有可能是过期的tokenID,需要重新登录】
                    Response.Redirect("http://192.168.1.69:3001/SSO_Server/Handles/SSO_ServerCenter.ashx?returnUrl=" +
                        Server.UrlEncode("http://192.168.1.69:3001/SSO_Test/Login2.aspx"));
                }
            }
            else
            {
                //到sso去返回tokenID 进入到server ---Login
                Response.Redirect("http://192.168.1.69:3001/SSO_Server/Handles/SSO_ServerCenter.ashx?returnUrl=" +
                    Server.UrlEncode("http://192.168.1.69:3001/SSO_Test/Login2.aspx"));
            }
        }
    }
}