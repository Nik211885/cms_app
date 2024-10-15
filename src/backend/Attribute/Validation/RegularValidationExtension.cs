using System.Text.RegularExpressions;

namespace backend.Attribute.Validation
{
    public static class RegularHelpersExtension
    {
        public static bool IsPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                string pattern = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
                return Regex.IsMatch(password, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
