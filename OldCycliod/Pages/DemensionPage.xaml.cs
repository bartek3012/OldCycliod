using CheckValues;
using Backend.Entity;
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
using Backend.Menager;
using Backend.Enum;

namespace OldCycliod
{
    /// <summary>
    /// Interaction logic for InputMaterialPage.xaml
    /// </summary>
    public partial class DemensionPage : Page
    {
        public DemensionPage()
        {
            InitializeComponent();
            checkValueElements = new CheckValue[(int)EnumName.Mmax]; //Mmax is first user input element 
            Initialize();
        }
        private WorkingConditionsPage workingCondPage;
        public CheckValue[] checkValueElements { get; set; } //czy nie priv?
        private CheckBoxService checkBoxServiceH;
        private CheckBoxService checkBoxServiceB;
        private CheckBoxService checkBoxServiceDelta;
        private FitMenager fitMenager;
        public DataValueMenager AllDataValue { get; set; }
        private void Initialize()
        {
            checkValueElements[(int)EnumName.D] = new CheckValue(textBlockDOutError, textBoxDOut, 500, 1.1);
            checkValueElements[(int)EnumName.d] = new CheckValue(textBlockDInError, textBoxDIn, 500);
            checkValueElements[(int)EnumName.e] = new CheckValue(textBlockEError, textBoxE, 500);
            checkValueElements[(int)EnumName.h] = new CheckValue(textBlockHError, textBoxH, 500);
            checkValueElements[(int)EnumName.b] = new CheckValue(textBlockBError, textBoxB, 500);
            checkValueElements[(int)EnumName.delta] = new CheckValue(textBlockDeltaError, textBoxDelta, 5000);

            checkBoxServiceH = new CheckBoxService(checkBoxH, textBoxH);
            checkBoxServiceB = new CheckBoxService(checkBoxB, textBoxB);
            checkBoxServiceDelta = new CheckBoxService(checkBoxDelta, textBoxDelta);

            fitMenager = new FitMenager();
            ComboBoxFit.ItemsSource = fitMenager.FitElemets;

            workingCondPage = new WorkingConditionsPage(this);
            AllDataValue = new DataValueMenager();
        }

        private void checkBoxH_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxServiceH.Checked();
        }
        private void checkBoxB_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxServiceB.Checked();
        }


        private void textBoxDOut_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            double D = checkValueElements[(int)EnumName.D].Cheack();
            double H = (D / 3.5)*0.6;
            double B = (D / 3.5) * 0.5;
            checkBoxServiceH.SetValue(H);
            checkBoxServiceB.SetValue(B);
        }

        private void checkBoxDelta_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxServiceDelta.Checked();
        }

        private void ComboBoxFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnumFit id = (EnumFit)ComboBoxFit.SelectedIndex;
            int delta = fitMenager.CheckFitValue(id, checkValueElements[(int)EnumName.D].Cheack());
            checkBoxServiceDelta.SetValue(delta);
        }

        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            bool error = false;
            AllDataValue.Elements[(int)EnumName.D].Value = checkValueElements[(int)EnumName.D].Cheack();
            if(checkValueElements[(int)EnumName.D].Error == false)
            {
                checkValueElements[(int)EnumName.d].Max = AllDataValue.Elements[(int)EnumName.D].Value - 1;
                checkValueElements[(int)EnumName.e].Max = AllDataValue.Elements[(int)EnumName.D].Value * 0.1;

                AllDataValue.Elements[(int)EnumName.d].Value = checkValueElements[(int)EnumName.d].Cheack();
                if (checkValueElements[(int)EnumName.d].Error == false)
                {
                    checkValueElements[(int)EnumName.b].Max = AllDataValue.Elements[(int)EnumName.d].Value - 1;
                    checkValueElements[(int)EnumName.h].Max = AllDataValue.Elements[(int)EnumName.d].Value - 1;
                }
            }

            
            for (int i = 2; i <= (int)EnumName.delta; i++) //zero and first elements (D, d) have been already checked
            {
                checkValueElements[i].ClearError();
                
                AllDataValue.Elements[i].Value = checkValueElements[i].Cheack();
                if(checkValueElements[i].Error == true)
                {
                    error = true;
                }
            }
            if(error == false)
            {
                NavigationService.Navigate(workingCondPage);
            }
        }
    }
}
