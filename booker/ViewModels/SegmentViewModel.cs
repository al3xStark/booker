using booker.Models;
using booker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booker.ViewModels
{
    public class SegmentViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Segment segment;
        public SegmentViewModel(int amount, Period period) 
        {
            segment = new Segment(amount, period);
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

        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
