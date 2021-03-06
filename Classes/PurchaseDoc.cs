using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader.Classes
{
    class PurchaseDoc:Document
    {
        public PurchaseDoc(string date, string name, float shares, float shareprice, float provision, float finalAmount) : base(date, name, shares, shareprice, provision, finalAmount)
        {
            
        }
    }
}
