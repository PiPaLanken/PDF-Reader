using System;

namespace PDF_Reader
{
    public class PDFReader
    {
        const bool TestMode = true;
        static string Path = "";
        static string DestinationPath = "";

        public PDFReader(string ImportPath, string ExportPath)
        {
            Path = ImportPath;
            DestinationPath = ExportPath;
            Start();
        }

        static void Main(string[] args)
        {
            if (TestMode)
                Path = @"C:\Users\mayer\Pictures\Uplay\";
            Start();
        }

        private static void Start()
        {
            FileLoader.GetFiles(Path);
            //Manager manager = new Manager(Path);
        }
        

    }
}
