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

namespace Playtech.Notification
{
    /// <summary>
    /// Interaction logic for NotificationUserControl.xaml
    /// </summary>
    public partial class NotificationUserControl: UserControl
    {
        private NotificationViewModel ViewModel { get; set; }
        public NotificationUserControl()
        {
            InitializeComponent();
            ViewModel = new NotificationViewModel();
            DataContext = ViewModel;
        }
    }
}
