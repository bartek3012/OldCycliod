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
        }
        private TextBlock textBlockError;
        private Button[] buttons;
        private bool error = true;
        public EnumWorkCondition EnumWork { get; private set; }

        public void Click(EnumWorkCondition enumWork)
        {
            foreach(Button button in buttons)
            {
                button.Background = Brushes.LightGray;
            }
            EnumWork = enumWork;
            buttons[(int)enumWork].Background = Brushes.Gray;
            clearError();
            error = false;
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
