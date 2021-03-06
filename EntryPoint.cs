using System;
using System.Collections.Generic;
using PDF_Reader.Process;
using PDF_Reader.Classes;

namespace PDF_Reader
{
    public class EntryPoint
    {
        const bool TESTMODE = true;
        public EntryPoint()
        {
        }

        static void Main(string[] args)
        {
            string path, destPath, customExpertName;
            if (TESTMODE)
            {
                path = @"C:\Users\mayer\Pictures\Uplay\";
                destPath = @"C:\Users\mayer\Pictures\Uplay\Export\";
                customExpertName = "NiceFile";
            }
            
            Start(path, destPath, customExpertName);

        }

        public static void Start(string importPath, string exportPath, string customExportName)
        {
            List<string> pdfFiles = FileLoader.GetPDFFiles(importPath);
            List<string> filterTypes = new List<string>() { "Kauf", "Verkauf" };
            Dictionary<string, List<string>> filteredPDFFiles = ContentReader.FilterBy(pdfFiles, filterTypes);
            List<Document> documents = ContentReader.GetDocumentsOutOfTypePaths(filteredPDFFiles);
            string excelPath = ExcelManager.CreateFile(exportPath, customExportName);

        }
    }
}
