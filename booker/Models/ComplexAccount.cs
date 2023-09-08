using booker.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace booker.Models
{
    class ComplexAccount: Account
    {
        public Segment[] Segments { get; set; }

        public ComplexAccount(string name, int amount, int segmentsNum = 4) : base(name, amount)
        {
            BuildSegments(segmentsNum);
        }

        private int[] DivideAmount(int segmentNum, Period[] periods = null)
        {
            int[] result = new int[segmentNum];
            periods = periods == null ? TimePeriod.GetPeriods(segmentNum) : periods;
            int currentPeriod = TimePeriod.GetCurentPeriodNum(periods);
            for (int i = 0; i < currentPeriod; i++)
                    result[i] = 0;

            int segmentsDuration = periods[currentPeriod].Duration;
            int leftFromCurrentSegment = periods[currentPeriod].DaysLeft;
            int daysLeft = (segmentNum - currentPeriod - 1) * segmentsDuration + leftFromCurrentSegment;

            int remain = Amount % daysLeft;
            int absolute = (Amount - remain) / daysLeft;
            int approximate, r;
            
            for (int i = currentPeriod; i < segmentNum; i++)
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
                if (count < 0) count = result.Length - 1;
                result[count] += 10;
                remain -= 10;
            }

            return result;
        }

        private void BuildSegments(int segmentNum)
        {
            Segments = new Segment[segmentNum];
            Period[] periods = TimePeriod.GetPeriods(segmentNum);
            int[] amounts = DivideAmount(segmentNum, periods);
            CheckAmount(amounts);

            for (int i = 0; i < segmentNum; i++)
                Segments[i] = new Segment(amounts[i], periods[i]);
        }

        private void CheckAmount(int[] amounts) 
        {
            int testAmount = 0;
            foreach (int amount in amounts)
                testAmount += amount;
            if (testAmount != Amount)
            {
                string errorMessage = "Ошибка распределения баланса по сегментам.\n" +
                $" Заданный баланс: {Amount}\n Сумма значений сегментов: {testAmount}";
                Logger.CreateLog(new Exception(errorMessage), ExceptionTag.Error);
            }
        }
    }
}
