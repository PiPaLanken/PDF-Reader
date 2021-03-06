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
                Excel.Workbook excelWorkBook = excel.Workbooks.Add(Type.Missing);
                if (excel == null)
                    throw new Exception("Excel is not properly installed!!");
                CreateExcelLayout(excel, excelWorkBook);
                excelWorkBook.SaveAs(exportPath);
                excel.Quit();
                return exportPath;
            }
            catch(Exception e)
            {
                throw new Exception("Creation failed: "+e.Message);
            }
        }
        public static void CreateExcelLayout(Excel.Application excel, Excel.Workbook excelWorkBook)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Test", typeof(string));


        }
    }
}
