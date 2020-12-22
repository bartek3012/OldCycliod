using Backend.Entity;
using Backend.Menager;
using Backend.Serivce;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Backend.Results
{
    public class Calculations
    {
        public Calculations(DataValueMenager dataValueMenager, BaseEntity selectedMaterialEntity, SelectedWorkCondition selectedCondition)
        {
            AllDataValue = dataValueMenager;
            SelectedMaterial = selectedMaterialEntity;
            SelectedWorkCondition = selectedCondition;
        }
        public DataValueMenager AllDataValue { get; private set; }
        public BaseEntity SelectedMaterial { get; private set; }
        public SelectedWorkCondition SelectedWorkCondition { get; private set; }

        private double p;
        private double deformation;
        private double l;
        private double lPrim;
        private double pPrim;
        private double F;
        private double FPrim;
        private double x;
        private double xPrim;
        private double M;
        private double Plost;
        private double Pin;
        private double mi;
        private bool isMinus;
        private bool efficiecyIsPositive = true;

        private double v;//
        public bool Calculate()
        {
            
            MakeClaclultion();
            if(isMinus==true)
            {
                var answer = MessageBox.Show("Dla podanego pasowania długość działania siły l' ma wartość ulemną." +
                    "\nCzy pominąć wartość pasowania w obliczeniach?", "Uwaga", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(answer == MessageBoxResult.Yes)
                {
                AllDataValue.SetValueByEnumName(EnumName.delta, 0);
                MakeClaclultion();
                }    
            }
            return efficiecyIsPositive;
        }
        private void MakeClaclultion()
        {
            isMinus = false;
            SetP();
            SetDeformation();
            SetL();
            SetLPrim();
            SetPPrim();
            SetF();
            SetFPrim();
            SetX();
            SetXPrim();
            SetM();
            SetPlost();
            SetPin();
            SetMi();
            //SetpminANDpmax();
        }
        private void SetP()
        {
            p = SelectedMaterial.Value * SelectedWorkCondition.Value; //MPa
        }
        private void SetDeformation()
        {
            deformation = p * AllDataValue.GetValueByEnumName(EnumName.k); //𝜇𝑚
        }
        private void SetL()
        {
            double numerator = (AllDataValue.GetValueByEnumName(EnumName.D)- AllDataValue.GetValueByEnumName(EnumName.e)) * deformation / 2000; //licznik, podzielenie przez 1000 aby otrzymać wynik w metrach
            double denumerator = deformation + (AllDataValue.GetValueByEnumName(EnumName.delta) / 2); //mianownik
            l = numerator / denumerator;  // [m]
        }
        private void SetLPrim()
        {
            //double secondPart = (AllDataValue.GetValueByEnumName(EnumName.D) - AllDataValue.GetValueByEnumName(EnumName.d)) / 2000;
            //double thirdPart = AllDataValue.GetValueByEnumName(EnumName.e)/ 1000;
            //lPrim = l - secondPart + thirdPart; //przez 1000 przejście na metry         [m]
            lPrim = l*(AllDataValue.GetValueByEnumName(EnumName.d)+ AllDataValue.GetValueByEnumName(EnumName.e))/(AllDataValue.GetValueByEnumName(EnumName.D) - AllDataValue.GetValueByEnumName(EnumName.e));
            if (lPrim <= 0)
            {
                isMinus = true;
            }
        }
        private void SetPPrim()
        {
            pPrim = (lPrim/l) * p; //MPa
        }
        private void SetF()
        {
            F = (p * l * AllDataValue.GetValueByEnumName(EnumName.h))/2; //[kN]
        }
        private void SetFPrim()
        {
            FPrim = (pPrim * lPrim * AllDataValue.GetValueByEnumName(EnumName.h)) / 2; //[kN]
        }
        private void SetX()
        {
            x = ((AllDataValue.GetValueByEnumName(EnumName.D) - AllDataValue.GetValueByEnumName(EnumName.e)) / 2000) - (l / 3);
        }
        private void SetXPrim()
        {
            xPrim = ((AllDataValue.GetValueByEnumName(EnumName.d) - AllDataValue.GetValueByEnumName(EnumName.e)) / 2000) - (lPrim / 3);
        }
        private void SetM()
        {
            M = 2000 * (F * x - FPrim * xPrim); //Nm
            AllDataValue.SetValueByEnumName(EnumName.Mmax, Math.Round(M,2));
        }
        private void SetV()
        {
            v = (4 * AllDataValue.GetValueByEnumName(EnumName.e) * AllDataValue.GetValueByEnumName(EnumName.nOut)) / 60000;
        }
        private void SetPlost()
        {
            double F = AllDataValue.GetValueByEnumName(EnumName.Mout) / (2 * (x - (xPrim * xPrim / x)));
            double wk = 2 * Math.PI * AllDataValue.GetValueByEnumName(EnumName.nOut)/60;
            Plost = 8 * wk * F * AllDataValue.GetValueByEnumName(EnumName.friction) * AllDataValue.GetValueByEnumName(EnumName.e) * (1 - (xPrim / x))/(Math.PI * 1000);

            //SetV();
            //Plost = 4000 * AllDataValue.GetValueByEnumName(EnumName.friction) * (F - FPrim) * v; //W
            AllDataValue.SetValueByEnumName(EnumName.PLost, Math.Round(Plost,2));
        }
        private void SetPin()
        {
            double w0 = 2 * Math.PI * AllDataValue.GetValueByEnumName(EnumName.nIn) / 60;
            Pin = AllDataValue.GetValueByEnumName(EnumName.Min) * w0;
            AllDataValue.SetValueByEnumName(EnumName.PAll, Math.Round(Pin,2));
        }
        private void SetMi()
        {
            mi = (1 - (Plost / Pin))*100; //%
            //mi = (1 - (4 * AllDataValue.GetValueByEnumName(EnumName.friction) * AllDataValue.GetValueByEnumName(EnumName.e)) / (Math.PI * 1000 * (x + xPrim)))*100;
            AllDataValue.SetValueByEnumName(EnumName.mi, Math.Round(mi,2));
            if(mi<=0)
            {
                efficiecyIsPositive = false;
            }
        }

        //private void SetpminANDpmax()
        //{
        //    double secondPartDeminator = (AllDataValue.GetValueByEnumName(EnumName.d) + AllDataValue.GetValueByEnumName(EnumName.e)) / (AllDataValue.GetValueByEnumName(EnumName.D) - AllDataValue.GetValueByEnumName(EnumName.e)) * AllDataValue.GetValueByEnumName(EnumName.d) / 2;
        //    double pmin = AllDataValue.GetValueByEnumName(EnumName.Mout) / (AllDataValue.GetValueByEnumName(EnumName.h) * l * secondPartDeminator);
        //    AllDataValue.SetValueByEnumName(EnumName.pmin, Math.Round(pmin, 2));

        //    secondPartDeminator = (AllDataValue.GetValueByEnumName(EnumName.D)/2000)+(AllDataValue.GetValueByEnumName(EnumName.d) + AllDataValue.GetValueByEnumName(EnumName.e)) / (AllDataValue.GetValueByEnumName(EnumName.D) - AllDataValue.GetValueByEnumName(EnumName.e));
        //    double pmax = AllDataValue.GetValueByEnumName(EnumName.Mout) / (AllDataValue.GetValueByEnumName(EnumName.h) * l * secondPartDeminator);
        //    AllDataValue.SetValueByEnumName(EnumName.pmax, Math.Round(pmax, 2));
        //}
        public List<DataValue> GetInputElements()
        {
            return AllDataValue.GetValuesByType("input");
        }
        public List<DataValue> GetOutputElements()
        {
            return AllDataValue.GetValuesByType("output");
        }

    }
}
