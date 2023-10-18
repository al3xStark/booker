using booker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace booker.Models
{
    [Table("Segments")]
    public class Segment
    {
        public readonly Period period;

        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("account_id")]
        public int AccountID { get; set; }
        [Column("Amount")]
        public int Amount { get; set; }
        [Column("initial_amount")]
        public int InitialAmount { get; set; }
        [Ignore]
        public List<Purchase> Purchases
        {
            get
            {
                var purchases = BookerRepository.AccountPurchase.GetPurchases(AccountID, AccountType.ComplexAccount);
                return purchases.Where(x => x.Created >= period.Start && x.Created <= period.End).ToList();
            }
        }
        public Segment(int account_id, int amount, Period period)
        {
            AccountID = account_id;
            InitialAmount = Amount = amount;
            this.period = period;
        }
        public Segment() { }
    }
}
