using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Playtech.Results
{
    public partial class SquareResult: UserControl
    {
        public SquareResult()
        {
            InitializeComponent();
        }

        public string Position
        {
            get { return (string)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(string), typeof(SquareResult), new PropertyMetadata(string.Empty));

        public Brush ColorBrush
        {
            get { return (Brush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }

        public static readonly DependencyProperty ColorBrushProperty =
            DependencyProperty.Register("ColorBrush", typeof(Brush), typeof(SquareResult), new PropertyMetadata(Brushes.Gray));
    }
}