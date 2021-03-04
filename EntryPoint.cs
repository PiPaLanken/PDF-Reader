using System;
using System.Collections.Generic;
using PDF_Reader.Process;
using PDF_Reader.Classes;

namespace PDF_Reader
{
    public class EntryPoint
    {
        const bool TESTMODE = true;
        static string Path = null;
        static string DestinationPath = null;

        public EntryPoint()
        {
        }

        static void Main(string[] args)
        {
            if (TESTMODE)
                Path = @"C:\Users\mayer\Pictures\Uplay\";
            
            List<string> pdfFiles = FileLoader.GetPDFFiles(Path);
            List<string> filterTypes = new List<string>() { "Kauf", "Verkauf" };
            Dictionary<string, List<string>> filteredPDFFiles = ContentReader.FilterBy(pdfFiles, filterTypes);
            List<Document> documents = ContentReader.GetDocumentsOutOfTypePaths(filteredPDFFiles);

        }

        public static void Start(string importPath, string exportPath)
        {
            Path = importPath;
            DestinationPath = exportPath;
            List<string> pdfFiles = FileLoader.GetPDFFiles(Path);
            List<string> filterTypes = new List<string>() { "Kauf", "Verkauf" };
            Dictionary<string, List<string>> filteredPDFFiles = ContentReader.FilterBy(pdfFiles, filterTypes);
        }
        

    }
}
