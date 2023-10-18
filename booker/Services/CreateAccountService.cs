using booker.Models;
using booker.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace booker.Services
{
    public class CreateAccountService
    {
        private const int SEGMENTSMAX = 10;
        private IAccount account;
        public string title;
        public int amount;
        public string typeString;
        public AccountType type;
        public int segmentsNum;
        public int SegmentMax => SEGMENTSMAX;
        public List<AccountStatus> AccountCheck()
        {
            var issues = new List<AccountStatus>();
            if (string.IsNullOrEmpty(title)) issues.Add(AccountStatus.TitleIsNull);
            if (amount < 1) issues.Add(AccountStatus.AmountZeroOrNegative);
            if (typeString is null) issues.Add(AccountStatus.AccountTypeIncorrect);
            else if (AccountTypeHandler.GetTypeTitle(type) != typeString) issues.Add(AccountStatus.AccountTypeIncorrect);
            if (type == AccountType.ComplexAccount)
            {
                if (segmentsNum < 1) issues.Add(AccountStatus.SegmentsZeroOrNegative);
                else if (segmentsNum == 1) issues.Add(AccountStatus.SegmentsOne);
                else if (segmentsNum > SEGMENTSMAX) issues.Add(AccountStatus.TooManySegments);
            }
            return issues;
        }
        public List<AccountStatus> CreateAccount()
        {
            var issues = AccountCheck();
            if (issues.Count != 0) return issues;
            switch (type)
            {
                case AccountType.RegularAccount:
                    account = new RegularAccount(title, amount);
                    break;
                case AccountType.ComplexAccount:
                    account = new ComplexAccount(title, amount, segmentsNum);
                    break;
                default:
                    break;
            }
            BookerRepository.Accounts.SaveAccount(account);
            return null;
        }
    }
    public enum AccountStatus
    {
        TitleIsNull,
        AmountZeroOrNegative,
        AccountTypeIncorrect,
        SegmentsZeroOrNegative,
        SegmentsOne,
        TooManySegments
    }
}
