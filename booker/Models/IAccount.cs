using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    public interface IAccount
    {
        string Title { get; set; }
        int Balance { get; set; }
    }
}
