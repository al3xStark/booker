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

            Label name = new Label();
            name.SetBinding(Label.TextProperty, "Name");
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

            StackLayout stackLayout2 = new StackLayout()
            {
                Children = { name, balance, segments }
            };
            Content = stackLayout2;
        }
    }
}