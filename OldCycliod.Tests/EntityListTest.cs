using Frontend.Entity;
using Frontend.Menager;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
    }
}
