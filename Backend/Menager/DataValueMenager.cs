using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Menager
{
    public class DataValueMenager : BaseMenager<DataValue>
    {
        //public Enum MyProperty { get; set; }
        protected override void Initialize()
        {
            Add(new DataValue("Średnica zewnętrzna D", EnumName.D, "[mm]", "input"));
            Add(new DataValue("Średnica wewnętrzna d", EnumName.d, "[mm]", "input"));
            Add(new DataValue("Mimośrodowe przesunięcie e", EnumName.e, "[mm]", "input"));
            Add(new DataValue("Głębokość rowka wpustu h", EnumName.h, "[mm]", "input"));
            Add(new DataValue("Szerokość rowka wpustu b", EnumName.b, "[mm]", "input"));
            Add(new DataValue("Luz pasowania Δ", EnumName.delta, "[μm]", "input"));
            Add(new DataValue("Moment zadany M", EnumName.Mout, "[Nm]", "input"));
            Add(new DataValue("Prędkość obrotowa n", EnumName.nIn, "[1/s]", "input"));
            Add(new DataValue("Wsp tarcia kinametycznego", EnumName.friction, "", "input"));
            Add(new DataValue("Wsp sztywności kontaktowej", EnumName.k, "[MPa/μm]", "input"));

            Add(new DataValue("Moment maksymalny Mmax", EnumName.Mmax, "[Nm]", "output"));
            Add(new DataValue("Straty mocy P", EnumName.P, "[W]", "output"));
            Add(new DataValue("Sprawność μ", EnumName.mi, "[%]", "output"));
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
        public List<DataValue> GetValuesByType(string type)
        {
            List <DataValue> result = new List<DataValue>();
            foreach(DataValue element in Elements)
            {
                if(element.Type == type)
                {
                    result.Add(element);
                }
            }
            return result;
        }
    }
}
