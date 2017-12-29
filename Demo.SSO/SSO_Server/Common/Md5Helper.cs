using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SSO_Server.Common
{
    public static class Md5Helper
    {
        #region md5加密

        public static string Md5(string content)
        {
            string md5Code = null;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(content);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组
            md5Code = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串

            return md5Code;
        }

        #endregion
    }
}