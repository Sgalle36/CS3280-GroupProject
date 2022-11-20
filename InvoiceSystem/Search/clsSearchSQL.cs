using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// SQL statements for clsSearchLogic
    /// <author>Stephanie Gallegos</author>
    /// </summary>
    internal class clsSearchSQL
    {
         
        /// <summary>
        /// Return all invoices with provided invoice number
        /// </summary>
        /// <param name="invoiceNum">Invoice number to search</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistictInvoiceNumber(int invoiceNum)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Return sql string to get all invoices with provided invoice date
        /// </summary>
        /// <param name="invoiceDate">Invoice date to search</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctInvoiceDate(DateTime invoiceDate)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceDate = {invoiceDate}";
                return sSQL;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Return sql to get all invoices with provided invoice cost
        /// </summary>
        /// <param name="invoiceCost">Invoice cost to search</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctInvoiceCost(decimal invoiceCost)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceCost = {invoiceCost}";
                return sSQL;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Return all invoices with provided date and number
        /// </summary>
        /// <param name="invoiceNum">Invoice number to search</param>
        /// <param name="invoiceDate">invoice date to search</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception
        public static string GetInvoicebyNumAndDate(int invoiceNum, DateTime invoiceDate)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceDate = {invoiceDate}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Return all invoices with provided number and cost
        /// </summary>
        /// <param name="invoiceNum">Invoice number to search</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicebyNumAndCost(int invoiceNum, decimal invoiceCost)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceNum = {invoiceNum} AND InvoiceCost = {invoiceCost}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// Return all invoices with provided cost and date
        /// </summary>
        /// <param name="invoiceNum">Invoice number to search</param>
        /// <param name="invoiceCost">Invoice date to search</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicebyCostAndDate(decimal invoiceCost, DateTime invoiceDate)
        {
            try
            {
                string sSQL = $"SELECT * FROM Invoices WHERE InvoiceCost = {invoiceCost} AND InvoiceDate = {invoiceDate}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }


        /// <summary>
        /// Return all invoices 
        /// </summary>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetAllInvoices()
        {
            try
            {
                //Get all invoices
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices ";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }

        }

        /// <summary>
        /// Return sql string to get invoices with provided invoice number
        /// </summary>
        /// <param name="InvoiceNum">Invoice number to search</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
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
        /// Return sql string to get all invoice items associated with the provided invoice number
        /// </summary>
        /// <param name="InvoiceNum">Invoice item to get items for</param>
        /// <returns>string sql</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceItems(int InvoiceNum)
        {
            try
            {
                string sSQL = $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc ";
                sSQL += $"WHERE LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {InvoiceNum}";
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
