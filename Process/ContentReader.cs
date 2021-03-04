using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using PDF_Reader.Classes;

namespace PDF_Reader.Process
{
    class ContentReader
    {
        public static Dictionary<string,List<string>> FilterBy(List<string> pdf_Files,List<string> types)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            List<string> Files = new List<string>();
            Files = pdf_Files;
            foreach (string type in types)
            {
                result.Add(type, new List<string>());
            }
            foreach (string file in Files)
            {
                PdfDocument document = PdfDocument.Open(@file);
                foreach (Page page in document.GetPages())
                {
                    foreach (string type in types)
                    {
                        if (page.Text.Contains(type))
                            result[type].Add(file);
                    }
                }
            }
            return result;
        }
        public static List<Document> GetDocumentsOutOfTypePaths(Dictionary<string, List<string>> typePaths)
        {
            foreach (string path in typePaths["Kauf"])
            {
                GetBasicInformationOutOfPath(path);
            }
            foreach (string path in typePaths["Verkauf"])
            {
                GetSellInformationOutOfPath(path);
            }
            return null;
        } 
        public static Document GetBasicInformationOutOfPath(string path)
        {
            string date = GetValueOutOfPDFBetween("Datum:", "Sven", path);
            string name = GetValueOutOfPDFBetween("Wertpapierbezeichnung","Nominale", path);
            float shares =  StringToFloat(GetValueOutOfPDFBetween("Stück", "Kurs", path));
            float sharePrice = StringToFloat(GetValueOutOfPDFBetween("Kurs", "Handelsplatz", path));
            float provision = StringToFloat(GetValueOutOfPDFBetween("Provision", "Endbetrag", path));
            float finalAmount = StringToFloat(GetValueOutOfPDFBetween("Lasten", "Abrechnungs-IBAN", path));

            return new Document(date,name,shares,sharePrice,provision,finalAmount);
        }

        public static String GetValueOutOfPDFBetween(string name1, string name2,string path)
        {
            PdfDocument pdfDocument = PdfDocument.Open(@path);
            List<string> wordsInDocument = GetWordsOutOfDocument(pdfDocument);
            string result = FilterWordToVariable(wordsInDocument, name1, name2);
            return result;
        }

        public static List<string> GetWordsOutOfDocument(PdfDocument pdfDocument)
        {
            List<string> result = new List<string>();
            foreach (Page page in pdfDocument.GetPages())
            {
                foreach (Word word in page.GetWords())
                {
                    result.Add(Convert.ToString(word));
                }
            }
            return result;
        }

        public static SellDoc GetSellInformationOutOfPath(string path)
        {
            return null;
        }

        public static string FilterWordToVariable(List<string> wordsInDocument, string beforeValue, string afterValue)
        {
            string result = "";
            List<string> nameBlocks = new List<string>();
            int min = wordsInDocument.IndexOf(beforeValue);
            int max = wordsInDocument.IndexOf(afterValue);
            int diff = max - min;
            if (min > 0 && max > 0)
                for (int x = 1; x < diff; x++)
                {
                    int number = min + x;
                    nameBlocks.Add(wordsInDocument[number]);
                }
            foreach (string name in nameBlocks)
            {
                result += name + " ";
            }
            return result;
        }
        public static float StringToFloat(string value)
        {
            string result = Regex.Replace(value, "[^0-9.,+-]", "");
            return Single.Parse(result);
        }
    }
}
