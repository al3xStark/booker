using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using booker.Views;
using booker.Services;
using System.IO;

namespace booker
{
    public partial class App : Application
    {
        private static BookerRepository database;
        public static BookerRepository DataBase
        {
            get
            {
                if (database == null)
                    database = BookerRepository.Instance;
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Page1());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
