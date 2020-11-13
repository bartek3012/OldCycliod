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
        public int Value { get; private set; }

        public void Click(EnumWorkCondition enumWork)
        {
            foreach(Button button in buttons)
            {
                button.Background = Brushes.LightGray;
            }
            buttons[(int)enumWork].Background = Brushes.Gray;
        }
    }
}
