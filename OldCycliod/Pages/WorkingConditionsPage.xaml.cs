using Backend;
using Backend.Entity;
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
        }
        private DemensionPage demensionPage;
        private CheckBoxService checkBoxService;
        private WorkConditionService conditionService;
        private CheckValue[] checkValueElements;
        private BaseEntity selectedMaterial;
        private void Inicjalize()
        {
            checkValueElements[(int)EnumName.Mz] = new CheckValue(textBlockMError, textBoxM, 100000);
            checkValueElements[(int)EnumName.n] = new CheckValue(textBlockNError, textBoxN, 100000);
            checkValueElements[(int)EnumName.k] = new CheckValue(textBlockKError, textBoxK, 100000);
            checkBoxService = new CheckBoxService(checkBoxK, textBoxK);

            Button[] ConditionButtons = new Button[] {LekkieButton, SrednieButton, CiezkieButton};
            conditionService = new WorkConditionService(textBlockWorkCaseError, ConditionButtons);
        }

        //public TextBox TextBoxMaterial
        //{
        //    set { textBoxMat = value; }
        //}

        private void SetMaterialButtonClick(object sender, RoutedEventArgs e)
        {
           ListMaterialWindow secondWindow = new ListMaterialWindow(selectedMaterial, textBoxMat);
            secondWindow.Show();
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            bool error = false;
            if(conditionService.ErrorCheck() == true)
            {
                error = true;
            }
            if(error == false)
            {
                NavigationService.Navigate(new ResultPage());
            }
            
        }

        private void checkBoxK_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxService.Checked();
        }

        private void conditionsButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Content.ToString();
            EnumWorkCondition enumWork;
            Enum.TryParse(name, out enumWork);
            conditionService.Click(enumWork);
        }
    }
}
