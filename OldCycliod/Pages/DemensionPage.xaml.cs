﻿using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for InputMaterialPage.xaml
    /// </summary>
    public partial class DemensionPage : Page
    {
        public DemensionPage()
        {
            InitializeComponent();
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkingConditionsPage());
        }
    }
}