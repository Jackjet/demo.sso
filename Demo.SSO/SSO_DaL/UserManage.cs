using SSO_DaL.Common;
using SSO_DaL.Entity;
using SSO_Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SSO_DaL
{
    public static class UserManage
    {
        #region 获取用户信息

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static Sys_UserInfo GetUserInfo(string LoginName, string pwd)
        {
            Sys_UserInfo userInfo = null;
            string error = null;
            string sql = string.Empty;
            //存储过程名称（更新数据库所有表中需要更新的卡号为新卡号）
            sql = "dbo_SSO_UserInfoSelect";
            SqlParameter sqlParameter1 = new SqlParameter("LoginName", LoginName);
            SqlParameter sqlParameter2 = new SqlParameter("Password", pwd);
            //DBHelper.Transaction(sql, out error);
            List<Sys_UserInfo> _SSoUser_List = DBHelper.ExcuteEntity<Sys_UserInfo>(sql, CommandType.StoredProcedure, out error, sqlParameter1, sqlParameter2);
            if (string.IsNullOrEmpty(error) && _SSoUser_List.Count > 0)
            {

                userInfo = _SSoUser_List[0];
                if (!string.IsNullOrEmpty(userInfo.HeadPic))
                {
                    userInfo.AbsHeadPic = System.Configuration.ConfigurationManager.AppSettings["PhotoPre"] + userInfo.HeadPic;
                }

                if (userInfo.CreateTime != null)
                {
                    string dt = ((DateTime)userInfo.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
                    userInfo.CreateTime = Convert.ToDateTime(dt);
                }

                if (userInfo.EditTime != null)
                {
                    string d2t = ((DateTime)userInfo.EditTime).ToString("yyyy-MM-dd HH:mm:ss");
                    userInfo.EditTime = Convert.ToDateTime(d2t);
                }

                if (userInfo.Birthday != null)
                {
                    string d3t = ((DateTime)userInfo.Birthday).ToString("yyyy-MM-dd HH:mm:ss");
                    userInfo.Birthday = Convert.ToDateTime(d3t);
                }
            }

            return userInfo;
        }

        #endregion
    }
}
