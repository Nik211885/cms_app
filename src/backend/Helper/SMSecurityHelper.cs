using UC.Core.Helpers;

namespace uc.api.cms.Helper
{
    public class SMSecurityHelper 
    {
        private static string KeyHashPassword { get; set; } = null!;
        public static void CreateKey(IConfigurationRoot conf)
        {
            KeyHashPassword ??= conf.GetValue<string>("KeyHash") ??
                    throw new ArgumentException("Key make hash password is not already");
        }
        public static string HashPassword(string password) 
            => SecurityHelper.Encrypt(password, KeyHashPassword);
        public static bool VerifyPassword(string passwordHash, string password) =>
            SecurityHelper.Decrypt(passwordHash, KeyHashPassword).Equals(password);
    }
}
