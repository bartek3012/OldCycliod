using Microsoft.Win32;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Backend.Serivce
{
    public static class FileService
    {
        public static void SaveFile(string dataToSave)
        {
            string path ="";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "cyc";
            saveFileDialog.Title = "Zapisz postępy w pracy";
            saveFileDialog.Filter = "OldCycloid File(*.oldh)|*.oldh";
            saveFileDialog.FilterIndex = 1;

            //if(!string.IsNullOrEmpty(path))
            //{
            //    saveFileDialog.InitialDirectory = Path.GetDirectoryName(path);
            //    saveFileDialog.FileName = Path.GetFileName(path);
            //}
            bool? result = saveFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                path = saveFileDialog.FileName;
                File.WriteAllText(path, dataToSave);
            }
        }

        public static string[] OpenFile()
        {
            string path = "";
            string []openFileText = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz plik do otwarcia";
            openFileDialog.DefaultExt = "oldh";
            openFileDialog.Filter = "Pliki programu OldCycliod (*.oldh)|*.oldh";
            openFileDialog.FilterIndex = 1;

            bool? result = openFileDialog.ShowDialog();
            if(result.HasValue && result.Value)
            {
                path = openFileDialog.FileName;
                openFileText = File.ReadAllLines(path);
                //List<string> openFileTextList = new List<string>(qopenFileText);
            }
            return openFileText;
        }
    }
}
