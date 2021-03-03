using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace PDF_Reader
{
    class ContentReader
    {
        List<string> Kauf = new List<string>();
        List<string> Verkauf = new List<string>();
        List<string> Files = new List<string>();
        public ContentReader(List<string> PDF_Files)
        {
            Files = PDF_Files;
            foreach (string file in Files)
            {
                ReadContent(file);
            }
        }
        private void ReadContent(string file)
        {
            PdfDocument document = PdfDocument.Open(@file);
            foreach (Page page in document.GetPages())
            {
                if (page.Text.Contains("Kauf"))
                    Kauf.Add(file);
                else if (page.Text.Contains("Verkauf"))
                    Verkauf.Add(file);
                else break;
            }
        }
    }
}
