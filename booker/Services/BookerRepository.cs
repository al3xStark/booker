using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace booker.Services
{
    public class BookerRepository
    {
        public const string DATABASE_NAME = "booker.db";
        private static BookerRepository instance;
        private static SQLiteConnection database;
        private static PurchaseRepository purchaseRep;
        private static ProductRepository productRep;

        private BookerRepository()
        {
            database = new SQLiteConnection(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
        }
        public static BookerRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new BookerRepository();
                return instance;
            }
        }
        public static PurchaseRepository Purchases
        {
            get
            {
                if (purchaseRep == null)
                    purchaseRep = new PurchaseRepository(database);
                return purchaseRep;
            }
        }
        public static ProductRepository Products
        {
            get
            {
                if (productRep == null)
                    productRep = new ProductRepository(database);
                return productRep;
            }
        }
    }
}
