using System;
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
    /// Interaction logic for InputPage.xaml
    /// </summary>
    public partial class InputPage : Page
    {
        public InputPage()
        {
            InitializeComponent();
        }

        private void SetMaterialButtonClick(object sender, RoutedEventArgs e)
        {
           MaterialWindow secondWindow = new MaterialWindow();
            secondWindow.Show();
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OutputPage());
        }
    }
}
