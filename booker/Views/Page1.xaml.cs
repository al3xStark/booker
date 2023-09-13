using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using booker.Models;
using booker.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace booker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            var account = new ComplexAcccountViewModel
            {
                Title = "Счет",
                Balance = 5397
            };
            BindingContext = account;

            #region _VIEW_
            //Label balance = new Label();
            //balance.SetBinding(Label.TextProperty, "Balance");

            //ListView segments = new ListView()
            //{
            //    ItemsSource = account.Segments,
            //    ItemTemplate = new DataTemplate(() =>
            //    {
            //        Label segBalance = new Label();
            //        segBalance.SetBinding(Label.TextProperty, "Amount");

            //        return new ViewCell { View = segBalance };
            //    })
            //};

            //Button logPageButton = new Button() { Text = "Журнал" };
            //logPageButton.Clicked += LogButtonClicked;

            //StackLayout stackLayout = new StackLayout()
            //{
            //    Children = { balance, segments, logPageButton }
            //};
            //Content = stackLayout;
            #endregion
        }

        private async void LogButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogPage());
        }
    }
}