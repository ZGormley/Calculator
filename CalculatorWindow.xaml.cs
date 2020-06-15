using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string outputText
        {
            get { return (string)GetValue(outputTextProperty); }
            set { SetValue(outputTextProperty, value); }
        }
        public DependencyProperty outputTextProperty = DependencyProperty.Register("outputText", typeof(string), typeof(CalculatorWindow));

        public string inputText
        {
            get { return (string)GetValue(inputTextProperty); }
            set { SetValue(inputTextProperty, value); }
        }
        public DependencyProperty inputTextProperty = DependencyProperty.Register("inputText", typeof(string), typeof(CalculatorWindow), new PropertyMetadata(new PropertyChangedCallback(inputTextPropertyChanged)));

        private static void inputTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CalculatorLogic Calc = ((CalculatorWindow)d).CalcLogic;
            Calc.inputText = (string)e.NewValue;
            Calc.Calculate();
        }

        private CalculatorLogic CalcLogic = new CalculatorLogic();
        //More usercontrols for individual calculator buttons could be created easily and extended without modifying the rest of the program
        public CalculatorWindow()
        {
            InitializeComponent();
            DataContext = this;
            CalcLogic.updateOutput += OutputUpdated;
            CalculatorButton.buttonPressed += ButtonInput;
        }

        private void OutputUpdated(string output)
        {
            outputText = output;
        }

        private void ButtonInput(string input)
        {
            inputText += input;
        }

        private void EraseClick(object sender, RoutedEventArgs e)
        {
            inputText = "";
        }
        private void BackspaceClick(object sender, RoutedEventArgs e)
        {
            if(inputText.Length > 0)
                inputText = inputText.Remove(inputText.Length - 1);
        }
    }
    
}
