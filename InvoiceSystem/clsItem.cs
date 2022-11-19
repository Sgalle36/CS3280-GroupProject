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
    /// Model for Items
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal class clsItem : INotifyPropertyChanged
    {
        private string itemCode;
        private string itemDesc;
        private decimal cost;

        /// <summary>
        /// Creates a new clsItem object.
        /// </summary>
        /// <param name="itemCode">The item code.</param>
        /// <param name="itemDesc">The description of the item.</param>
        /// <param name="cost">The cost of the item.</param>
        public clsItem(string itemCode, string itemDesc, decimal cost)
        {
            ItemCode = itemCode;
            ItemDesc = itemDesc;
            Cost = cost;
        }

        /// <summary>
        /// The item code.
        /// </summary>
        public string ItemCode
    {
            get { return itemCode; }
            set
            {
                if (value != itemCode)
                {
                    itemCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string ItemDesc
    {
            get { return itemDesc; }
            set
            {
                if (value != itemDesc)
                {
                    itemDesc = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The cost of the item.
        /// </summary>
        public decimal Cost
{
            get { return cost; }
            set
            {
                if (value != cost)
                {
                    cost = value;
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
