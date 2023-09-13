using booker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static Xamarin.Essentials.Permissions;

namespace booker.ViewModels
{
    public class ComplexAcccountViewModel : IAccountViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IAccount complexAccount;

        public ComplexAcccountViewModel()
        {
            complexAccount = new ComplexAccount();
        }
        public string Title
        {
            get => complexAccount.Title;
            set
            {
                if (complexAccount.Title != value)
                {
                    complexAccount.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public int Balance
        {
            get => complexAccount.Balance;
            set
            {
                if (complexAccount.Balance != value)
                {
                    complexAccount.Balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }
        public Segment[] Segments => ((ComplexAccount)complexAccount).Segments;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
