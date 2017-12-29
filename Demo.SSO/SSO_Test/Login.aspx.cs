
using SSO_Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSO_Test
{
    public partial class Login : System.Web.UI.Page
    {

        public string Catch = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            var t = HttpContext.Current.User.Identity.Name;

            if (Request["IsSuccessed"] != null)
            {
                if (Request["IsSuccessed"] == "true")
                {
                    Response.Clear();
                    Response.Write(JsHelper.Serialize(new { Data = Request["token"] }));
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write(JsHelper.Serialize(new { Data = "登录失败" }));
                    Response.End();
                }

            }         
        }
    }
}