namespace V5.Library.Security.Regular
{
    using System;
    using System.Text.RegularExpressions;

    public static class RegexMatch
    {
        #region Public Methods and Operators

        public static bool IsMatch(string text, string regexExpression)
        {
            return IsMatch(text, regexExpression, RegexOptions.IgnoreCase);
        }

        public static bool IsMatch(string text, string regexExpression, RegexOptions regexOptions)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            if (string.IsNullOrEmpty(regexExpression))
            {
                throw new ArgumentNullException("regexExpression");
            }

            return Regex.IsMatch(text, regexExpression, regexOptions);
        }

        #endregion
    }
}