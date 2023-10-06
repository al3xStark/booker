using booker.Models;
using booker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace booker.ViewModels
{
    public class SegmentViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Segment segment;
        public SegmentViewModel(int account_id, int amount, Period period) 
        {
            segment = new Segment(account_id, amount, period);
        }
        public SegmentViewModel(Segment segment) 
        {
            this.segment = segment;
        }
        public int Amount
        {
            get => segment.Amount;
            set
            {
                segment.Amount = value;
                OnPropertyChanged("Amount");
            }
        }
        public List<Purchase> Purchases => segment.Purchases;

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
