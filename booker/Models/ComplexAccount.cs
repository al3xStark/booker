using booker.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace booker.Models
{
    class ComplexAccount: IAccount
    {
        private int balance;
        
        public string Title { get; set; }
        public int Balance
        {
            get => balance;
            set
            {
                balance = value;
                UpdateSegments(Segments.Length);
            }
        }
        public Segment[] Segments { get; set; }
        public ComplexAccount(int balance, int segmentsNum)
        {
            this.balance = balance;
            BuildSegments(segmentsNum);
        }

        private int[] DivideAmount(int segmentsNum, Period[] periods = null)
        {
            int[] result = new int[segmentsNum];
            periods = periods ?? TimePeriod.GetPeriods(segmentsNum);
            int currentPeriod = TimePeriod.GetCurentPeriodNum(periods);
            for (int i = 0; i < currentPeriod; i++)
                    result[i] = 0;

            int segmentsDuration = periods[currentPeriod].Duration;
            int leftFromCurrentSegment = periods[currentPeriod].DaysLeft;
            int daysLeft = (segmentsNum - currentPeriod - 1) * segmentsDuration + leftFromCurrentSegment;

            int remain = Balance % daysLeft;
            int absolute = (Balance - remain) / daysLeft;
            int approximate, r;
            
            for (int i = currentPeriod; i < segmentsNum; i++)
            {
                if (i == currentPeriod)
                    approximate = absolute * leftFromCurrentSegment;
                else
                    approximate = absolute * segmentsDuration;
                r = approximate % 10;
                result[i] = approximate - r;
                remain += r;
            }

            int count = result.Length - 1;
            result[count] += remain % 10;
            remain -= remain % 10;

            while (remain > 0)
            {
                count--;
                if (count < currentPeriod) count = result.Length - 1;
                result[count] += 10;
                remain -= 10;
            }

            return result;
        }

        private void BuildSegments(int segmentsNum)
        {
            Period[] periods = TimePeriod.GetPeriods(segmentsNum);
            int[] amounts = DivideAmount(segmentsNum, periods);
            CheckAmount(amounts);

            Segments = new Segment[segmentsNum];
            for (int i = 0; i < segmentsNum; i++)
                Segments[i] = new Segment(amounts[i], periods[i]);
        }

        private void CheckAmount(int[] amounts) 
        {
            int testAmount = 0;
            foreach (int amount in amounts)
                testAmount += amount;
            if (testAmount != Balance)
            {
                string errorMessage = "Ошибка распределения баланса по сегментам.\n" +
                $" Заданный баланс: {Balance}\n Сумма значений сегментов: {testAmount}";
                Logger.CreateLog(new Exception(errorMessage), ExceptionTag.Error);
            }
        }

        private void UpdateSegments(int segmentsNum)
        {
            int[] amounts = DivideAmount(segmentsNum);
            CheckAmount(amounts);

            for (int i = 0; i < segmentsNum; i++)
                Segments[i].Amount = amounts[i];
        }
    }
}
