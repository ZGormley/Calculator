using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    public class StartUp : Application
    {
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main()
        {
            StartUp app = new StartUp();
            app.InitializeComponent();
            app.Run();
        }

        public void InitializeComponent()
        {
            this.StartupUri = new Uri("Calculator.xaml", System.UriKind.Relative);
        }
    }
}
