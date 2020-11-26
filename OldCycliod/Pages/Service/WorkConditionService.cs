using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace OldCycliod.Pages.Service
{
   public class WorkConditionService
    {
        public WorkConditionService(TextBlock textError, Button []but)
        {
            textBlockError = textError;
            buttons = but;
            Value = 0;
        }
        private TextBlock textBlockError;
        private Button[] buttons;
        private bool error = true;
        public double Value { get; private set; }
        public EnumWorkCondition EnumWork { get; private set; }

        public void Click(EnumWorkCondition enumWork)
        {
            foreach(Button button in buttons)
            {
                button.Background = Brushes.LightGray;
            }
            buttons[(int)enumWork].Background = Brushes.Gray;
            clearError();
            error = false;
            setValue();
        }

        private void setValue() 
        {
            switch (EnumWork)
            {
                case EnumWorkCondition.Lekkie:
                    Value = 0.1;
                    break;
                case EnumWorkCondition.Średnie:
                    Value = 0.06;
                    break;
                case EnumWorkCondition.Ciężkie:
                    Value = 0.03;
                    break;
            };
        }
        public bool ErrorCheck()
        {
            if(error == true)
            {
                textBlockError.Text = "Nie wybrano warunków pracy";
                return true;
            }
            else
            {
                return false;
            }
        }

        private void clearError()
        {
            textBlockError.Text = "";
        }
    }
}
