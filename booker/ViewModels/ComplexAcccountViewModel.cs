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
        public ComplexAcccountViewModel(string title, int balance, int segmentNum) : base(new ComplexAccount(title, balance, segmentNum)) 
        {
            Segment[] segments = ((ComplexAccount)account).Segments;
            Segments = new SegmentViewModel[segments.Length];
            for (int i = 0; i < segments.Length; i++)
            {
                Segments[i] = new SegmentViewModel(segments[i]);
            }
        }
        public SegmentViewModel[] Segments { get; private set; }
        public new int Balance
        {
            get => account.Balance;
            set
            {
                if (account.Balance != value)
                {
                    account.Balance = value;
                    OnPropertyChanged("Balance");
                    foreach (var segment in Segments)
                        segment.OnPropertyChanged("Amount");
                }
            }
        }
    }
}
