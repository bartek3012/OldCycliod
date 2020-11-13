using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace OldCycliod.Pages.Service
{
    public class CheckBoxService
    {
        private CheckBox checkBox;
        private TextBox textBox;
       public CheckBoxService(CheckBox check, TextBox text)
        {
            checkBox = check;
            textBox = text;
        }
        private void resetCheckBox()
        {
            textBox.Background = Brushes.LightGray;
            textBox.IsReadOnly = true;
            checkBox.IsEnabled = true;
            checkBox.IsChecked = false;
        }
        public void Checked()
        {
            textBox.Background = Brushes.White;
            textBox.IsReadOnly = false;
            checkBox.IsEnabled = false;
        }
        public void SetValue(double value)
        {
            value = Math.Round(value, 2);
            textBox.Text = value.ToString();
            resetCheckBox();
        }

    }
}
