using Backend;
using Backend.Menager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace Backend.Menager
{
    public class MaterialsMenager : BaseMenager<BaseEntity>
    {
        private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MaterialsBase.xml");
        protected override void Initialize()
        {
            TakeFileToList();
            //Elements.Add(new BaseEntity("S185", "Stal niestopowa", 100));
            //Elements.Add(new BaseEntity("S235", "Stal niestopowa", 130));
            //Elements.Add(new BaseEntity("E295", "Stal niestopowa", 145));
            //Elements.Add(new BaseEntity("E335", "Stal niestopowa", 160));
            //Elements.Add(new BaseEntity("E360", "Stal niestopowa", 175));

            //Elements.Add(new BaseEntity("C10E", "Stal niestopowa (normalizowana)", 105));
            //Elements.Add(new BaseEntity("C22", "Stal niestopowa (normalizowana)", 125));
            //Elements.Add(new BaseEntity("C35", "Stal niestopowa (normalizowana)", 155));
            //Elements.Add(new BaseEntity("C55", "Stal niestopowa (normalizowana)", 185));

            //Elements.Add(new BaseEntity("C25", "Stal niestopowa (ulepszana cieplnie)", 150));
            //Elements.Add(new BaseEntity("C35", "Stal niestopowa (ulepszana cieplnie)", 180));
            //Elements.Add(new BaseEntity("C55", "Stal niestopowa (ulepszana cieplnie)", 225));

            //Elements.Add(new BaseEntity("17Cr3", "Stal stopowa (nawęglana i hartowana)", 250));
            //Elements.Add(new BaseEntity("20Cr4", "Stal stopowa (nawęglana i hartowana)", 325));
            //Elements.Add(new BaseEntity("20MNCr5", "Stal stopowa (nawęglana i hartowana)", 375));

            //Elements.Add(new BaseEntity("28Mn7", "Stal stopowa (ulepszana cieplnie)", 260));
            //Elements.Add(new BaseEntity("34Cr4", "Stal stopowa (ulepszana cieplnie)", 355));
            //Elements.Add(new BaseEntity("41Cr4", "Stal stopowa (ulepszana cieplnie)", 380));
            //Elements.Add(new BaseEntity("42CrMo4", "Stal stopowa (ulepszana cieplnie)", 430));

            //Elements.Add(new BaseEntity("L400", "Staliwo węglowe", 125));
            //Elements.Add(new BaseEntity("L500", "Staliwo węglowe", 150));
            //Elements.Add(new BaseEntity("L600", "Staliwo węglowe", 170));
            //Elements.Add(new BaseEntity("L650", "Staliwo węglowe", 180));

            //Elements.Add(new BaseEntity("EN-GJL-150", "Żeliwo szare", 145));
            //Elements.Add(new BaseEntity("EN-GJL-200", "Żeliwo szare", 195));
            //Elements.Add(new BaseEntity("EN-GJL-250", "Żeliwo szare", 245));
            //Elements.Add(new BaseEntity("EN-GJL-300", "Żeliwo szare", 290));
            //Elements.Add(new BaseEntity("EN-GJL-350", "Żeliwo szare", 340));

            //TakeListToFile();
        }

        public void DeleteMaterial(BaseEntity selectedMat)
        {
            bool x = Elements.Remove(selectedMat);
            TakeListToFile();
        }

        public void AddNewMaterial(BaseEntity newMaterial)
        {
            Elements.Add(newMaterial);
            TakeListToFile();
        }

        /// <summary>
        /// Take materials from list to file
        /// </summary>
        public void TakeListToFile()
        {
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "BaseEntity";
            root.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BaseEntity>), root);

            using StreamWriter streamWriter = new StreamWriter(path, false);
            xmlSerializer.Serialize(streamWriter, Elements);
            streamWriter.Close();
        }

        public void TakeFileToList()
        {
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "BaseEntity";
            root.IsNullable = true;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<BaseEntity>), root);
            string xml = File.ReadAllText(path);
            StringReader stringReader = new StringReader(xml);
            Elements = (List<BaseEntity>)xmlSerializer.Deserialize(stringReader);
        }

        public BaseEntity GetMaterialByName(string name)
        {
            BaseEntity selectedMaterial = Elements.Find(p => p.Content == name);
            return selectedMaterial;
        }
    }
}
