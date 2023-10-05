using booker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    [Table("Accounts")]
    public class Account
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("type")]
        public AccountType Type { get; set; }
        [Column("account_id")]
        public int AccountID { get; set; }
        public Account() { }
        public Account(AccountType type, int accountID) 
        {
            Type = type;
            AccountID = accountID;
        }
    }
}
