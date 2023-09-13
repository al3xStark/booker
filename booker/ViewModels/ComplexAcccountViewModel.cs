using booker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Xamarin.Essentials.Permissions;

namespace booker.ViewModels
{
    public class ComplexAcccountViewModel : AccountViewModel
    {
        public ComplexAcccountViewModel() : base(new ComplexAccount()) { }
        public Segment[] Segments => ((ComplexAccount)account).Segments;
    }
}
