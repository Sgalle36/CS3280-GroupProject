using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

//This file has the SQL queries required for the items window

namespace GroupProject.Items
{
    /// <summary>
    /// Holds all required SQL queries for the Items window
    /// </summary>
    internal class clsItemsSQL
    {
        /// <summary>
        /// SQL query to get all itemcodes, descs, and costs from ItemDesc
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetAllItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL query to get invoice number from a given ItemCode
        /// </summary>
        /// <param name="sItemCode">Item Coder</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceNumberFromItemCode(string sItemCode)
        {
            try
            {
                string sSQL = "select distinct(InvoiceNum) from LineItems where ItemCode = '" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL query to update item desc and cost by item code
        /// </summary>
        /// <param name="sItemCode">ItemCode</param>
        /// <param name="sCost">Cost</param>
        /// <param name="sItemDesc">ItemDesc</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateItemDescAndCost(string sItemCode, string sCost, string sItemDesc)
        {
            try
            {
                string sSQL = "Update ItemDesc Set ItemDesc = '" + sItemDesc + "', Cost = " + sCost + " where ItemCode = '" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL query to insert a new item into itemdesc
        /// </summary>
        /// <param name="sItemDesc">Item Descr</param>
        /// <param name="sItemCode">Item Code</param>
        /// <param name="sCost">Cost</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertNewItem(string sItemDesc, string sItemCode, string sCost)
        {
            try
            {
                string sSQL = "Insert into ItemDesc (ItemCode, ItemDesc, Cost) Values ('" + sItemCode + "' , '" + sItemDesc + "' , " + sCost + ")"; return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// SQL query to Delete item by ItemCode
        /// </summary>
        /// <param name="sItemCode">Cost</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteItem(string sItemCode)
        {
            try
            {
                string sSQL = "Delete from ItemDesc Where ItemCode = '" + sItemCode + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
