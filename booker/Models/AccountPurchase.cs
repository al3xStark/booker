using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    [Table("AccountPurchase")]
    public class AccountPurchase
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("account_id")]
        public int AccountID { get; set; }
        [Column("purchase_id")]
        public int PurchaseID { get; set; }
    }
}
