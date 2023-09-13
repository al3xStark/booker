using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    class RegularAccount: IAccount
    {
        public readonly int initialAmount;
        public string Title { get; set; }
        public int Balance { get; set; }
        public RegularAccount()
        {            
            initialAmount = Balance;
        }
    }
}
