using booker.Models;
using booker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace booker.ViewModels
{
    public class CreateAccountViewModel : INotifyPropertyChanged
    {
        private CreateAccountService service;
        public event PropertyChangedEventHandler PropertyChanged;
        public CreateAccountViewModel() 
        {
            service = new CreateAccountService();
        }
        public string Title 
        { 
            get => service.title; 
            set
            {
                if (service.title != value)
                {
                    service.title = value;
                    OnPropertyChanged("Title");
                }
            } 
        }
        public string Amount
        {
            get
            {
                if (service.amount == 0) return null;
                return service.amount.ToString();
            }
            set
            {
                if (int.TryParse(value, out int newValue))
                    if (service.amount != newValue)
                    {
                        service.amount = newValue;
                        OnPropertyChanged("Amount");
                    }
            }
        }
        public List<string> AccountTypes => AccountTypeHandler.GetTypeTitles();
        public string Type 
        {
            set
            {
                service.typeString = value;
                AccountType newType = AccountTypeHandler.GetType(value);
                if (service.type != newType)
                    service.type = newType;
            }
        }
        public string SegmentsNum
        {
            get
            {
                if (service.segmentsNum == 0) return null;
                return service.segmentsNum.ToString();
            }
            set
            {
                if (int.TryParse(value, out int newValue))
                    if (service.segmentsNum != newValue)
                    {
                        service.segmentsNum = newValue;
                        OnPropertyChanged("SegmentsNum");
                    }
            }
        }
        public string GetAccountIssue(AccountStatus status)
        {
            switch (status)
            {
                case AccountStatus.TitleIsNull:
                    return "Название не может быть пустым";
                case AccountStatus.AmountZeroOrNegative:
                    return "Сумма должна быть положительной";
                case AccountStatus.AccountTypeIncorrect:
                    return "Ошибка определения аккаунта. Повторите выбор";
                case AccountStatus.SegmentsZeroOrNegative:
                    return "Количество сегментов должно быть положительным";
                case AccountStatus.SegmentsOne:
                    return "Недостаточно сегментов. " +
                        $"Выберите \"{AccountTypeHandler.GetTypeTitle(AccountType.RegularAccount)}\", если вам необходим один сегмент";
                case AccountStatus.TooManySegments:
                    return $"Превышение допустимого количества сегментов. Максимальное допустимое значение = {service.SegmentMax}";
                default: return null;
            }
        }
        public List<AccountStatus> OnConfirmButtonClicked()
        {
            return service.CreateAccount();
        }
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
