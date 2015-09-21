namespace V5.Portal.Common
{
    using System.Text.RegularExpressions;

    public static class CustomValidator
    {
        public static bool IsMobile(string str)
        {
            var reg = Regex.Match(str, @"^(((13[0-9]{1})|15[0-9]{1}|18[0-9]{1})+\d{8})$", RegexOptions.IgnoreCase);
            if (reg.Success)
            {
                return true;
            }
            return false;
        }

        public static bool IsPhone(string str)
        {
            var reg = Regex.Match(str, @"^(([0\+]\d{2,3}-?)?(0\d{2,3})-?)(\d{7,8})(-?(\d{3,}))?$", RegexOptions.IgnoreCase);
            if (reg.Success)
            {
                return true;
            }
            return false;
        }
    }
}