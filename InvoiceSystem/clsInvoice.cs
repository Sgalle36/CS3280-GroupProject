using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem
{
    /// <summary>
    /// Model for Invoices
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal class clsInvoice : INotifyPropertyChanged
    {
        private int? invoiceNum;
        private DateTime invoiceDate;
        private int totalCost;
        private List<clsItem> items;

        /// <summary>
        /// Creates a new clsInvoice object.
        /// </summary>
        /// <param name="invoiceNum">The invoice number, null for new invoices that haven't been saved in the database yet.</param>
        /// <param name="invoiceDate">The date of the invoice.</param>
        /// <param name="totalCost">The total cost of the invoice.</param>
        /// <param name="items">Items belonging to the invoice.</param>
        public clsInvoice(int? invoiceNum, DateTime invoiceDate, int totalCost, List<clsItem> items)
        {
            if (invoiceNum != null)
            {
                InvoiceNum = (int)invoiceNum;
            }
            InvoiceDate = invoiceDate;
            TotalCost = totalCost;
            Items = items;
        }

        /// <summary>
        /// The number of the invoice, null if the invoice has not been saved in the database yet.
        /// </summary>
        public int? InvoiceNum 
        { 
            get { return invoiceNum; } 
            set
            { 
                if (value != invoiceNum)
                {
                    invoiceNum = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// The date of the invoice.
        /// </summary>
        public DateTime InvoiceDate 
        {
            get { return invoiceDate; } 
            set
            {
                if (value != invoiceDate) 
                {
                    invoiceDate = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// The total cost of the invoice.
        /// </summary>
        public int TotalCost 
        {
            get { return totalCost; } 
            set
            { 
                if (value != totalCost)
                {
                    totalCost = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Items belonging to the invoice.
        /// </summary>
        public List<clsItem> Items 
        {
            get { return items; } 
            set
            {
                if (value != items)
                {
                    items = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Event notifies when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
