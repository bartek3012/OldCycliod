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
        public CheckValue[] checkValueElements { get; set; }
        private CheckBoxService checkBoxServiceH;
        private CheckBoxService checkBoxServiceB;
        private CheckBoxService checkBoxServiceDelta;
        private FitMenager fitMenager;
        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(workingCondPage);
        }

        private void Initialize()
        {
            checkValueElements[(int)EnumName.D] = new CheckValue(textBlockDOutError, textBoxDOut, 100000);
            checkValueElements[(int)EnumName.d] = new CheckValue(textBlockDInError, textBoxDIn, 100000);
            checkValueElements[(int)EnumName.e] = new CheckValue(textBlockEError, textBoxE, 100000);
            checkValueElements[(int)EnumName.h] = new CheckValue(textBlockHError, textBoxH, 100000);
            checkValueElements[(int)EnumName.b] = new CheckValue(textBlockBError, textBoxB, 100000);
            checkValueElements[(int)EnumName.delta] = new CheckValue(textBlockDeltaError, textBoxDelta, 100000);

            checkBoxServiceH = new CheckBoxService(checkBoxH, textBoxH);
            checkBoxServiceB = new CheckBoxService(checkBoxB, textBoxB);
            checkBoxServiceDelta = new CheckBoxService(checkBoxDelta, textBoxDelta);

            fitMenager = new FitMenager();
            ComboBoxFit.ItemsSource = fitMenager.FitElemets;

            workingCondPage = new WorkingConditionsPage(this);
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
            checkBoxServiceDelta.SetValue(1); //Do uzupełnienia
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
    }
}
