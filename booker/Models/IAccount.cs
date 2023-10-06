using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    public interface IAccount
    {
        int ID { get; set; }
        string Title { get; set; }
        int Balance { get; set; }
        List<Purchase> Purchases { get; }
    }
}
