using System;
using System.Reflection;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Main Window SQL
    /// <author>Natalie Mueller</author>
    /// </summary>
    internal static class clsMainSQL
    {
        /// <summary>
        /// Generates an SQL query to create a new invoice.
        /// </summary>
        /// <param name="InvoiceDate"></param>
        /// <param name="TotalCost"></param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string CreateInvoice(DateTime InvoiceDate, decimal TotalCost)
        {
            try
            {
                string sSQL = $"INSERT INTO Invoices (InvoiceDate, TotalCost) Values ('{InvoiceDate.ToShortDateString()}', {TotalCost})";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to get the most recent invoice number.
        /// </summary>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string GetInvoiceNumber()
        {
            try
            {
                string sSQL = $"SELECT MAX(InvoiceNum) FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to get the information for the specified invoice.
        /// </summary>
        /// <param name="InvoiceNum">The invoice number</param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string GetInvoice(int InvoiceNum)
        {
            try
            {
                string sSQL = $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {InvoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to update the total cost of the invoice.
        /// </summary>
        /// <param name="InvoiceNum">The invoice number</param>
        /// <param name="TotalCost"></param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string UpdateInvoice(int InvoiceNum, DateTime InvoiceDate, decimal TotalCost)
        {
            try
            {
                string sSQL = $"UPDATE Invoices SET InvoiceDate = '{InvoiceDate.ToShortDateString()}', TotalCost = {TotalCost} WHERE InvoiceNum = {InvoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to add a new item to the specified invoice.
        /// </summary>
        /// <param name="InvoiceNum">The invoice number</param>
        /// <param name="LineItemNum">The invoice line item number</param>
        /// <param name="ItemCode">The item code</param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string AddItem(int InvoiceNum, int LineItemNum, string ItemCode)
        {
            try
            {
                string sSQL = $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values ({InvoiceNum}, {LineItemNum}, '{ItemCode}')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to get all of the items belonging to the specified invoice.
        /// </summary>
        /// <param name="InvoiceNum">The invoice number</param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string GetInvoiceItems(int InvoiceNum)
        {
            try
            {
                string sSQL  = $"SELECT ItemDesc.ItemCode, ItemDesc.ItemDesc, LineItems.LineItemNum, ItemDesc.Cost FROM LineItems, ItemDesc ";
                sSQL        += $"WHERE LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {InvoiceNum} ";
                sSQL        += $"ORDER BY LineItems.LineItemNum";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to remove all items from the given invoice.
        /// </summary>
        /// <param name="InvoiceNum">The invoice number</param>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string RemoveAllItems(int InvoiceNum)
        {
            try
            {
                string sSQL = $"DELETE FROM LineItems WHERE InvoiceNum = {InvoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Generates an SQL query to get all available items for invoicing.
        /// </summary>
        /// <returns>The SQL Query</returns>
        /// <exception cref="Exception">Was unable to create the SQL Query</exception>
        public static string GetAllItems()
        {
            try
            {
                string sSQL = $"SELECT ItemCode, ItemDesc, Cost from ItemDesc ORDER BY ItemCode";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
