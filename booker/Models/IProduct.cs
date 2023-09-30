using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    public interface IProduct
    {
        int ID { get; set; }
        string Title { get; set; }
        int Cost { get; set; }
        DateTime Expiration { get; set; }
    }
}
