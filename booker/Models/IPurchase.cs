using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    internal interface IPurchase
    {
        int ID { get; set; }
        string Title { get; set; }
        int Value { get; set; }
        DateTime Created { get; set; }
    }
}
