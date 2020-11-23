using Backend.Entity;
using Backend.Enum;
using Backend.FitDataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

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
        public void CheckFitValue()
        {
            List<ValueFit> values = new List<ValueFit>();
            values.Add(new ValueFit(1, 28));
            values.Add(new ValueFit(2, 38));
            values.Add(new ValueFit(3, 47));
            values.Add(new ValueFit(4, 59));
            values.Add(new ValueFit(5, 73));
            values.Add(new ValueFit(6, 89));
            values.Add(new ValueFit(7, 106));
            values.Add(new ValueFit(8, 126));
            values.Add(new ValueFit(9, 148));
            values.Add(new ValueFit(10, 172));
            values.Add(new ValueFit(11, 191));
            values.Add(new ValueFit(12, 214));
            values.Add(new ValueFit(13, 232));

            XmlDocument xmlDoc = new XmlDocument();
            string secondPartPath = EnumFit.H7_f8.ToString() + ".xml";
            string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "H7_f8.xml");
            string xml = File.ReadAllText(path1);
            xmlDoc.Load(path1);

            string path = @"FitDataBase\" + EnumFit.H8_e8.ToString() + ".xml";

            //XmlRootAttribute root = new XmlRootAttribute();
            //root.ElementName = "ValueFit";
            //root.IsNullable = true;
            //XmlSerializer xml = new XmlSerializer(typeof(List<ValueFit>), root);

            //using (StreamWriter sw = new StreamWriter(@"C:\Users\user\source\repos\OldCycliod\Backend\FitDataBase\XmlFiles\H8_e8.xml"))
            //{
            //    xml.Serialize(sw, values);
            //}

        }
    }
}
