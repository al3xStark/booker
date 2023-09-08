using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace booker.Services
{
    static public class Logger
    {
        static private string LogInfo(Exception e) => e.Message +
                "\nОбъект: " + e.Source +
                "\nМетод, вызвавший исключение: " + e.TargetSite +
                "\nСтэк: " + e.StackTrace +
                "\n====================================";

        static public void CreateLog(Exception e, ExceptionTag tag)
        {
            string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/logs.txt";
            StreamWriter stream = new StreamWriter(logFilePath, true);
            stream.WriteLine(string.Format(DateTime.Now.ToString()) + "\n");
            string type = string.Empty;
            switch (tag)
            {
                case ExceptionTag.Error:
                    type = "Error: ";
                    break;
                case ExceptionTag.Warning:
                    type = "Warning: ";
                    break;
                case ExceptionTag.Info:
                    type = "Info: ";
                    break;
                default:
                    break;
            }
            stream.WriteLine(type + LogInfo(e));
            stream.Close();
        }
    }
    public enum ExceptionTag
    {
        Error,
        Warning,
        Info
    }
}
