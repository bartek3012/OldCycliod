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
        public Calculations(DataValueMenager dataValueMenager, BaseEntity selectedMaterialEntity, WorkConditionService selectedCondition)
        {
            allDataValue = dataValueMenager;
            selectedMaterial = selectedMaterialEntity;
            selectedWorkCondition = selectedCondition;
            Calculate();

        }
        public DataValueMenager allDataValue { get; private set; }
        public BaseEntity selectedMaterial { get; private set; }
        public WorkConditionService selectedWorkCondition { get; private set; }

        private double p;
        private double deformation;

        public void Calculate()
        {
            SetP();
            SetDeformation();
        }
        private void SetP()
        {
            p = selectedMaterial.Value * selectedWorkCondition.Value; //MPa
        }
        private void SetDeformation()
        {
            deformation = p * allDataValue.GetValueByEnumName(EnumName.k); //𝜇𝑚
        }
    }
}
