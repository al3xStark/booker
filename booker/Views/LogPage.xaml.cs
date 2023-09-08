using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace booker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogPage : ContentPage
    {
        public LogPage()
        {
            InitializeComponent();
            Title = "Журнал";
            Editor logsEditor = new Editor()
            {
                Text = GetLogs(),
                IsReadOnly = true
            };
            Content = logsEditor;
        }
        private string GetLogs()
        {
            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/logs.txt";
            if (File.Exists(logFilePath))
                return File.ReadAllText(logFilePath);
            else return "Журнал не обнаружен.";
        }

    }
}