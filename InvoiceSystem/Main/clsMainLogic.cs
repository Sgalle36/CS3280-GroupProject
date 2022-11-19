using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Main
{
    internal class clsMainLogic
    {
        public clsInvoice? Invoice { get; set; }
        public ObservableCollection<clsItem>? InvoiceItems { get; set; }
        public ObservableCollection<clsItem> Items { get; set; }

        public clsMainLogic()
        {
            Items = new ObservableCollection<clsItem>(GetAllItems());
        }

        public clsMainLogic(int invoiceNum)
        {
            Invoice = GetInvoice(invoiceNum);
            InvoiceItems = new ObservableCollection<clsItem>(Invoice.Items);
            Items = new ObservableCollection<clsItem>(GetAllItems());
        }

        public static clsInvoice GetInvoice(int invoiceNum)
        {
            int rows = 0;
            DataSet ds = App.db.ExecuteSQLStatement(clsMainSQL.GetInvoice(invoiceNum), ref rows);
            DataRow[] dr = ds.Tables[0].AsEnumerable().ToArray();

            return new clsInvoice
            (
                invoiceNum,
                (DateTime)dr[0].ItemArray[1],
                (int)dr[0].ItemArray[2],
                GetInvoiceItems(invoiceNum)
            );
        }

        public static List<clsItem> GetInvoiceItems(int invoiceNum)
        {
            List<clsItem> items = new List<clsItem>();

            int rows = 0;
            DataSet ds = App.db.ExecuteSQLStatement(clsMainSQL.GetInvoiceItems(invoiceNum), ref rows);
            DataRow[] dr = ds.Tables[0].AsEnumerable().ToArray();
            foreach (DataRow row in dr)
            {
                items.Add(new clsItem((string)row.ItemArray[0], (string)row.ItemArray[1], (Decimal)row.ItemArray[2]));
            }

            return items;
        }

        public static List<clsItem> GetAllItems()
        {
            List<clsItem> items = new List<clsItem>();

            int rows = 0;
            DataSet ds = App.db.ExecuteSQLStatement(clsMainSQL.GetAllItems(), ref rows);
            DataRow[] dr = ds.Tables[0].AsEnumerable().ToArray();
            foreach (DataRow row in dr)
            {
                items.Add(new clsItem((string)row.ItemArray[0], (string)row.ItemArray[1], (Decimal)row.ItemArray[2]));
            }

            return items;
        }
    }
}
