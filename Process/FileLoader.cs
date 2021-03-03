using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PDF_Reader
{
    class FileLoader
    {
        static List<string> PDF_Files = new List<string>();
        static List<string> WrongFiles = new List<string>();
        public static void GetFiles(string Path)
        {
            var directory = Directory.GetFiles(Path);
            foreach (string file in directory)
            {
                PDF_Files.Add(file);
                Console.WriteLine(file);
            }
            Console.WriteLine("\n");
            PDF_Files = CheckFileType(PDF_Files);
            foreach (string file in PDF_Files)
            {
                Console.WriteLine(file);
            }
            ContentReader readContent = new ContentReader(PDF_Files);
        }

        private static List<string> CheckFileType(List<string> ListToCheck)
        {
            List<string> updatedList = new List<string>();
            foreach(string file in ListToCheck)
            {
                if (file.EndsWith(".pdf"))
                    updatedList.Add(file);
                else WrongFiles.Add(file);
            }
            return updatedList;
        }
    }
}
