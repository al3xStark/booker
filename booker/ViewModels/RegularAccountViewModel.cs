using booker.Models;
using booker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace booker.ViewModels
{
    public class RegularAccountViewModel : AccountViewModel
    {
        public RegularAccountViewModel(RegularAccount account) : base(account)
        {
        }
    }
}
