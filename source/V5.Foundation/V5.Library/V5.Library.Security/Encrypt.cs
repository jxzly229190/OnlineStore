namespace V5.Library.Security
{
    using System;
    using System.Web.Security;

    public class Encrypt
    {
        #region Public Methods and Operators

        public static string HashByMD5(string characters)
        {
            if (string.IsNullOrEmpty(characters))
            {
                throw new ArgumentNullException("characters");
            }

            return FormsAuthentication.HashPasswordForStoringInConfigFile(characters, "MD5");
        }

        public static string HashBySHA1(string characters)
        {
            if (string.IsNullOrEmpty(characters))
            {
                throw new ArgumentNullException("characters");
            }

            return FormsAuthentication.HashPasswordForStoringInConfigFile(characters, "SHA1");
        }

		#region HW-ERP MD5签名加密
		/// <summary>
		/// MD5加密
		/// </summary>
		/// <param name="strFeed">要加密的字符串</param>
		/// <returns>加密后的字符串</returns>
		public static string HwErpMd5(string strFeed)
		{
			var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] encryptedBytes = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(strFeed));
			var sBuilder = new System.Text.StringBuilder();
			for (int i = 0; i < encryptedBytes.Length; i++)
				sBuilder.AppendFormat("{0:x2}", encryptedBytes[i]);
			return sBuilder.ToString();
		}
		#endregion

        #endregion
    }
}