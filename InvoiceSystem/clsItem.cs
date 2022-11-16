using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    internal class clsItem
    {
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public decimal Cost { get; set; }

        public clsItem(string itemCode, string itemDesc, decimal cost)
        {
            ItemCode = itemCode;
            ItemDesc = itemDesc;
            Cost = cost;
        }
    }
}
