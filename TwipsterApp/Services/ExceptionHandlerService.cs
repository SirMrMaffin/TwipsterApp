using System;
using System.Windows;

namespace TwipsterApp.Services
{
    public class ExceptionHandlerService
    {
        public void Explain(Exception x)
        {
            MessageBox.Show(x.Message + "\n Check provided information.");
        }
    }
}
