using Backend.Entity;
using Backend.Enum;
using Backend.FitDataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
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
            FitElemets.Add(new Fit(EnumFit.H7_f7));
            FitElemets.Add(new Fit(EnumFit.H7_g6));
            FitElemets.Add(new Fit(EnumFit.H7_h6));
            FitElemets.Add(new Fit(EnumFit.H8_d9));
            FitElemets.Add(new Fit(EnumFit.H8_e8));
            FitElemets.Add(new Fit(EnumFit.H8_h7));
            FitElemets.Add(new Fit(EnumFit.H8_h8));
            FitElemets.Add(new Fit(EnumFit.H9_d9));
            FitElemets.Add(new Fit(EnumFit.H11_d11));
            FitElemets.Add(new Fit(EnumFit.H11_h11));
        }

        public List<Fit> FitElemets { get; private set; }
        public int CheckFitValue(EnumFit enumFit, double widthB)
        {
            string secondPartPath = enumFit.ToString() + ".xml";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, secondPartPath);
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "ValueFit";
            root.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ValueFit>), root);
            string xml = File.ReadAllText(path);
            StringReader stringReader = new StringReader(xml);
            var values = (List<ValueFit>)xmlSerializer.Deserialize(stringReader);

            if(widthB>1 && widthB<=3)
            {
                return values.Where(p => p.Id == 1).Select(p => p.Value).First();
            }
            else if (widthB <= 6)
            {
                return values.Where(p => p.Id == 2).Select(p => p.Value).First();
            }
            else if (widthB <= 10)
            {
                return values.Where(p => p.Id == 3).Select(p => p.Value).First();
            }
            else if (widthB <= 18)
            {
                return values.Where(p => p.Id == 4).Select(p => p.Value).First();
            }
            else if (widthB <= 30)
            {
                return values.Where(p => p.Id == 5).Select(p => p.Value).First();
            }
            else if (widthB <= 50)
            {
                return values.Where(p => p.Id == 6).Select(p => p.Value).First();
            }
            else if (widthB <= 80)
            {
                return values.Where(p => p.Id == 7).Select(p => p.Value).First();
            }
            else if (widthB <= 120)
            {
                return values.Where(p => p.Id == 8).Select(p => p.Value).First();
            }
            else if (widthB <= 180)
            {
                return values.Where(p => p.Id == 9).Select(p => p.Value).First();
            }
            else if (widthB <= 250)
            {
                return values.Where(p => p.Id == 10).Select(p => p.Value).First();
            }
            else if (widthB <= 315)
            {
                return values.Where(p => p.Id == 11).Select(p => p.Value).First();
            }
            else if (widthB <= 400)
            {
                return values.Where(p => p.Id == 12).Select(p => p.Value).First();
            }
            else if (widthB <= 500)
            {
                return  values.Where(p => p.Id == 13).Select(p => p.Value).First();
            }
            else
            {
                MessageBox.Show("Błędna wartość wpustu");
                return -1;
            }


        }
    }
}
