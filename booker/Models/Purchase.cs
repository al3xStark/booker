using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    [Table("Purchases")]
    public class Purchase
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; }
        [Column("title")]
        public string Title { get; set; }
        [Column("value")]
        public int Value { get; set; }
        [Column("created_at")]
        public DateTime Created { get; set; }
    }
}
