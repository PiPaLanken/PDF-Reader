using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader
{
    public class Manager
    {
        string Path = null;
        public Manager(string FilePath)
        {
            Path = FilePath;
            Console.WriteLine(Path);
        }

        public void Steps()
        {

        }
    }
}
