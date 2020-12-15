using Backend;
using Backend.Entity;
using Backend.Menager;
using Backend.Results;
using Backend.Serivce;
using CheckValues;
using OldCycliod.Pages.Service;
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
    public partial class WorkingConditionsPage : Page
    {
        public WorkingConditionsPage(DemensionPage _demensionPage)
        {
            InitializeComponent();
            demensionPage = _demensionPage;
            checkValueElements = demensionPage.checkValueElements;
            Inicjalize();
            selectedMaterial = new BaseEntity();
        }
        private readonly DemensionPage demensionPage;
        private CheckBoxService checkBoxKService;
        private CheckBoxService checkBoxFrictionService;
        private WorkConditionService conditionService;
        private readonly CheckValue[] checkValueElements;
        private BaseEntity selectedMaterial;
        private ListMaterialWindow secondWindow;//
        private TextBox[] allTextBox;
        private bool isMaterialFromFile = false;
        private void Inicjalize()
        {
            checkValueElements[(int)EnumName.Min] = new CheckValue(textBlockMinError, textBoxMin, 100000, 0.1);
            checkValueElements[(int)EnumName.Mout] = new CheckValue(textBlockMoutError, textBoxMout, 100000, 0.1);
            checkValueElements[(int)EnumName.nIn] = new CheckValue(textBlockNinError, textBoxNin, 100000, 0.1);
            checkValueElements[(int)EnumName.nOut] = new CheckValue(textBlockNoutError, textBoxNout, 100000, 0.1);
            checkValueElements[(int)EnumName.friction] = new CheckValue(textBlockFrictionError, textBoxFriction, 0.99, 0.001);
            checkValueElements[(int)EnumName.k] = new CheckValue(textBlockKError, textBoxK, 1000, 0.001);
            checkBoxKService = new CheckBoxService(checkBoxK, textBoxK);
            checkBoxFrictionService = new CheckBoxService(checkBoxFriction, textBoxFriction);

            Button[] ConditionButtons = new Button[] {LekkieButton, SrednieButton, CiezkieButton};
            conditionService = new WorkConditionService(textBlockWorkCaseError, ConditionButtons);

            allTextBox = new TextBox[] { textBoxMin, textBoxMout, textBoxNin, textBoxNout, textBoxFriction, textBoxK, textBoxMat };

        }

        private void SetDataFromFile()
        {
            int i = (int)EnumName.Min + 1;
            foreach (TextBox textBox in allTextBox)
            {
                textBox.Text = demensionPage.dataFromFile[i];
                i++;
            }
            int lastElement = demensionPage.dataFromFile.Length - 1;
            EnumWorkCondition workCondition;
            if(Enum.TryParse(demensionPage.dataFromFile[lastElement], out workCondition) == true)
            {
                conditionService.Click(workCondition);
            }
            string materialName = demensionPage.dataFromFile[lastElement - 1];
            if(materialName != null && materialName != "")
            {
                MaterialsMenager materialsMenager = new MaterialsMenager();
                selectedMaterial = materialsMenager.GetMaterialByName(materialName);
                if(selectedMaterial == null)
                {
                    isMaterialFromFile = false;
                    textBoxMat.Text = "";
                }
                isMaterialFromFile = true;
            }
        }

        private void SetMaterialButtonClick(object sender, RoutedEventArgs e)
        {
            secondWindow = new ListMaterialWindow(textBoxMat);
            secondWindow.Show();
            selectedMaterial = secondWindow.selectedMat;
            textBlockMatError.Text = "";
            textBoxMat.Background = Brushes.LightGray;
            isMaterialFromFile = false;
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            
            bool error = false;
            for(int i = (int)EnumName.Min; i<= (int)EnumName.k; i++)
            {
                demensionPage.AllDataValue.Elements[i].Value = checkValueElements[i].Cheack();
                if(checkValueElements[i].Error == true)
                {
                    error = true;
                }
            }
            if(conditionService.ErrorCheck() == true)
            {
                error = true;
            }
            if(textBoxMat.Text == "")
            {
                error = true;
                textBlockMatError.Text = "Nie wybrano materiału";
                textBoxMat.Background = Brushes.LightPink;
            }
            else if(isMaterialFromFile == false)
            {
                selectedMaterial = secondWindow.selectedMat;
            }
            if (error == false)
            {
                if(demensionPage.AllDataValue.GetValueByEnumName(EnumName.Min)*demensionPage.AllDataValue.GetValueByEnumName(EnumName.nIn)<= demensionPage.AllDataValue.GetValueByEnumName(EnumName.Mout) * demensionPage.AllDataValue.GetValueByEnumName(EnumName.nOut))
                {
                    MessageBox.Show("Iloczyn prędkości obrotowej i momentu wyjściowego musi być mniejszy od wejściowego", "Błędna wartość", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
               NavigationService.Navigate(new ResultPage(new Calculations(demensionPage.AllDataValue, selectedMaterial, new SelectedWorkCondition(conditionService.EnumWork)), this));
            }
            
        }

        private void checkBoxK_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxKService.Checked();
        }

        private void checkBoxFriction_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxFrictionService.Checked();
        }

        private void conditionsButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Content.ToString();
            EnumWorkCondition enumWork;
            Enum.TryParse(name, out enumWork);
            conditionService.Click(enumWork);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(demensionPage);
        }
        public string GetTextToSave()
        {
            string dataToSave = "";
            for (int i = 0; i <= (int)EnumName.delta; i++)
            {
                dataToSave += $"{demensionPage.AllDataValue.Elements[i].Value}\n";
            }
            dataToSave += $"{demensionPage.GetSelectedFit()}\n";

            foreach(TextBox textBox in allTextBox)
            {
                dataToSave += $"{textBox.Text}\n";
            }
            if (conditionService.ErrorCheck() == false)
            {
                dataToSave += $"{conditionService.EnumWork}\n";
            }
            else
            {
                dataToSave += "\n";
            }
            return dataToSave;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            FileService.SaveFile(GetTextToSave());
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (demensionPage.dataFromFile != null)
            {
                SetDataFromFile();
            }

        }
    }
}
