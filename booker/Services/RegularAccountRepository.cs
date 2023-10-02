using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class RegularAccountRepository: IAccountRepository
    {
        SQLiteConnection database;
        public RegularAccountRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<RegularAccount>();
        }
        public int DeleteAccount(int id)
        {
            return database.Delete<RegularAccount>(id);
        }

        public IEnumerable<IAccount> GetAccounts()
        {
            return database.Table<RegularAccount>().ToList();
        }

        public IAccount GetAccount(int id)
        {
            return database.Get<RegularAccount>(id);
        }

        public int SaveAccount(IAccount item)
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
