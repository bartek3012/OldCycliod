using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Backend.Serivce
{
   public class WorkConditionService : WorkCondition
    {
        public WorkConditionService() { }
        public void SetValue(EnumWorkCondition enumWorkCondition)
        {
            EnWorkCondition = enumWorkCondition;

            Value = (int)EnWorkCondition switch
            {
                (int)EnumWorkCondition.Lekkie => 0.1,
                (int)EnumWorkCondition.Średnie => 0.06,
                (int)EnumWorkCondition.Ciężkie => 0.03,
                _ => 0,
            };
            if(Value == 0)
            {
                MessageBox.Show("Błąd ustawienia warunków pracy", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
