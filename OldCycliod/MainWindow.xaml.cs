﻿using Backend.Enum;
using Backend.Menager;
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

namespace OldCycliod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //FitMenager fitMenager = new FitMenager();
            //fitMenager.CheckFitValue(EnumFit.H11_h11, 9);
            InputOutputPages.NavigationService.Navigate(new DemensionPage());
        }
    }
}
