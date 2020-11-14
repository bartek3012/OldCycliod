using Frontend;
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
    /// Interaction logic for MaterialWindow.xaml
    /// </summary>
    public partial class ListMaterialWindow : Window
    {
        public ListMaterialWindow()
        {
            InitializeComponent();
        }
        public MaterialsMenager<BaseEntity> Materials;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow();
            addMaterialWindow.Show();
            Materials = new MaterialsMenager();
        }
    }
}
