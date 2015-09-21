namespace V5.Library.Security.Regular
{
    using System;
    using System.Text.RegularExpressions;

    public static class RegexValidate
    {
        #region Public Methods and Operators

        public static bool IsNumber(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^[0-9]*$", RegexOptions.ExplicitCapture);
        }

        public static bool IsEnglish(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^[A-Za-z]+$", RegexOptions.ExplicitCapture);
        }

        public static bool IsChinese(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^[\u4e00-\u9fa5]{0,}$", RegexOptions.ExplicitCapture);
        }

        public static bool IsZipCode(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^[1-9]\d{5}(?!\d)", RegexOptions.ExplicitCapture);
        }

        public static bool IsTelephone(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^(((\(\d{3,4}\))|(\d{3,4}-))?[1-9]\d{6,7}(-\d{0,4})?)?$", RegexOptions.ExplicitCapture);
        }

        public static bool IsMobilePhone(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", RegexOptions.ExplicitCapture);
        }

        public static bool IsIpAddress(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$", RegexOptions.ExplicitCapture);
        }

        public static bool IsEmailAddress(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^([\w\.\-]+@[\w\.\-]+\.[\w\.\-]+)$", RegexOptions.ExplicitCapture);
        }

        public static bool IsURL(string text)
        {
            ArgumentNullException(text);

            if (text.StartsWith("www."))
            {
                text = "http://" + text;
            }

            return RegexMatch.IsMatch(text, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", RegexOptions.ExplicitCapture);
        }

        public static bool IsIdentityCardNumber(string text)
        {
            ArgumentNullException(text);

            return RegexMatch.IsMatch(text, @"^(\d{17}[\d|X]|\d{15})$", RegexOptions.ExplicitCapture);
        }

        #endregion

        #region Methods

        private static void ArgumentNullException(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }
        }

        #endregion
    }
}