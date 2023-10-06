using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace booker.Services
{
    public class AccountPurchaseRepository
    {
        SQLiteConnection database;
        public AccountPurchaseRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<AccountPurchase>();
        }
        public int DeleteItem(int id)
        {
            return database.Delete<AccountPurchase>(id);
        }

        public IEnumerable<Purchase> GetPurchases(Account account)
        {
            var purchases = new List<Purchase>();
            var list = database.Table<AccountPurchase>().Where(x => x.AccountID == account.ID).ToList();
            foreach (var item in list)
                purchases.Add(BookerRepository.Purchases.GetPurchase(item.PurchaseID));
            return purchases;
        }
        public IEnumerable<Purchase> GetPurchases(int account_id, AccountType type)
        {
            return GetPurchases(BookerRepository.Accounts.GetAccount(account_id, type));
        }
        public IEnumerable<IAccount> GetAccounts(Purchase purchase)
        {
            var accounts = new List<IAccount>();
            var list = database.Table<AccountPurchase>().Where(x => x.PurchaseID == purchase.ID).ToList();
            foreach (var item in list)
                accounts.Add(BookerRepository.Accounts.FindAccount(item.AccountID));
            return accounts;
        }

        public AccountPurchase GetItem(int id)
        {
            return database.Get<AccountPurchase>(id);
        }

        public int SaveItem(AccountPurchase item)
        {
            if (item.ID != 0)
            {
                database.Update(item);
                return item.ID;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
