using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Made a static instance to easily access the outputtextbox from calculatorlogic in the future I would go back to rework the databinding to make this uneccessary
        public static MainWindow instance;

        //More usercontrols for individual calculator buttons could be created easily and extended without modifying the rest of the program
        public MainWindow()
        {
            InitializeComponent();
            instance = this; 
        }

        public void updateOutput(string output)
        {
            Output.outputText = output;
        }
    }
}
