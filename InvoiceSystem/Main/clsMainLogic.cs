using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Main Window Logic
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal class clsMainLogic
    {
        /// <summary>
        /// The current invoice loaded in the system.
        /// </summary>
        public clsInvoice? Invoice { get; set; }

        /// <summary>
        /// The items of the current invoice.
        /// </summary>
        public ObservableCollection<clsItem>? InvoiceItems { get; set; }

        /// <summary>
        /// All available items.
        /// </summary>
        public ObservableCollection<clsItem> Items { get; set; }

        /// <summary>
        /// Create a new main logic instance without a specific invoice.
        /// </summary>
        public clsMainLogic()
        {
            Items = new ObservableCollection<clsItem>(GetAllItems());
        }

        /// <summary>
        /// Create a new main logic instance loaded with a specific invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        public clsMainLogic(int invoiceNum)
        {
            Invoice = GetInvoice(invoiceNum);
            InvoiceItems = new ObservableCollection<clsItem>(Invoice.Items);
            Items = new ObservableCollection<clsItem>(GetAllItems());
        }

        /// <summary>
        /// Get the information for the given invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        /// <returns>An invoice item with the number, date, and total cost of the invoice.</returns>
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

        /// <summary>
        /// Gets a list of all the items that have been billed on the given invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        /// <returns>A list of items belonging to that invoice.</returns>
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

        /// <summary>
        /// Gets a list of all the available items in the database.
        /// </summary>
        /// <returns>A list of all items in the database.</returns>
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
