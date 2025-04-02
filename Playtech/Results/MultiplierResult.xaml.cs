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

namespace Playtech.Results
{
    /// <summary>
    /// Interaction logic for MultiplierResult.xaml
    /// </summary>
    public partial class MultiplierResult: UserControl
    {
        public MultiplierResult()
        {
            InitializeComponent();
        }

        public string Multiplier
        {
            get { return (string)GetValue(MultiplierProperty); }
            set { SetValue(MultiplierProperty, value); }
        }

        public static readonly DependencyProperty MultiplierProperty =
            DependencyProperty.Register("Multiplier", typeof(string), typeof(MultiplierResult), new PropertyMetadata(string.Empty));

        public Brush ColorBrush
        {
            get { return (Brush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }

        public static readonly DependencyProperty ColorBrushProperty =
            DependencyProperty.Register("ColorBrush", typeof(Brush), typeof(MultiplierResult), new PropertyMetadata(Brushes.Gray));

    }
}
