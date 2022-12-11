using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        /// <summary>
        /// The item code.
        /// </summary>
        private string itemCode;

        /// <summary>
        /// The item description.
        /// </summary>
        private string itemDesc;

        /// <summary>
        /// The item cost.
        /// </summary>
        private decimal cost;

        /// <summary>
        /// Creates a new clsItem object.
        /// </summary>
        /// <param name="itemCode">The item code.</param>
        /// <param name="itemDesc">The description of the item.</param>
        /// <param name="cost">The cost of the item.</param>
        public clsItem(string itemCode, string itemDesc, decimal cost)
        {
            try
            {
                ItemCode = itemCode;
                ItemDesc = itemDesc;
                Cost = cost;
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
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

        /// <summary>
        /// Notifies when a property has been changed.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Override equals method.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>Whether this and obj are equal.</returns>
        public override bool Equals(object? obj)
        {
            try
            {
                if (obj == null) return false;
                if (obj.GetType() != GetType()) return false;
                return itemCode.Equals(((clsItem)obj).itemCode);
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the hash code of an item.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            try
            {
                return itemCode.GetHashCode();
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the string representation.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            try
            {
                return $"{itemCode} - {itemDesc}";
            }
            catch (Exception ex)
            {
                throw new Exception($"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name} -> {ex.Message}");
            }
        }
    }
}
