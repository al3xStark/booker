using booker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class ProductRepository
    {
        SQLiteConnection database;
        public ProductRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<Product>();
        }
        public IEnumerable<Product> GetProducts()
        {
            return database.Table<Product>().ToList();
        }
        public Product GetProduct(int id)
        {
            return database.Get<Product>(id);
        }
        public int DeleteProduct(int id)
        {
            return database.Delete<Product>(id);
        }
        public int SaveProduct(Product item)
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
