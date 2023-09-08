using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using booker.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace booker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private Account account;
        public Page1()
        {
            InitializeComponent();
            account = new ComplexAccount("Cчет", 5967);
            BindingContext = account;

            Title = account.Name;
            Label balance = new Label();
            balance.SetBinding(Label.TextProperty, "Amount");
            ListView segments = new ListView()
            {
                ItemsSource = ((ComplexAccount)account).Segments,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label segBalance = new Label();
                    segBalance.SetBinding(Label.TextProperty, "Amount");

                    return new ViewCell { View = segBalance };
                })
            };
            Button logPageButton = new Button() { Text = "Журнал" };
            logPageButton.Clicked += LogButtonClicked;

            StackLayout stackLayout = new StackLayout()
            {
                Children = { balance, segments, logPageButton }
            };
            Content = stackLayout;
        }

        private async void LogButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogPage());
        }
    }
}