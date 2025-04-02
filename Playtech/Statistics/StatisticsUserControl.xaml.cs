﻿using System;
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

namespace Playtech.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsUserControl.xaml
    /// </summary>
    public partial class StatisticsUserControl: UserControl
    {
        private StatisticsViewModel StatisticsViewModel { get; }
        public StatisticsUserControl()
        {
            InitializeComponent();
            StatisticsViewModel = new StatisticsViewModel(new StatisticsModel(4948));
            DataContext = StatisticsViewModel;
        }
    }
}
