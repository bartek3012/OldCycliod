using Backend.Entity;
using Backend.Results;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Backend.Serivce
{
    public class PdfService
    {
        public PdfService(Calculations _calculations)
        {
            calculations = _calculations;
            CreatePdf();
        }
        private Calculations calculations;

        private void CreatePdf()
        {
            PdfDocument document = new PdfDocument(); //stworzenie dokumentu
            PdfPage page = document.AddPage(); //stworzenie strony domumentu
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            XGraphics gfx = XGraphics.FromPdfPage(page); //narzędzie do pisania
            string pathGrfphic = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GearPdf.jpg");

            XFont fontMainTitle = new XFont("Verdana", 20, XFontStyle.Bold);
            XFont fontMainUnderTitle = new XFont("Verdana", 13, XFontStyle.Bold);
            XFont fontValue = new XFont("Verdana", 11);
            XFont fontValueBold = new XFont("Verdana", 10, XFontStyle.Bold);

            gfx.DrawImage(XImage.FromFile(pathGrfphic), 140, 60, 297.9, 329.9);
            gfx.DrawString("Raport z programu OldCyclodi", fontMainTitle, XBrushes.Black, new XRect(0, -380, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Dane wejściowe", fontMainUnderTitle, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString("Dane obliczone", fontMainUnderTitle, XBrushes.Black, new XRect(0, 230, page.Width, page.Height), XStringFormats.Center);

            if(calculations.allDataValue.GetValueByEnumName(EnumName.Mmax)< calculations.allDataValue.GetValueByEnumName(EnumName.Mz))
            {
                gfx.DrawString("Moment maksymalny jest mniejszy niż zadany", fontMainUnderTitle, XBrushes.DarkRed, new XRect(0, 340, page.Width, page.Height), XStringFormats.Center);

            }
            else
            {
                gfx.DrawString("Przekładnia jest w stanie przenieść zaplanowany moment obrotowy", fontMainUnderTitle, XBrushes.Green, new XRect(0, 340, page.Width, page.Height), XStringFormats.Center);
            }
            gfx.DrawString("Raport wygenerowany przez program OldCycloid", fontValue, XBrushes.Gray, new XRect(0, -10, page.Width, page.Height), XStringFormats.BottomCenter);

            string namesInput = GetName("input");
            namesInput += $"Naprężenia dop na ściskanie kc\n{calculations.selectedMaterial.Type}\nWarunki pracy";

            string valuesInput = GetValue("input");
            valuesInput += $"{calculations.selectedMaterial.Value}\n{calculations.selectedMaterial.Content}\n{calculations.selectedWorkCondition.EnWorkCondition.ToString()} ({calculations.selectedWorkCondition.Value})";

            string unitsInput = GetUnit("input");
            unitsInput += $"{calculations.selectedMaterial.Unit}\n(materiał)";

            string namesOutput = GetName("output");
            string valuesOutput = GetValue("output");
            string unitsOutput = GetUnit("output");

            XTextFormatter tf = new XTextFormatter(gfx);
            XRect rect1 = new XRect(100, 440, 230, 180);
            gfx.DrawRectangle(XBrushes.WhiteSmoke, rect1);
            tf.DrawString(namesInput, fontValue, XBrushes.Black, rect1, XStringFormats.TopLeft);

            XRect rect = new XRect(330, 440, 90, 180);
            gfx.DrawRectangle(XBrushes.LightGray, rect);
            tf.DrawString(valuesInput, fontValue, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(420, 440, 70, 180);
            gfx.DrawRectangle(XBrushes.WhiteSmoke, rect);
            tf.DrawString(unitsInput, fontValue, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect1 = new XRect(100, 670, 230, 46);
            gfx.DrawRectangle(XBrushes.WhiteSmoke, rect1);
            tf.DrawString(namesOutput, fontValue, XBrushes.Black, rect1, XStringFormats.TopLeft);

            rect = new XRect(330, 670, 90, 46);
            gfx.DrawRectangle(XBrushes.LightGray, rect);
            tf.DrawString(valuesOutput, fontValueBold, XBrushes.Black, rect, XStringFormats.TopLeft);

            rect = new XRect(420, 670, 70, 46);
            gfx.DrawRectangle(XBrushes.WhiteSmoke, rect);
            tf.DrawString(unitsOutput, fontValue, XBrushes.Black, rect, XStringFormats.TopLeft);

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "Wybierz lokalizacje zapisu raportu";
            fileDialog.DefaultExt = "pdf";
            fileDialog.Filter = "Pdf files(*.pdf)|*.pdf";
            try
            {
            if (fileDialog.ShowDialog() == true)
            {
                document.Save(fileDialog.FileName);
            }
            }
            catch (IOException e)
            {
                MessageBox.Show("Plik nie może być nadpisany, ponieważ jest teraz otwarty.\nZamknij plik i powtórz operacje", "Zamknij nadpisywany plik", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private string GetName(string type)
        {
            List<DataValue> names = calculations.allDataValue.GetValuesByType(type);
            string result = "";
            foreach (DataValue name in names)
            {
                result += $"{name.Content}\n";
            }
            return result;
        }
        private string GetValue(string type)
        {
            List<DataValue> values = calculations.allDataValue.GetValuesByType(type);
            string result = "";
            foreach (DataValue value in values)
            {
                result += $"{value.Value.ToString()}\n";
            }
            return result;
        }
        private string GetUnit(string type)
        {
            List<DataValue> units = calculations.allDataValue.GetValuesByType(type);
            string result = "";
            foreach (DataValue unit in units)
            {
                result += $"{unit.Unit}\n";
            }
            return result;
        }
    }

}
