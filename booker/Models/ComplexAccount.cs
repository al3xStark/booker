using booker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace booker.Models
{
    [Table("ComplexAccounts")]
    public class ComplexAccount: IAccount
    {
        private int balance;
        private Segment[] segments;
        [PrimaryKey, AutoIncrement, Column("id")]
        public int ID { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("balance")]
        public int Balance
        {
            get => balance;
            set
            {
                balance = value;
                if (segments != null)
                {
                    UpdateSegments(segments.Length);
                    foreach (Segment segment in segments)
                        SegmentRepository.SaveSegment(segment);
                }
            }
        }
        [Ignore]
        public List<Purchase> Purchases => BookerRepository.AccountPurchase.GetPurchases(ID, AccountType.ComplexAccount).ToList();
        [Ignore]
        public Segment[] Segments 
        { 
            get
            {
                if (segments is null)
                    segments = SegmentRepository.GetAccountSegments(ID).ToArray();
                return segments;
            }
            set => segments = value;
        }
        public ComplexAccount(string title, int balance, int segmentsNum)
        {
            Title = title;
            this.balance = balance;
            BuildSegments(segmentsNum);
            foreach (Segment segment in segments)
                SegmentRepository.SaveSegment(segment);
        }
        public ComplexAccount() { }

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

            segments = new Segment[segmentsNum];
            for (int i = 0; i < segmentsNum; i++)
                segments[i] = new Segment(ID, amounts[i], periods[i]);
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
                segments[i].Amount = amounts[i];
        }
    }
}
