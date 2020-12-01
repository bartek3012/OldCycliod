using Backend.Entity;
using Backend.Results;
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
        public ResultPage(Calculations calculations)
        {
            InitializeComponent();
            allData = calculations;
            input = calculations.GetInputElements();
            output = calculations.GetOutputElements();
            SetAllText();
        }
        private Calculations allData;
        private List<DataValue> input;
        private List<DataValue> output;

        private void SetAllText()
        {
            foreach(DataValue element in input)
            {
                NameIn.Text += element.Content + "\r\n";
                ValueIn.Text += element.Value.ToString() + "\r\n";
                UnitIn.Text += element.Unit + "\r\n";
            }
            NameIn.Text += "Naprężenia dop na ściskanie kc" + "\r\n";
            ValueIn.Text += allData.selectedMaterial.Value.ToString() + "\r\n";
            UnitIn.Text += allData.selectedMaterial.Unit + "\r\n";

            NameIn.Text += allData.selectedMaterial.Type + "\r\n";
            ValueIn.Text += allData.selectedMaterial.Content + "\r\n";
            UnitIn.Text +=  "(materiał)"+"\r\n";

            NameIn.Text += allData.selectedWorkCondition.Content+ "\r\n";
            ValueIn.Text += $"{allData.selectedWorkCondition.EnWorkCondition.ToString()} ({allData.selectedWorkCondition.Value}) \r\n";

            foreach (DataValue element in output)
            {
                NameOut.Text += element.Content + "\r\n";
                ValueOut.Text += element.Value.ToString() + "\r\n";
                UnitOut.Text += element.Unit + "\r\n";
            }
            if(allData.allDataValue.GetValueByEnumName(EnumName.Mz)> allData.allDataValue.GetValueByEnumName(EnumName.Mmax))
            {
                TextBlockCheckTorque.Text = "Wartość zadanego momentu jest większa od wartości maksymalnej!";
                TextBlockCheckTorque.Foreground = Brushes.DarkRed;
            }
            else
            {
                TextBlockCheckTorque.Text = "Przekładnia jest w stanie przenieść zadany moment obrotowy";
                TextBlockCheckTorque.Foreground = Brushes.DarkGreen;
            }
        }

        private void NameIn_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void newCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DemensionPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void pdfButton_Click(object sender, RoutedEventArgs e)
        {
            new Backend.Serivce.PdfService(allData);
        }
    }
}
