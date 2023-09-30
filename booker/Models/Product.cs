using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    [Table("Purchases")]
    public class Product
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("cost")]
        public int Cost { get; set; }
        [Column("expiration")]
        public DateTime Expiration { get; set; }
    }
}
