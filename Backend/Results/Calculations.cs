using Backend.Entity;
using Backend.Menager;
using Backend.Serivce;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Results
{
    public class Calculations
    {
        public Calculations(DataValueMenager dataValueMenager, BaseEntity selectedMaterialEntity, SelectedWorkCondition selectedCondition)
        {
            allDataValue = dataValueMenager;
            selectedMaterial = selectedMaterialEntity;
            selectedWorkCondition = selectedCondition;
            Calculate();

        }
        public DataValueMenager allDataValue { get; private set; }
        public BaseEntity selectedMaterial { get; private set; }
        public SelectedWorkCondition selectedWorkCondition { get; private set; }

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

        public void Calculate()
        {
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
        }
        private void SetP()
        {
            p = selectedMaterial.Value * selectedWorkCondition.Value; //MPa
        }
        private void SetDeformation()
        {
            deformation = p * allDataValue.GetValueByEnumName(EnumName.k); //𝜇𝑚
        }
        private void SetL()
        {
            double numerator = (allDataValue.GetValueByEnumName(EnumName.D) - allDataValue.GetValueByEnumName(EnumName.e)) * deformation / 2000; //licznik, podzielenie przez 1000 aby otrzymać wynik w metrach
            double denumerator = deformation + (allDataValue.GetValueByEnumName(EnumName.delta) / 2); //mianownik
            l = numerator / denumerator;  // [m]
        }
        private void SetLPrim()
        {
            double secondPart = (allDataValue.GetValueByEnumName(EnumName.D) - allDataValue.GetValueByEnumName(EnumName.d)) / 2000;
            double thirdPart = allDataValue.GetValueByEnumName(EnumName.e)/ 1000;
            lPrim = l - secondPart + thirdPart; //przez 1000 przejście na metry         [m]
        }
        private void SetPPrim()
        {
            pPrim = (lPrim/l) * p; //MPa
        }
        private void SetF()
        {
            F = (p * l * allDataValue.GetValueByEnumName(EnumName.h))/2; //[kN]
        }
        private void SetFPrim()
        {
            FPrim = (pPrim * lPrim * allDataValue.GetValueByEnumName(EnumName.h)) / 2; //[kN]
        }
        private void SetX()
        {
            x = ((allDataValue.GetValueByEnumName(EnumName.D) - allDataValue.GetValueByEnumName(EnumName.e)) / 2000) - (l / 3);
        }
        private void SetXPrim()
        {
            xPrim = ((allDataValue.GetValueByEnumName(EnumName.d) - allDataValue.GetValueByEnumName(EnumName.e)) / 2000) - (lPrim / 3);
        }
        private void SetM()
        {
            M = 2000 * (F * x - FPrim * xPrim); //Nm
        }
    }
}
