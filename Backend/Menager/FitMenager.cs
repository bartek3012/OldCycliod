using Backend.Entity;
using Backend.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Menager
{
    public class FitMenager
    {
        public FitMenager()
        {
            FitElemets = new List<Fit>();
            Initialize();
        }
        private void Initialize()
        {
            FitElemets.Add(new Fit(EnumFit.H7_e8));
            FitElemets.Add(new Fit(EnumFit.H7_f8));
            FitElemets.Add(new Fit(EnumFit.H7_g8));
            FitElemets.Add(new Fit(EnumFit.H7_h8));
            FitElemets.Add(new Fit(EnumFit.H8_d9));
            FitElemets.Add(new Fit(EnumFit.H8_e8));
            FitElemets.Add(new Fit(EnumFit.H8_h7));
            FitElemets.Add(new Fit(EnumFit.H8_h8));
            FitElemets.Add(new Fit(EnumFit.H9_d9));
            FitElemets.Add(new Fit(EnumFit.H11_d11));
            FitElemets.Add(new Fit(EnumFit.H11_h11));
        }

        public List<Fit> FitElemets;
    }
}
