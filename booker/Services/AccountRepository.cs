using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace booker.Services
{
    public enum AccountType
    {
        RegularAccount,
        ComplexAccount
    }
    public class AccountRepository
    {
        SQLiteConnection database;
        private List<IAccountRepository> accounts;
        private RegularAccountRepository regularAccountRep;
        private ComplexAccountRepository complexAccountRep;
        public AccountRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<Account>();
            regularAccountRep = new RegularAccountRepository(database);
            accounts.Add(regularAccountRep);
            complexAccountRep = new ComplexAccountRepository(database);
            accounts.Add(complexAccountRep);
        }
        public RegularAccountRepository RegularAccounts => regularAccountRep;
        public ComplexAccountRepository ComplexAccounts => complexAccountRep;
        private IAccountRepository GetRepository(IAccount account)
        {
            if (account is RegularAccount) return regularAccountRep;
            if (account is ComplexAccount) return complexAccountRep;
            else return null;
        }
        private IAccountRepository GetRepository(AccountType type)
        {
            if (type is AccountType.RegularAccount) return regularAccountRep;
            if (type is AccountType.ComplexAccount) return complexAccountRep;
            else return null;
        }
        private AccountType GetAccountType(IAccount account)
        {
            if (account is RegularAccount) return AccountType.RegularAccount;
            if (account is ComplexAccount) return AccountType.ComplexAccount;
            else return 0;
        }
        public Account GetAccount(int accountID, AccountType type)
        {
            return database.Table<Account>().First(x => x.AccountID == accountID && x.Type == type);
        }
        public IEnumerable<IAccount> GetAccounts()
        {
            var list = new List<IAccount>();
            foreach (var account in accounts)
                list.AddRange(account.GetAccounts());
            return list;
        }
        public IAccount FindAccount(int id)
        {
            var account = database.Get<Account>(id);
            var repository = GetRepository(account.Type);
            if (repository != null)
                return repository.GetAccount(account.AccountID);
            else return null;
        }
        public int SaveAccount(IAccount item)
        {
            var repository = GetRepository(item);
            if (repository != null)
            {
                if (item.ID == 0)
                {
                    int accountID = repository.SaveAccount(item);                
                    database.Insert(new Account(GetAccountType(item), accountID));
                    return accountID;
                }
                else return repository.SaveAccount(item);
            }
            else return 0;
        }
        public int DeleteAccount(IAccount item) 
        {
            var accountItems = database.Table<Account>().Where(x => x.AccountID == item.ID);
            foreach (var accountItem in accountItems)
                database.Delete<Account>(accountItem.ID);
            var repository = GetRepository(item);
            if (repository != null)
                return repository.DeleteAccount(item.ID);
            else return 0;
        }
    }
}
