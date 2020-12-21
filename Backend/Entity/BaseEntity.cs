using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Backend
{
    public class BaseEntity
    {
        public BaseEntity(string content, string type, string unit, double value = 0)
        {
            Value = value;
            Content = content;
            Type = type;
            Unit = unit;
        }

        public BaseEntity(string content, string type, double value)
        {
            Value = value;
            Content = content;
            Type = type;
            Unit = "[MPa]";
        }

        public BaseEntity()
        {

        }
        [XmlAttribute("Value")]
        public double Value { get; set; }
        [XmlElement("Content")]
        public string Content { get; set; }
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Unit")]
        public string Unit { get; set; }
    }


}
