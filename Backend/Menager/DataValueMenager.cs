using Frontend.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Frontend.Menager
{
    public class DataValueMenager : BaseMenager<DataValue>
    {
        //public Enum MyProperty { get; set; }
        public override void Initialize()
        {
            Add(new DataValue("Średnica zewnętrzna D", EnumName.D, "[mm]", "input"));
            Add(new DataValue("Średnica wewnętrzna d", EnumName.d, "[mm]", "input"));
            Add(new DataValue("Mimośrodowe przesunięcie e", EnumName.e, "[mm]", "input"));
            Add(new DataValue("Głębokość rowka wpustu h", EnumName.h, "[mm]", "input"));
            Add(new DataValue("Szerokość rowka wpustu b", EnumName.b, "[mm]", "input"));
            Add(new DataValue("Luz pasowania Δ", EnumName.delta, "[μm]", "input"));
            Add(new DataValue("Moment zadany M", EnumName.Mz, "[Nm]", "input"));
            Add(new DataValue("Prędkość obrotowa n", EnumName.n, "1/s", "input"));
            Add(new DataValue("Wsp sztywności kontaktowej", EnumName.k, "MPa/𝜇m", "input"));

            Add(new DataValue("Moment maksymalny Mmax", EnumName.Mmax, "[Nm]", "output"));
            Add(new DataValue("Straty mocy P", EnumName.P, "W", "output"));
            Add(new DataValue("Sprawność μ", EnumName.mi, "%", "output"));
        }

        public void SetValueByEnumName(EnumName enumName, double value)
        {
            DataValue dataValue = Elements.Find(p => p.NameId == enumName);
            dataValue.Value = value;
        }
        public double GetValueByEnumName(EnumName enumName)
        {
            DataValue dataValue = Elements.Find(p => p.NameId == enumName);
            return dataValue.Value;
        }
    }
}
