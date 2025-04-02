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

namespace Playtech.Commands
{
    /// <summary>
    /// Interaction logic for CommandsUserControl.xaml
    /// </summary>
    public partial class CommandsUserControl: UserControl
    {
        private CommandsViewModel ViewModel { get; set; }
        public CommandsUserControl()
        {
            InitializeComponent();
            ViewModel = new CommandsViewModel();
            DataContext = ViewModel;
        }
    }
}
