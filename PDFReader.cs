using System;

namespace PDF_Reader
{
    public class PDFReader
    {
        const bool TestMode = true;
        static string Path = "";

        public PDFReader(string ImportPath)
        {
            Path = ImportPath;
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
            LoadFiles.GetFiles(Path);
            //Manager manager = new Manager(Path);
        }
        

    }
}
