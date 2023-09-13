using booker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booker.ViewModels
{
    public interface IAccountViewModel : INotifyPropertyChanged
    {
        //IAccount Account { get; set; }
        string Title { get; set; }
        int Balance { get; set; }
    }
}
