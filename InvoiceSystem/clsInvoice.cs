using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    internal class clsInvoice
    {
        public int InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int TotalCost { get; set; }
        public List<clsItem> Items { get; set; }

        public clsInvoice(int invoiceNum, DateTime invoiceDate, int totalCost, List<clsItem> items)
        {
            InvoiceNum = invoiceNum;
            InvoiceDate = invoiceDate;
            TotalCost = totalCost;
            Items = items;
        }
    }
}
