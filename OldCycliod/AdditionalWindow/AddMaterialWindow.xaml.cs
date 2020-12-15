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
using System.Windows.Shapes;

namespace OldCycliod
{
    /// <summary>
    /// Interaction logic for AddMaterialWindow.xaml
    /// </summary>
    public partial class AddMaterialWindow : Window
    {
        private readonly ListMaterialWindow listWindow;
        public AddMaterialWindow(ListMaterialWindow listMaterialWindow)
        {
            InitializeComponent();
            listWindow = listMaterialWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = textBoxNewType.Text;
            string name = textBoxNewName.Text;
            string presureString = textBoxPresure.Text;
            if (Double.TryParse(presureString, out double presure) == false)
            {
                textBlockPresureError.Background = Brushes.LightPink;
                textBlockPresureError.Text = "Nie podano poprawnie liczby";
            }
            else
            {
                listWindow.AddMaterialWindow(type, name, presure);
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
