using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PDF_Reader.Process
{
    class FileLoader
    {
        public static List<string> GetPDFFiles(string Path)
        {
            List<string> PDF_Files = new List<string>();
            var directory = Directory.GetFiles(Path);
            foreach (string file in directory)
            {
                PDF_Files.Add(file);
                Console.WriteLine(file);
            }
            Console.WriteLine("\n");
            return CheckFileType(PDF_Files);
            //ContentReader readContent = new ContentReader(PDF_Files);
        }

        private static List<string> CheckFileType(List<string> ListToCheck)
        {
            List<string> WrongFiles = new List<string>();
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
