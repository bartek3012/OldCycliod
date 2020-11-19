using Backend.Serivce;
using Backend.Entity;
using Backend.Menager;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Backend;

namespace OldCycliod.Tests
{
   public class EntityListTest
    {
        [Fact]
        public void Test()
        {
            //Arrange
            DataValueMenager dataValueMenager = new DataValueMenager();

            //Act
            dataValueMenager.SetValueByEnumName(EnumName.h, 34.9);
            double result = dataValueMenager.GetValueByEnumName(EnumName.h);

            //Assert
            Assert.Equal(34.9, result, 2);
        }

        [Fact]
        public void WorkConditionTest()
        {
            //Arrange
            WorkConditionService workConditionService = new WorkConditionService();

            //Act
            workConditionService.SetValue(Backend.Entity.EnumWorkCondition.Ciężkie);

            //Assert
            Assert.Equal(0.03, workConditionService.Value);
        }

        [Fact]
        public void GetEntityByType()
        {
            //Arrange
            DataValueMenager dataValueMenager = new DataValueMenager();

            //Act
            BaseEntity baseEntity = dataValueMenager.GetByType("wrongType");
            BaseEntity baseEntity2 = dataValueMenager.GetByType("output");

            //Assert
            Assert.Null(baseEntity);
            Assert.Equal(dataValueMenager.Elements[(int)EnumName.Mmax], baseEntity2);
        }

        [Fact]
        public void FitEntity()
        {
            //Arrange+Act
            FitMenager dataValueMenager = new FitMenager();
            string result = dataValueMenager.FitElemets[0].Name;
            //Assert
            Assert.Equal("H7/e8", result);
        }
    }
}
