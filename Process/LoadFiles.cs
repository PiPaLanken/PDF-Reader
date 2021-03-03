using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PDF_Reader
{
    class LoadFiles
    {
        static List<string> PDF_Files = new List<string>();
        public static void GetFiles(string Path)
        {
            var directory = Directory.GetFiles(Path);
            foreach (string file in directory)
            {
                PDF_Files.Add(file);
                Console.WriteLine(file);
            }
            CheckFileType(PDF_Files);
        }

        private static List<string> CheckFileType(List<string> FileList)
        {
            foreach(string file in FileList)
            {
                if (!file.EndsWith(".pdf"))
                    FileList.Remove(file);
            }
            return FileList;
        }
    }
}
