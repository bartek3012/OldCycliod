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
            materialListBox.ItemsSource = materials.Elements;
            textBoxMat = textBoxMaterial;
            selectedMat = new BaseEntity();
        }
        private MaterialsMenager materials;
        public BaseEntity selectedMat { get; set; }
        private TextBox textBoxMat;
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow(this);
            addMaterialWindow.Show();
        }

        private void Button_Click_Select(object sender, RoutedEventArgs e)
        {
            select();
        }

        private void MaterialListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            select();
        }

        private void select()
        {
            selectedMat = (materialListBox.SelectedItem as BaseEntity);
            if(selectedMat != null)
            {
                textBoxMat.Text = selectedMat.Content;
            }
            this.Close();
        }
        public void AddMaterialWindow(string type, string name, double presure)
        {
            BaseEntity newMaterial = new BaseEntity(name, type, presure);
            materials.AddNewMaterial(newMaterial);
            materialListBox.Items.Refresh();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            BaseEntity selectedMaterial = (materialListBox.SelectedItem as BaseEntity);
            if (selectedMaterial != null)
            {
                materials.DeleteMaterial(selectedMaterial);
                materialListBox.Items.Refresh();

            }
        }
    }
}
