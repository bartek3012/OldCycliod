using Backend.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Entity
{
    public class Fit
    {
        public Fit(EnumFit enumFit)
        {
            FitEnum = enumFit;
            Name = enumFit.ToString().Replace("_", "/");
        }
        public string  Name { get; set; }
        public EnumFit FitEnum { get; set; }

    }
}
