using System;
using System.Collections.Generic;
using System.Text;

namespace PDF_Reader.Classes
{
    class Document
    {
        public string Date = null;
        public string Name = null;
        public float Shares =0;
        public float SharePrice = 0f;
        public float Provision = 0f;
        public float FinalAmount = 0f;
        public Document(string date,  string name, float shares, float sharePrice, float provision, float finalAmount)
        {
            Date = date;
            Name = name;
            Shares = shares;
            SharePrice = sharePrice;
            Provision = provision;
            FinalAmount = finalAmount;
        }

    }
}
