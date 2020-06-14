using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TextInput : UserControl
    {
        public string inputText
        {
            get { return (string)GetValue(inputTextProperty); }
            set { SetValue(inputTextProperty, value); }
        }
        private string oldInputText = "";

        public DependencyProperty inputTextProperty = DependencyProperty.Register("inputText", typeof(string), typeof(TextInput));

        public TextInput()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CheckEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                updateInputText();

            }
        }

        //Split this from check enter to allow it to be called from a future = calculator button easily also
        private void updateInputText()
        {
            if (inputText != oldInputText)
            {
                oldInputText = inputText;
                CalculatorLogic.Instance.updateInputText(inputText);
            }
        }
    }
}
