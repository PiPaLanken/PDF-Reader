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
                result.Add(type, new List<string>());
            foreach (string file in Files)
            {
                PdfDocument document = PdfDocument.Open(@file);
                foreach (Page page in document.GetPages())
                    foreach (string type in types)
                        if (page.Text.Contains(type))
                            result[type].Add(file);
            }
            return result;
        }
        public static List<Document> GetDocumentsOutOfTypePaths(Dictionary<string, List<string>> typePaths)
        {
            List<Document> documents = new List<Document>();
            foreach (string path in typePaths["Kauf"])
            {
                documents.Add(GetBuyInformationOutOfPath(path));
            }
            foreach (string path in typePaths["Verkauf"])
            {
                documents.Add(GetSellInformationOutOfPath(path));
            }
            return documents;
        } 
        public static Document GetBasicInformationOutOfPath(string path)
        {
            string date = GetValueOutOfPDFBetween("Datum:", "Sven", path,0);
            string name = GetValueOutOfPDFBetween("Wertpapierbezeichnung","Nominale", path,0);
            float shares =  StringToFloat(GetValueOutOfPDFBetween("Stück", "Kurs", path, 0));
            float sharePrice = StringToFloat(GetValueOutOfPDFBetween("Kurs", "Handelsplatz", path, 0));
            float courtage = StringToFloat(GetValueOutOfPDFBetween("Courtage", "Handelsplatzgebühr", path, 0));
            float tradingPlaceFee = StringToFloat(GetValueOutOfPDFBetween("Handelsplatzgebühr", "Provision", path, 0));
            float provision = StringToFloat(GetValueOutOfPDFBetween("Provision", "Endbetrag", path, 0));
            float finalAmount = StringToFloat(GetValueOutOfPDFBetween("Endbetrag", "Abrechnungs-IBAN", path, 0));

            return new Document(date,name,shares,sharePrice,courtage,tradingPlaceFee, provision,finalAmount);
        }
        public static PurchaseDoc GetBuyInformationOutOfPath(string path)
        {
            Document document = GetBasicInformationOutOfPath(path);
            float tradingFee = StringToFloat(GetValueOutOfPDFBetween("Endbetrag", "Abrechnungs-IBAN", path, 0));

            return new PurchaseDoc(document, tradingFee);
        }

        public static SellDoc GetSellInformationOutOfPath(string path)
        {
            Document document = GetBasicInformationOutOfPath(path);
            float CapitalTax = StringToFloat(GetValueOutOfPDFBetween("Kapitalertragsteuer", "Kirchensteuer", path, 1));
            float ChurchTax = StringToFloat(GetValueOutOfPDFBetween("Kirchensteuer", "Solidaritätszuschlag", path, 1));
            float solidTax = StringToFloat(GetValueOutOfPDFBetween("Solidaritätszuschlag", "Provision", path, 1));


            return new SellDoc(document,CapitalTax,ChurchTax,solidTax);
        }

        public static String GetValueOutOfPDFBetween(string name1, string name2,string path, int skipNumericWordsAfterBeforeValue)
        {
            PdfDocument pdfDocument = PdfDocument.Open(@path);
            List<string> wordsInDocument = GetWordsOutOfDocument(pdfDocument);
            string result = FilterWordToVariable(wordsInDocument, name1, name2, skipNumericWordsAfterBeforeValue);
            return result;
        }

        public static List<string> GetWordsOutOfDocument(PdfDocument pdfDocument)
        {
            List<string> result = new List<string>();
            foreach (Page page in pdfDocument.GetPages())
                foreach (Word word in page.GetWords())
                    result.Add(Convert.ToString(word));
            return result;
        }

        public static string FilterWordToVariable(List<string> wordsInDocument, string beforeValue, string afterValue, int skipNumericWordsAfterBeforeValue)
        {
            afterValue = ReplaceAfterValueByHandelsentgeldIfItExists(wordsInDocument, beforeValue, afterValue);
            string result = "";
            List<string> nameBlocks = new List<string>();
            int min = wordsInDocument.IndexOf(beforeValue);
            int max = wordsInDocument.IndexOf(afterValue);
            min += skipNumericWordsAfterBeforeValue;
            int diff = max - min;
            if (min > 0 && max > 0)
            {
                for (int x = 1; x < diff; x++)
                {
                    int number = min + x;
                    nameBlocks.Add(wordsInDocument[number]);
                }
                foreach (string name in nameBlocks)
                    result += name + " ";
            }
            else if (!CheckIfTaxesAreIncluded(wordsInDocument)||!CheckIfFeesAreIncluded(beforeValue))
                return "0";
            return result;
        }

        public static bool CheckIfTaxesAreIncluded(List<string> WordsInDocument)
        {
            List<string> Words = WordsInDocument;
            foreach (string word in Words)
                if (word.Contains("Kapitalertragsteuer"))
                    return true;
            return false;
        }

        public static string ReplaceAfterValueByHandelsentgeldIfItExists(List<string> WordsInDocument, string beforeValue, string afterValue)
        {
            if (beforeValue == "Provision")
            {
                List<string> Words = WordsInDocument;
                foreach (string word in Words)
                    if (word.Contains("Handelsentgelt"))
                        return "Handelsentgelt";
            }
            return afterValue;
        }

        public static bool CheckIfFeesAreIncluded(string beforeValue)
        {
            switch (beforeValue)
            {
                case "Handelsplatzgebühr":
                    return true;
                case "Courtage":
                    return true;
                case "Handelsentgelt":
                    return true;
                default:
                    return false;
            }
        }

        public static float StringToFloat(string value)
        {
            string result = Regex.Replace(value, "[^0-9.,+-]", "");
            return Single.Parse(result);
        }
    }
}
