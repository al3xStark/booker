﻿using booker.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Models
{
    class Segment
    {
        public readonly int initialAmount;
        public readonly Period period;
        public int Amount { get; set; }

        public Segment(int amount, Period period)
        {
            initialAmount = Amount = amount;
            this.period = period;
        }
    }
}
