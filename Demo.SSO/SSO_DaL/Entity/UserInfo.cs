using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSO_DaL.Entity
{
    [Serializable]
    public partial class Sys_UserInfo
    {

        /// <summary>
        ///主键 
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        ///用户唯一值 
        /// </summary>
        public string UniqueNo { get; set; }
        /// <summary>
        ///用户类型 
        /// </summary>
        public Byte? UserType { get; set; }
        /// <summary>
        ///姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///昵称 
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        ///性别 
        /// </summary>
        public Byte? Sex { get; set; }
        /// <summary>
        ///联系电话 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///出生日期 
        /// </summary>
        public DateTime? Birthday { get; set; }
        /// <summary>
        ///用户账号 
        /// </summary>
        public string LoginName { get; set; }
        ///// <summary>
        /////密码 
        ///// </summary>
        //public string Password { get; set; }
        /// <summary>
        ///身份证件号 
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        ///头像 
        /// </summary>
        public string HeadPic { get; set; }
        /// <summary>
        ///注册的组织机构 
        /// </summary>
        public string RegisterOrg { get; set; }
        /// <summary>
        ///认证类型 
        /// </summary>
        public Byte? AuthenType { get; set; }
        /// <summary>
        ///现住址 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///备注 
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        ///创建人 
        /// </summary>
        public string CreateUID { get; set; }
        /// <summary>
        ///创建时间 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///修改人 
        /// </summary>
        public string EditUID { get; set; }
        /// <summary>
        ///修改时间 
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        ///启用/禁用 
        /// </summary>
        public Byte? IsEnable { get; set; }
        /// <summary>
        ///是否删除 
        /// </summary>
        public Byte? IsDelete { get; set; }
        /// <summary>
        ///是否第一次登陆
        /// </summary>
        public int IsFirstLogin { get; set; }
        


        /// <summary>
        /// 绝对路径存储（头像）
        /// </summary>
        public string AbsHeadPic { get; set; }
    }
}
