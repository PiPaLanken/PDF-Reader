using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader.Classes
{
    class SellDoc : Document
    {
        public SellDoc(string date,string name, float shares, float shareprice, float provision, float finalAmount, float capitalTax, float churchTax, float solidTax) : base(date, name, shares, shareprice, provision, finalAmount)
        {
            
        }
        public SellDoc(Document bDoc, float CapitalTax, float ChurchTax, float solidTax) : base(bDoc.Date, bDoc.Name, bDoc.Shares, bDoc.SharePrice, bDoc.Provision, bDoc.FinalAmount)
        {

        }
    }
}
