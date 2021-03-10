using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader.Classes
{
    class PurchaseDoc:Document
    {
        public float TradingFee = 0;
        public PurchaseDoc(string date, string name, float shares, float shareprice,float courtage, float tradingPlaceFee, float provision, float finalAmount) : base(date, name, shares, shareprice,courtage,tradingPlaceFee, provision, finalAmount)
        {
            
        }
        public PurchaseDoc(Document bDoc, float tradingFee) : base(bDoc.Date, bDoc.Name, bDoc.Shares, bDoc.SharePrice,bDoc.Courtage ,bDoc.TradingPlaceFee, bDoc.Provision, bDoc.FinalAmount)
        {
            TradingFee = tradingFee;
        }
    }
}
