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
    /// wpf class for outputting the calculated value to a textbox
    /// </summary>
    public partial class TextOutput : UserControl
    {
        public string outputText
        {
            get { return (string)GetValue(outputTextProperty); }
            set { SetValue(outputTextProperty, value);  }
        }

        public DependencyProperty outputTextProperty = DependencyProperty.Register("outputText", typeof(string), typeof(TextOutput));

        public TextOutput()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
