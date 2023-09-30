using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class PurchaseRepository
    {
        SQLiteConnection database;
        public PurchaseRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<Purchase>();
        }
        public IEnumerable<Purchase> GetPurchases()
        {
            return database.Table<Purchase>().ToList();
        }
        public Purchase GetPurchase(int id)
        {
            return database.Get<Purchase>(id);
        }
        public int DeletePurchase(int id)
        {
            return database.Delete<Purchase>(id);
        }
        public int SavePurchase(Purchase item)
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
