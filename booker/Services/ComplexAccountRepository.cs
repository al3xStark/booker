using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class ComplexAccountRepository: IAccountRepository
    {
        SQLiteConnection database;
        public ComplexAccountRepository(SQLiteConnection database)
        {
            this.database = database;
            SegmentRepository.GetInstance(database);
            database.CreateTable<ComplexAccount>();
        }
        public IEnumerable<IAccount> GetAccounts()
        {
            return database.Table<ComplexAccount>().ToList();
        }
        public IAccount GetAccount(int id)
        {
            return database.Get<ComplexAccount>(id);
        }
        public int DeleteAccount(int id)
        {
            return database.Delete<ComplexAccount>(id);
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
