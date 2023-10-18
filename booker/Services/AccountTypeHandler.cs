using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace booker.Services
{
    public enum AccountType
    {
        RegularAccount,
        ComplexAccount
    }
    public class AccountTypeHandler
    {
        private static Dictionary<AccountType, string> TypeTitles = new Dictionary<AccountType, string>()
        {
            [AccountType.RegularAccount] = "Regular Account",
            [AccountType.ComplexAccount] = "Complex Account"
        };
        public static List<string> GetTypeTitles()
        {
            var types = new List<string>();
            foreach (var type in TypeTitles)
                types.Add(type.Value);
            return types;
        }
        public static string GetTypeTitle(AccountType type) => TypeTitles[type];
        public static AccountType GetType(string typeTitle) => TypeTitles.First(x => x.Value == typeTitle).Key;
    }
}
