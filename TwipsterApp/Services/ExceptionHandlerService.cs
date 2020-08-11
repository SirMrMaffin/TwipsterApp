using System;
using System.Windows;

namespace TwipsterApp.Services
{
    public static class ExceptionHandlerService
    {
        public static void Explain(Exception x)
        {
            MessageBox.Show(x.Message + "\n Check provided information.");
        }
    }
}
