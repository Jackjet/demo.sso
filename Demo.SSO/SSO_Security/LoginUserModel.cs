using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO_Security
{
    public class LoginUserModel
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public string LoginTime { get; set; }
    }
}