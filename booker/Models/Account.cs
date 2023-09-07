using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    public class Account
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public Account(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}
