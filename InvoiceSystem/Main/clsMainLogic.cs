using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Main Window Logic
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal class clsMainLogic : INotifyCollectionChanged
    {
        /// <summary>
        /// Tracks the current line item of the invoice
        /// </summary>
        private int lineItem;

        /// <summary>
        /// The current invoice loaded in the system.
        /// </summary>
        public clsInvoice Invoice { get; set; }

        /// <summary>
        /// All available items.
        /// </summary>
        public ObservableCollection<clsItem> Items { get; set; }

        /// <summary>
        /// Create a new main logic instance without a specific invoice.
        /// </summary>
        public clsMainLogic()
        {
            try
            {
                Invoice = new clsInvoice(null, DateTime.Now, 0, new List<clsItem>());
                Items = new ObservableCollection<clsItem>(GetAllItems());
                lineItem = 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new main logic instance loaded with a specific invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        public clsMainLogic(int invoiceNum)
        {
            try
            {
                Invoice = GetInvoice(invoiceNum);
                Items = new ObservableCollection<clsItem>(GetAllItems());
                lineItem = Invoice.Items.Count + 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }

        }

        /// <summary>
        /// Get the information for the given invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        /// <returns>An invoice item with the number, date, and total cost of the invoice.</returns>
        public static clsInvoice GetInvoice(int invoiceNum)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of all the items that have been billed on the given invoice.
        /// </summary>
        /// <param name="invoiceNum">The number of the invoice.</param>
        /// <returns>A list of items belonging to that invoice.</returns>
        public static List<clsItem> GetInvoiceItems(int invoiceNum)
        {
            try
            {
                List<clsItem> items = new List<clsItem>();

                int rows = 0;
                DataSet ds = App.db.ExecuteSQLStatement(clsMainSQL.GetInvoiceItems(invoiceNum), ref rows);
                DataRow[] dr = ds.Tables[0].AsEnumerable().ToArray();
                foreach (DataRow row in dr)
                {
                    items.Add(new clsItem((string)row.ItemArray[0], (string)row.ItemArray[1], (int)row.ItemArray[2], (Decimal)row.ItemArray[3]));
                }

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a list of all the available items in the database.
        /// </summary>
        /// <returns>A list of all items in the database.</returns>
        public static List<clsItem> GetAllItems()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Refreshes items with updated descriptions and costs.
        /// </summary>
        public void RefreshItemData()
        {
            try
            {
                Items = new ObservableCollection<clsItem>(GetAllItems());

                if (Invoice.InvoiceNum != null)
                {
                    Invoice.Items = new ObservableCollection<clsItem>(GetInvoiceItems((int)Invoice.InvoiceNum));
                }
                else //new invoice
                {
                    Invoice.Items.ToList().ForEach(item =>
                    {
                        item.ItemDesc = Items.FirstOrDefault(updated => item.ItemCode == updated.ItemCode).ItemDesc;
                        item.Cost = Items.FirstOrDefault(updated => item.ItemCode == updated.ItemCode).Cost;
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new invoice with all of the listed items.
        /// </summary>
        public void CreateInvoice()
        {
            try
            {
                if (Invoice != null)
                {
                    App.db.ExecuteNonQuery(clsMainSQL.CreateInvoice(Invoice.InvoiceDate, Invoice.Items.Sum(i => i.Cost)));
                    int.TryParse(App.db.ExecuteScalarSQL(clsMainSQL.GetInvoiceNumber()), out int num);
                    Invoice.InvoiceNum = num;

                    for (int i = 0; i < Invoice.Items.Count; i++)
                    {
                        App.db.ExecuteNonQuery(clsMainSQL.AddItem((int)Invoice.InvoiceNum, i + 1, Invoice.Items.ElementAt(i).ItemCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an invoice and its items.
        /// </summary>
        public void SaveInvoice()
        {
            try
            {
                if (Invoice != null)
                {
                    App.db.ExecuteNonQuery(clsMainSQL.UpdateInvoice((int)Invoice.InvoiceNum, Invoice.InvoiceDate, Invoice.TotalCost));
                    App.db.ExecuteNonQuery(clsMainSQL.RemoveAllItems((int)Invoice.InvoiceNum));

                    for (int i = 0; i < Invoice.Items.Count; i++)
                    {
                        App.db.ExecuteNonQuery(clsMainSQL.AddItem((int)Invoice.InvoiceNum, i + 1, Invoice.Items.ElementAt(i).ItemCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Add the item to the invoice and remove it from available invoice items.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(clsItem item)
        {
            try
            {
                clsItem invoiceItem = new clsItem(item.ItemCode, item.ItemDesc, lineItem++, item.Cost);
                Invoice.Items.Add(invoiceItem);
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Remove the item from the invoice and add it to the available invoice items.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(clsItem item)
        {
            try
            {
                Invoice.Items.Remove(item);
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }
        /// <summary>
        /// Notifies when a collection has changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                try
                {
                    ((INotifyCollectionChanged)Items).CollectionChanged += value;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
                }
            }

            remove
            {
                try
                {
                    ((INotifyCollectionChanged)Items).CollectionChanged -= value;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
                }
            }
        }
    }
}
