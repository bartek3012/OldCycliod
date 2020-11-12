using CheckValues;
using Frontend.Entity;
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
    /// Interaction logic for InputMaterialPage.xaml
    /// </summary>
    public partial class DemensionPage : Page
    {
        public DemensionPage()
        {
            InitializeComponent();
            checkValueElements = new CheckValue[(int)EnumName.SizeOfElements];
            Initialize();
        }

        private CheckValue[] checkValueElements;
        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WorkingConditionsPage());
        }

        private void Initialize()
        {
            checkValueElements[(int)EnumName.D] = new CheckValue(textBlockDOutError, textBoxDOut, 100000);
            checkValueElements[(int)EnumName.d] = new CheckValue(textBlockDInError, textBoxDIn, 100000);
            checkValueElements[(int)EnumName.e] = new CheckValue(textBlockEError, textBoxE, 100000);
            checkValueElements[(int)EnumName.h] = new CheckValue(textBlockHError, textBoxH, 100000);
            checkValueElements[(int)EnumName.b] = new CheckValue(textBlockBError, textBoxB, 100000);
            checkValueElements[(int)EnumName.delta] = new CheckValue(textBlockDeltaError, textBoxDelta, 100000);
        }

        private void checkBoxH_Checked(object sender, RoutedEventArgs e)
        {
            textBoxH.Background = Brushes.White;
            textBoxH.IsReadOnly = false;
            checkBoxH.IsEnabled = false;
        }

        private void setCheckBoxEnabled()
        {
            textBoxH.Background = Brushes.LightGray;
            textBoxH.IsReadOnly = true;
            checkBoxH.IsEnabled = true;
            checkBoxH.IsChecked = false;
            textBoxB.Background = Brushes.LightGray;
            textBoxB.IsReadOnly = true;
            checkBoxB.IsEnabled = true;
            checkBoxB.IsChecked = false;
        }

        private void textBoxDOut_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            double D = checkValueElements[(int)EnumName.D].Cheack();
            double H = (D / 3.5)*0.6;
            double B = (D / 3.5) * 0.5;
            textBoxH.Text = H.ToString();
            textBoxB.Text = B.ToString();
            setCheckBoxEnabled();
        }
    }
}
