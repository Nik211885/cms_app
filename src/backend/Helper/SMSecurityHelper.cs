using Microsoft.Extensions.Primitives;
using System.Text;

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
        public static string GenerateNewPassword(int length = 8)
        {
            List<int> check = new();
            var random = new Random();
            var password = new StringBuilder().Append("quacert");
            for (int i = 0; i < length - 1; i++)
            {
                var rand = random.Next(0, 4);
                switch (rand)
                {
                    case 0:
                        password.Append(Convert.ToChar(random.Next(48, 58)));
                        check.Add(rand);
                        break;
                    case 1:
                        password.Append(Convert.ToChar(random.Next(65, 91)));
                        check.Add(rand);
                        break;
                    case 2:
                        password.Append(Convert.ToChar(random.Next(97, 123)));
                        check.Add(rand);
                        break;
                    case 3:
                        var randSpecialCase = random.Next(0, 4);
                        check.Add(rand);
                        switch (randSpecialCase)
                        {
                            case 0:
                                password.Append(Convert.ToChar(random.Next(33, 48)));
                                break;
                            case 1:
                                password.Append(Convert.ToChar(random.Next(60, 65)));
                                break;
                            case 2:
                                password.Append(Convert.ToChar(random.Next(91, 97)));
                                break;
                            case 3:
                                password.Append(Convert.ToChar(random.Next(123, 127)));
                                break;
                        }
                        break;
                }
            }
            if (!check.Any(x => x == 0))
            {
                password.Append(Convert.ToChar(random.Next(48, 58)));
            }
            if (!check.Any(x => x == 1))
            {
                password.Append(Convert.ToChar(random.Next(65, 91)));
            }
            if (!check.Any(x => x == 2))
            {
                password.Append(Convert.ToChar(random.Next(97, 123)));
            }
            if (!check.Any(x => x == 3))
            {
                var randSpecialCase = random.Next(0, 4);
                switch (randSpecialCase)
                {
                    case 0:
                        password.Append(Convert.ToChar(random.Next(33, 48)));
                        break;
                    case 1:
                        password.Append(Convert.ToChar(random.Next(60, 65)));
                        break;
                    case 2:
                        password.Append(Convert.ToChar(random.Next(91, 97)));
                        break;
                    case 3:
                        password.Append(Convert.ToChar(random.Next(123, 127)));
                        break;
                }
            }
            return password.ToString();
        }
    }
}
