using booker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booker.Models
{
    [Table("Segments")]
    public class Segment
    {
        public readonly int initialAmount;
        public readonly Period period;
        [Column("account_id")]
        public int AccountID { get; set; }
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("Amount")]
        public int Amount { get; set; }
        public Segment(int account_id, int amount, Period period)
        {
            AccountID = account_id;
            initialAmount = Amount = amount;
            this.period = period;
        }
    }
}
