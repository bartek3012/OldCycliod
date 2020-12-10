using Backend;
using Backend.Entity;
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
        private DemensionPage demensionPage;
        private CheckBoxService checkBoxKService;
        private CheckBoxService checkBoxFrictionService;
        private WorkConditionService conditionService;
        private CheckValue[] checkValueElements;
        private BaseEntity selectedMaterial;
        private ListMaterialWindow secondWindow;//
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

        }

        //public TextBox TextBoxMaterial
        //{
        //    set { textBoxMat = value; }
        //}

        private void SetMaterialButtonClick(object sender, RoutedEventArgs e)
        {
            secondWindow = new ListMaterialWindow(textBoxMat);
            secondWindow.Show();
            selectedMaterial = secondWindow.selectedMat;
            textBlockMatError.Text = "";
            textBoxMat.Background = Brushes.LightGray;
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
            else
            {
                selectedMaterial = secondWindow.selectedMat;
            }
            if(error == false)
            {
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
    }
}
