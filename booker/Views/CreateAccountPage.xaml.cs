using booker.Services;
using booker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace booker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        private CreateAccountViewModel viewModel;
        public CreateAccountPage()
        {
            InitializeComponent();
            viewModel = new CreateAccountViewModel();
            BindingContext = viewModel;
        }
        void AccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            accountTypeErrorMessage.IsVisible = false;
            segmentsNumErrorMessage.IsVisible = false;

            if ((string)accountType.SelectedItem == AccountTypeHandler.GetTypeTitle(AccountType.ComplexAccount))
                segmentsNum.IsVisible = true;
            else segmentsNum.IsVisible = false;
            viewModel.Type = (string)accountType.SelectedItem;
        }
        void SegmentsNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            segmentsNumErrorMessage.IsVisible = false;
        }
        void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            amountErrorMessage.IsVisible = false;
        }
        void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            titleErrorMessage.IsVisible = false;
        }
        void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            ClearErrorLabels();
            List<AccountStatus> issues = viewModel.OnConfirmButtonClicked();
            if (issues.Count != 0)
            {
                foreach (AccountStatus issue in issues)
                    ShowErrorLabel(issue);
            }
        }
        void ShowErrorLabel(AccountStatus issue)
        {
            var errorLabel = GetErrorLabel(issue);
            if (errorLabel != null)
            {
                errorLabel.Text = viewModel.GetAccountIssue(issue);
                errorLabel.IsVisible = true;
            }
        }
        Label GetErrorLabel(AccountStatus issue)
        {
            switch (issue)
            {
                case AccountStatus.TitleIsNull: return titleErrorMessage;
                case AccountStatus.AmountZeroOrNegative: return amountErrorMessage;
                case AccountStatus.AccountTypeIncorrect: return accountTypeErrorMessage;
                case AccountStatus.SegmentsZeroOrNegative: return segmentsNumErrorMessage;
                case AccountStatus.SegmentsOne: return segmentsNumErrorMessage;
                case AccountStatus.TooManySegments: return segmentsNumErrorMessage;
                default: return null;
            }
        }
        void ClearErrorLabels()
        {
            titleErrorMessage.IsVisible = false;
            amountErrorMessage.IsVisible = false;
            accountTypeErrorMessage.IsVisible = false;
            segmentsNumErrorMessage.IsVisible = false;
        }
    }
}