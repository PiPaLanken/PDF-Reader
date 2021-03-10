using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader.Classes
{
    class SellDoc : Document
    {
        public float CapitalTax = 0;
        public float ChurchTax = 0;
        public float SolidTax = 0;
        public Document Doc;
        public SellDoc(string date,string name, float shares, float shareprice, float courtage, float tradingPlaceFee,float provision, float finalAmount, float capitalTax, float churchTax, float solidTax) : base(date, name, shares, shareprice,courtage, tradingPlaceFee, provision, finalAmount)
        {
            
        }
        public SellDoc(Document bDoc, float capitalTax, float churchTax, float solidTax) : base(bDoc.Date, bDoc.Name, bDoc.Shares, bDoc.SharePrice,bDoc.Courtage,bDoc.TradingPlaceFee, bDoc.Provision, bDoc.FinalAmount)
        {
            Doc = bDoc;
            CapitalTax = capitalTax;
            ChurchTax = churchTax;
            SolidTax = solidTax;
        }
    }
}
