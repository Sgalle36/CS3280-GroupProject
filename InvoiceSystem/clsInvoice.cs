using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InvoiceSystem
{
    /// <summary>
    /// Model for Invoices
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal class clsInvoice : INotifyPropertyChanged, INotifyCollectionChanged, IEquatable<clsInvoice>
    {
        /// <summary>
        /// The invoice number.
        /// </summary>
        private int? invoiceNum;

        /// <summary>
        /// The invoice date.
        /// </summary>
        private DateTime invoiceDate;

        /// <summary>
        /// The invoice total cost.
        /// </summary>
        private decimal totalCost;

        /// <summary>
        /// The invoice items.
        /// </summary>
        private ObservableCollection<clsItem> items;

        /// <summary>
        /// Event notifies when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Creates a new clsInvoice object.
        /// </summary>
        /// <param name="invoiceNum">The invoice number, null for new invoices that haven't been saved in the database yet.</param>
        /// <param name="invoiceDate">The date of the invoice.</param>
        /// <param name="totalCost">The total cost of the invoice.</param>
        /// <param name="items">Items belonging to the invoice.</param>
        public clsInvoice(int? invoiceNum, DateTime invoiceDate, decimal totalCost, List<clsItem> items)
        {
            if (invoiceNum != null)
            {
                InvoiceNum = (int)invoiceNum;
            }
            InvoiceDate = invoiceDate;
            TotalCost = totalCost;
            Items = new ObservableCollection<clsItem>(items);
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
        public decimal TotalCost
        {
            get { return items.Sum(i => i.Cost); }
            set { totalCost = value; }
        }

        /// <summary>
        /// Items belonging to the invoice.
        /// </summary>
        public ObservableCollection<clsItem> Items
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
        /// Notifies when a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notifies when a collection has changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)items).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)items).CollectionChanged -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(clsInvoice other)
        {
            if (other is null)
                return false;

            return this.invoiceNum == other.invoiceNum;
        }

        public override bool Equals(object obj) => Equals(obj as clsInvoice);
        public override int GetHashCode() => (invoiceNum).GetHashCode();
    }
}
