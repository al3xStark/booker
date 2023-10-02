using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace booker.Services
{
    public class AccountRepository
    {
        private List<IAccountRepository> accounts;
        private RegularAccountRepository regularAccountRep;
        private ComplexAccountRepository complexAccountRep;
        public AccountRepository(SQLiteConnection database)
        {
            regularAccountRep = new RegularAccountRepository(database);
            accounts.Add(regularAccountRep);
            complexAccountRep = new ComplexAccountRepository(database);
            accounts.Add(complexAccountRep);
        }
        public RegularAccountRepository RegularAccounts => regularAccountRep;
        public ComplexAccountRepository ComplexAccounts => complexAccountRep;
        public IEnumerable<IAccount> GetAccounts()
        {
            var list = new List<IAccount>();
            foreach (var account in accounts)
                list.AddRange(account.GetAccounts());
            return list;
        }
        public int SaveAccount(IAccount item)
        {
            if (item is ComplexAccount)
                return complexAccountRep.SaveAccount(item);
            else if (item is RegularAccount)
                return regularAccountRep.SaveAccount(item);
            else return 0;
        }
        public int DeleteAccount(IAccount item) 
        {
            if (item is ComplexAccount)
                return complexAccountRep.DeleteAccount(item.ID);
            else if (item is RegularAccount)
                return regularAccountRep.DeleteAccount(item.ID);
            else return 0;
        }
    }
}
