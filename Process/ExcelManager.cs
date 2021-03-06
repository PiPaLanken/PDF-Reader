using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace PDF_Reader.Process
{
    class ExcelManager
    {

        /*Microsoft.Office.Interop.Excel.Workbook workBook;
        Microsoft.Office.Interop.Excel.Worksheet workSheet;
        Microsoft.Office.Interop.Excel.Range cellRange;*/

       public static void WriteDataToExcel(string destinationPath)
       {

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
                var excelWorkBook = excel.Workbooks.Add(Type.Missing);
                if (excel == null)
                    throw new Exception("Excel is not properly installed!!");
                excelWorkBook.SaveAs(exportPath);
                return null;
            }
            catch(Exception e)
            {
                throw new Exception("Creation failed: "+e.Message);
            }
        }
    }
}
