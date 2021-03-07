using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PDF_Reader.Classes;
using Excel = Microsoft.Office.Interop.Excel;

namespace PDF_Reader.Process
{
    class ExcelManager
    {
        public static void WriteDataIntoExcel(List<Document> documents)
        {
            foreach (Document doc in documents)
            {
                if (doc.Name== "SellDoc")
                {

                }
            }
        }

        public static string CreateFile(string destinationPath, string customExportName)
        {
            string exportFileType = ".xlsx";
            string exportPathWithCustomName = destinationPath + customExportName;
            if (File.Exists(exportPathWithCustomName+exportFileType))
            {
                int n = 1;
                while (File.Exists(exportPathWithCustomName+ "_" + n+exportFileType))
                    n++;
                exportPathWithCustomName +="_"+ n;
            }
            CreateExcelFile(exportPathWithCustomName + exportFileType);
            return exportPathWithCustomName + exportFileType;
        }

        public static string CreateExcelFile(string exportPath)
        {
            try
            {
                Excel.Application excel = new Excel.Application();
                CreateExcelLayout(excel,exportPath).Quit();
                return exportPath;
            }
            catch(Exception e)
            {
                throw new Exception("Creation failed: "+e.Message);
            }
        }
        public static System.Data.DataTable SetTableValues()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Datum", typeof(DateTime));
            table.Columns.Add("Aktie", typeof(string));
            table.Columns.Add("Anzahl", typeof(float));
            table.Columns.Add("Kurs", typeof(float));
            table.Columns.Add("Kurswert", typeof(float));
            //if (verkauf)
            //table.Columns.Add("Kapitalsertragssteuer", typeof(float));
            //table.Columns.Add("Kirchensteuer", typeof(float));
            //table.Columns.Add("Solidaritätssteuer", typeof(float));
            table.Columns.Add("Provision", typeof(float));
            table.Columns.Add("Endbetrag", typeof(float));

            return table;
        }
        public static Excel.Application CreateExcelLayout(Excel.Application excel, string exportPath)
        {
            Microsoft.Office.Interop.Excel.Range cellRange;
            Excel.Workbook excelWorkBook = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)excel.ActiveSheet;
            worksheet.Name = "Kauf";
            //worksheet.Name = "Verkauf";
            worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, 7]].Merge();
            worksheet.Cells[1, 1] = "Aktien - Käufe";
            //worksheet.Cells[1, 1] = "Aktien - Verkauf";
            worksheet.Cells.Font.Size = 15;

            int rowcount = 2;

            foreach (System.Data.DataRow dataRow in SetTableValues().Rows)
            {
                rowcount += 1;
                for (int i=1; i<=SetTableValues().Columns.Count; i++)
                {
                    if (rowcount == 3)
                    {
                        worksheet.Cells[2, i] = SetTableValues().Columns[i - i].ColumnName;
                        worksheet.Cells.Font.Color = System.Drawing.Color.Black;
                    }
                    worksheet.Cells[rowcount, i] = dataRow[i - 1].ToString();
                    if (rowcount > 3)
                    {
                        if (i== SetTableValues().Columns.Count) 
                        { 
                            if (rowcount %2 == 0)
                            {
                                cellRange = worksheet.Range[worksheet.Cells[rowcount, 1], worksheet.Cells[rowcount, SetTableValues().Columns.Count]];
                            }
                        }
                    }
                }
            }
            cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rowcount, SetTableValues().Columns.Count]];
            cellRange.EntireColumn.AutoFit();
            Microsoft.Office.Interop.Excel.Borders border = cellRange.Borders;
            border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;

            cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[2, SetTableValues().Columns.Count]];

            excelWorkBook.SaveAs(exportPath);
            excelWorkBook.Close();
            return excel;

            //fehlt nur workbook closed und return excel;
        }
    }
}
