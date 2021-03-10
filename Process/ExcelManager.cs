using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PDF_Reader.Classes;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;



namespace PDF_Reader.Process
{
    class ExcelManager
    {
        private static List<Document> Documents;
        private static bool isBuy = true;

        public ExcelManager()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static string CreateFilePath(string destinationPath, string customExportName)
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
            return exportPathWithCustomName + exportFileType;
        }

        public static string CreateExcelFileWithData(string exportPath, List<Document> documents)
        {
            Documents = documents;
            ManageExcelCreate(exportPath);
            return exportPath;
        }
        public static void ManageExcelCreate(string exportPath)
        {
            using (var package = new ExcelPackage())
            {
                CreateExcelBuyLayout(exportPath,package);
                isBuy = false;
                CreateExcelSellLayout(exportPath, package);
                FileInfo file = new FileInfo(exportPath);
                package.SaveAs(file);
            }
                
        }

        public static bool CreateExcelBuyLayout(string exportPath, ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.Add("Buying");
            worksheet.Name = "Kauf";
            worksheet.Cells[1, 1].Value = "Aktien - Käufe";
            worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1, 1, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dashed);
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, 8].Merge = true;
            int rowCount = 2;

            foreach (System.Data.DataRow dataRow in CreateTableLayout().Rows)
            {
                rowCount += 1;
                for (int i =1; i<= CreateTableLayout().Columns.Count; i++)
                {
                    if (rowCount == 3)
                        worksheet.Cells[2, i].Value = CreateTableLayout().Columns[i - 1].ColumnName;
                    worksheet.Cells[rowCount, i].Value = dataRow[i - 1].ToString();
                }
            }
            return true;
        }
        public static bool CreateExcelSellLayout(string exportPath, ExcelPackage package)
        {
            var worksheet = package.Workbook.Worksheets.Add("Selling");
            worksheet.Name = "Verkauf";
            worksheet.Cells[1, 1].Value = "Aktien - Verkäufe";
            worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Cells[1, 1, 1, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Dashed);
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, 11].Merge = true;

            int rowCount = 2;
            foreach (System.Data.DataRow dataRow in CreateTableLayout().Rows)
            {
                rowCount += 1;
                for (int i = 1; i <= CreateTableLayout().Columns.Count; i++)
                {
                    if (rowCount == 3)
                        worksheet.Cells[2, i].Value = CreateTableLayout().Columns[i - 1].ColumnName;
                    worksheet.Cells[rowCount, i].Value = dataRow[i - 1].ToString();
                }
            }

            return true;
        }

        public static System.Data.DataTable CreateTableLayout()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Datum", typeof(DateTime));
            table.Columns.Add("Aktie", typeof(string));
            table.Columns.Add("Anzahl", typeof(float));
            table.Columns.Add("Kurs", typeof(float));
            table.Columns.Add("Kurswert", typeof(float));

            if (!isBuy)
            {
                table.Columns.Add("Kapitalsertragssteuer", typeof(float));
                table.Columns.Add("Kirchensteuer", typeof(float));
                table.Columns.Add("Solidaritätssteuer", typeof(float));
            }

            table.Columns.Add("Provision", typeof(float));
            table.Columns.Add("Endbetrag", typeof(float));
            
            foreach (Document doc in Documents)
            {
                if (doc.GetType() == typeof(SellDoc)&&!isBuy)
                {
                    var sellDoc = (SellDoc)doc;
                    table.Rows.Add(sellDoc.Date, sellDoc.Name, sellDoc.Shares, sellDoc.SharePrice, 0f, sellDoc.CapitalTax, sellDoc.ChurchTax, sellDoc.SolidTax, sellDoc.Provision, sellDoc.FinalAmount);
                }
                if (doc.GetType() == typeof(PurchaseDoc)&&isBuy)
                    table.Rows.Add(doc.Date, doc.Name, doc.Shares, doc.SharePrice, 0f, doc.Provision, doc.FinalAmount);
            }
            return table;
        }
    }
}
