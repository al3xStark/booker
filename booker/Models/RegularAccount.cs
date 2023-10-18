using booker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace booker.Models
{
    [Table("RegularAccounts")]
    public class RegularAccount: IAccount
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("initial_amount")]
        public int InitialAmount { get; private set; }
        [Column("balance")]
        public int Balance { get; set; }
        [Ignore]
        public List<Purchase> Purchases => BookerRepository.AccountPurchase.GetPurchases(ID, AccountType.RegularAccount).ToList();
        public RegularAccount() { }
        public RegularAccount(string title, int balance)
        {
            Title = title;
            Balance = InitialAmount = balance;
        }
    }
}
