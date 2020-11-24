using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Backend.FitDataBase
{
    public class ValueFit
    {
        public ValueFit()
        {
        }
        public ValueFit(int id, int value)
        {
            Id = id;
            Value = value;
        }
        [XmlAttribute ("Id")]
        public int Id { get; set; }
        [XmlElement("Value")]
        public int Value { get; set; }
    }
}
