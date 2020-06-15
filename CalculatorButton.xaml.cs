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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CalculatorButton : UserControl
    {
        public delegate void buttonPressHandler(string output);
        public static event buttonPressHandler buttonPressed;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string buttonValue
        {
            get { return (string)GetValue(buttonValueProperty); }
            set { SetValue(buttonValueProperty, value); }
        }
        public static DependencyProperty buttonValueProperty = DependencyProperty.Register("buttonValue", typeof(string), typeof(CalculatorButton), new PropertyMetadata(new PropertyChangedCallback(buttonValuePropertyChanged)));

        private static void buttonValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CalculatorButton CB = (CalculatorButton)d;
            CB.Text.Content = CB.buttonValue;
        }

        public CalculatorButton()
        {
            InitializeComponent();
        }

        private void CalculatorButtonClicked(object sender, RoutedEventArgs e)
        {
            buttonPressed(buttonValue);
        }
    }
}
