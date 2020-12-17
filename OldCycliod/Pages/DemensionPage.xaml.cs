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
using Backend.Serivce;

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
            CheckValueElements = new CheckValue[(int)EnumName.Mmax]; //Mmax is first user input element 
            Initialize();
        }
        private WorkingConditionsPage workingCondPage;
        public CheckValue[] CheckValueElements { get; private set; } //czy nie priv?
        private CheckBoxService checkBoxServiceH;
        private CheckBoxService checkBoxServiceB;
        private CheckBoxService checkBoxServiceDelta;
        private FitMenager fitMenager;
        private bool setComboBox = false;
        private TextBox[] allTextBox;
        public string []dataFromFile { get; set; }
        public DataValueMenager AllDataValue { get; set; }
        private void Initialize()
        {
            CheckValueElements[(int)EnumName.D] = new CheckValue(textBlockDOutError, textBoxDOut, 3000, 15);
            CheckValueElements[(int)EnumName.d] = new CheckValue(textBlockDInError, textBoxDIn, 500);
            CheckValueElements[(int)EnumName.e] = new CheckValue(textBlockEError, textBoxE, 500);
            CheckValueElements[(int)EnumName.h] = new CheckValue(textBlockHError, textBoxH, 500, 1.1);
            CheckValueElements[(int)EnumName.b] = new CheckValue(textBlockBError, textBoxB, 500, 1.1);
            CheckValueElements[(int)EnumName.delta] = new CheckValue(textBlockDeltaError, textBoxDelta, 5000);

            checkBoxServiceH = new CheckBoxService(checkBoxH, textBoxH);
            checkBoxServiceB = new CheckBoxService(checkBoxB, textBoxB);
            checkBoxServiceDelta = new CheckBoxService(checkBoxDelta, textBoxDelta);

            fitMenager = new FitMenager();
            comboBoxFit.ItemsSource = fitMenager.FitElemets;

            workingCondPage = new WorkingConditionsPage(this);
            AllDataValue = new DataValueMenager();
            allTextBox = new TextBox[] { textBoxDOut, textBoxDIn, textBoxE, textBoxH, textBoxB, textBoxDelta};
            dataFromFile = null;
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
            double D = CheckValueElements[(int)EnumName.D].Cheack();
            if(CheckValueElements[(int)EnumName.D].Error == false)
            {
            double H = Math.Round((D / 3.5)*0.3, 0);
            double B = Math.Round((D / 3.5) * 0.5, 0);
            checkBoxServiceH.SetValue(H);
            checkBoxServiceB.SetValue(B);
            if(setComboBox == true)
            {
                EnumFit id = (EnumFit)comboBoxFit.SelectedIndex;
                int delta = fitMenager.CheckFitValue(id, CheckValueElements[(int)EnumName.D].Cheack());
                checkBoxServiceDelta.SetValue(delta);
            }
            }
        }

        private void checkBoxDelta_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxServiceDelta.Checked();
        }

        private void ComboBoxFit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnumFit id = (EnumFit)comboBoxFit.SelectedIndex;
            double B = CheckValueElements[(int)EnumName.b].Cheack();
            if(B>1 && (int)id!=-1)
            {
            int delta = fitMenager.CheckFitValue(id, B);
            checkBoxServiceDelta.SetValue(delta);
            }
            setComboBox = true;
        }
        public int GetSelectedFit()
        {
            return comboBoxFit.SelectedIndex;
        }
        private void nextButtonClick(object sender, RoutedEventArgs e)
        {
            bool error = false;
            AllDataValue.Elements[(int)EnumName.D].Value = CheckValueElements[(int)EnumName.D].Cheack();
            if (CheckValueElements[(int)EnumName.D].Error == false)
            {
                CheckValueElements[(int)EnumName.d].Max = Math.Round(AllDataValue.Elements[(int)EnumName.D].Value * 0.75, 2);
                CheckValueElements[(int)EnumName.e].Max = Math.Round(AllDataValue.Elements[(int)EnumName.D].Value * 0.1, 1);

                AllDataValue.Elements[(int)EnumName.d].Value = CheckValueElements[(int)EnumName.d].Cheack();
                if (CheckValueElements[(int)EnumName.d].Error == false)
                {
                    CheckValueElements[(int)EnumName.b].Max = Math.Round(AllDataValue.Elements[(int)EnumName.D].Value - 0.5, 3);
                    CheckValueElements[(int)EnumName.h].Max = Math.Round(AllDataValue.Elements[(int)EnumName.D].Value - 0.5, 3);
                }
            }

            for (int i = 2; i <= (int)EnumName.delta; i++) //zero and first elements (D, d) have been already checked
            {
                CheckValueElements[i].ClearError();
                AllDataValue.Elements[i].Value = CheckValueElements[i].Cheack();
            }
            for (int i = 0; i <= (int)EnumName.delta; i++)
            {
                if (CheckValueElements[i].Error == true)
                {
                    error = true;
                }
            }
            if (error == false)
            {
                NavigationService.Navigate(workingCondPage);
            }
        }

        private void clearButtonClick(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i<=(int)EnumName.delta; i++)
            {
                CheckValueElements[i].Clear();
            }

        }

        private void textBoxB_LostFocus(object sender, RoutedEventArgs e)
        {
            if(setComboBox == true)
            {
            EnumFit id = (EnumFit)comboBoxFit.SelectedIndex;
            int delta = fitMenager.CheckFitValue(id, CheckValueElements[(int)EnumName.b].Cheack());
            checkBoxServiceDelta.SetValue(delta);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string result ="";
            foreach (TextBox textBox in allTextBox)
            {
                result += $"{textBox.Text}\n";
            }
            result += comboBoxFit.SelectedIndex.ToString();
            FileService.SaveFile(result);
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            comboBoxFit.SelectedIndex = -1;
            string[] data  = FileService.OpenFile();
            if(data==null)
            {
                return;
            }
            int i = 0;
            foreach (TextBox item in allTextBox)
            {
                item.Text = data[i];
                i++;
            }
            try
            {
            if(data.Length>(int)EnumName.delta+2)
            {
                dataFromFile = data;
            }
            comboBoxFit.SelectedIndex = Int32.Parse(data[(int)EnumName.delta+1]);
            }
            catch (Exception)
            {
                MessageBox.Show("Plik do otwarcia jest nie poprawny", "Niepoprawny plik", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
    }
}
