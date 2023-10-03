using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    [Table("RegularAccounts")]
    class RegularAccount: IAccount
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("initial_amount")]
        public int InitialAmount { get; private set; }
        [Column("balance")]
        public int Balance { get; set; }
        public RegularAccount()
        {            
            
        }
    }
}
