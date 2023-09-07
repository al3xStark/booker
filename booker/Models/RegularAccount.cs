using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    class RegularAccount: Account
    {
        public readonly int initialAmount;
        public RegularAccount(string name, int amount) : base(name, amount)
        {            
            initialAmount = Amount;
        }
    }
}
