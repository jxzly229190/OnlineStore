// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderCode.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The order code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Library
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The order code.
    /// </summary>
    public static class OrderCode
    {
        /// <summary>
        /// The characters.
        /// </summary>
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        /// <summary>
        /// The code size.
        /// </summary>
        private const int codeSize = 9;

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Create()
        {
            var chars = characters.ToCharArray();
            var data = new byte[1];

            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[codeSize];
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(codeSize);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }
    }
}