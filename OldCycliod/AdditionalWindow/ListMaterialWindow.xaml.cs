using Backend;
using Backend.Menager;
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
using OldCycliod.Pages;

namespace OldCycliod
{
    /// <summary>
    /// Interaction logic for MaterialWindow.xaml
    /// </summary>
    public partial class ListMaterialWindow : Window
    {
        public ListMaterialWindow(TextBox textBoxMaterial)
        {
            InitializeComponent();
            materials = new MaterialsMenager();
            //Materials.Elements.ItemSource = new List<BaseEntity>();
            MaterialListBox.ItemsSource = materials.Elements;
            textBoxMat = textBoxMaterial;
        }
        private MaterialsMenager materials;
        private TextBox textBoxMat;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow();
            addMaterialWindow.Show();
        }

        private void Button_Click_Select(object sender, RoutedEventArgs e)
        {
            selectMaterial();
        }

        private void selectMaterial()
        {
            BaseEntity Material = (MaterialListBox.SelectedItem as BaseEntity);
            textBoxMat.Text = Material.Content;
        }
    }
}
