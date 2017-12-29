using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSO_Test
{
    public partial class Login3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CMD = Request.Form["CMD"];
            if (CMD == "GetUserInfo")
            {
                //string tokenID = Request.Form["tokenID"];
                //if (!string.IsNullOrEmpty(tokenID))
                //{                   
                //    Response.Redirect("http://192.168.1.69:3001/SSO_Server/Login.aspx?" + "UserName=" + userName.Trim() + "&Code=" + password_Code.Trim() + "&returnUrl=" +
                //            Server.UrlEncode("http://192.168.1.69:3001/SSO_Test/Login.aspx"));

                //}
            }
        }
    }
}