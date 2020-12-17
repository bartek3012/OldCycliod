using Backend.Entity;
using Backend.Results;
using Backend.Serivce;
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
    /// Interaction logic for OutputPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage(Calculations calculations, WorkingConditionsPage page)
        {
            InitializeComponent();
            allData = calculations;
            input = calculations.GetInputElements();
            output = calculations.GetOutputElements();
            earlierPage = page;
            SetAllText();
        }
        private readonly Calculations allData;
        private readonly List<DataValue> input;
        private readonly List<DataValue> output;
        private readonly WorkingConditionsPage earlierPage;

        private void SetAllText()
        {
            foreach(DataValue element in input)
            {
                NameIn.Text += element.Content + "\r\n";
                ValueIn.Text += element.Value.ToString() + "\r\n";
                UnitIn.Text += element.Unit + "\r\n";
            }
            NameIn.Text += "Naprężenia dop. na ściskanie kc" + "\r\n";
            ValueIn.Text += allData.SelectedMaterial.Value.ToString() + "\r\n";
            UnitIn.Text += allData.SelectedMaterial.Unit + "\r\n";

            NameIn.Text += allData.SelectedMaterial.Type + "\r\n";
            ValueIn.Text += allData.SelectedMaterial.Content + "\r\n";
            UnitIn.Text +=  "(materiał)"+"\r\n";

            NameIn.Text += allData.SelectedWorkCondition.Content+ "\r\n";
            ValueIn.Text += $"{allData.SelectedWorkCondition.EnWorkCondition.ToString()} ({allData.SelectedWorkCondition.Value}) \r\n";

            foreach (DataValue element in output)
            {
                NameOut.Text += element.Content + "\r\n";
                ValueOut.Text += element.Value.ToString() + "\r\n";
                UnitOut.Text += element.Unit + "\r\n";
            }
            if(allData.AllDataValue.GetValueByEnumName(EnumName.Mout)> allData.AllDataValue.GetValueByEnumName(EnumName.Mmax))
            {
                TextBlockCheckTorque.Text = "Wartość zadanego momentu jest większa od wartości maksymalnej!";
                TextBlockCheckTorque.Foreground = Brushes.DarkRed;
            }
            else
            {
                TextBlockCheckTorque.Text = "Mechanizm wyjściowy jest w stanie przenieść zadany moment obrotowy";
                TextBlockCheckTorque.Foreground = Brushes.DarkGreen;
            }
        }

        private void newCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DemensionPage());
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void pdfButton_Click(object sender, RoutedEventArgs e)
        {
            new Backend.Serivce.PdfService(allData);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            earlierPage.SetNullDataFromFile();
            NavigationService.Navigate(earlierPage);
        }

        private void saveButton_Click_1(object sender, RoutedEventArgs e)
        {
            FileService.SaveFile(earlierPage.GetTextToSave());
            
        }
    }
}
