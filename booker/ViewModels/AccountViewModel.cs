using booker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booker.ViewModels
{
    public abstract class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected IAccount account;
        public AccountViewModel(IAccount account) => this.account = account;
        public string Title
        {
            get => account.Title;
            set
            {
                if (account.Title != value)
                {
                    account.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public int Balance
        {
            get => account.Balance;
            set
            {
                if (account.Balance != value)
                {
                    account.Balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
