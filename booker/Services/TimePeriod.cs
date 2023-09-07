using System;

namespace booker.Services
{
    static class TimePeriod
    {
        public static DateTime Start => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        public static DateTime End => new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
        public static int DaysAmount => DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        public static Period[] GetPeriods(int segmentNum)
        {
            Period[] periods = new Period[segmentNum];
            DateTime start = Start, end;
            int approximate = DaysAmount / segmentNum;
            int remain = DaysAmount % segmentNum;

            for (int i = 0; i < segmentNum; i++)
            {
                if (remain > 0)
                {
                    end = start.AddDays(approximate);
                    remain--;
                }
                else end = start.AddDays(approximate - 1);
                periods[i] = new Period(start, end);
                start = end.AddDays(1);
            }
            return periods;
        }
        public static int GetCurentPeriodNum(Period[] periods)
        {
            int curentPeriodNum = 0;
            foreach (Period period in periods)
            {
                if (period.End.Date >= DateTime.Now.Date) break;
                curentPeriodNum++;
            }
            return curentPeriodNum;
        }
        public static Period GetCurentPeriod(Period[] periods) 
            => periods[GetCurentPeriodNum(periods)];

    }

    struct Period
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Period(DateTime start, DateTime end) { Start = start; End = end;}
        public int Duration => (End - Start).Days + 1;
        public int DaysLeft => (End - DateTime.Now.Date).Days + 1;
    }
}
