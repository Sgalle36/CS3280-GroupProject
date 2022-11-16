using System;
using System.Reflection;

namespace InvoiceSystem.Main
{
    internal static class clsMainSQL
    {
        //- INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/13/2018#, 100)
        public static string CreateInvoice(DateOnly InvoiceDate, double TotalCost)
        {
            try
            {
                string sSQL = $"INSERT INTO Invoices (InvoiceDate, TotalCost) Values ({InvoiceDate}, {TotalCost})";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //- INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (123, 1, 'AA')
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

        //- SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123
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

        //- SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = 5000
        public static string GetAllItems(int InvoiceNum)
        {
            try
            {
                string sSQL  = $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc ";
                sSQL        += $"WHERE LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = {InvoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //- UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
        public static string UpdateInvoiceCost(int InvoiceNum, double TotalCost)
        {
            try
            {
                string sSQL = $"UPDATE Invoices SET TotalCost = {TotalCost} WHERE InvoiceNum = {InvoiceNum}";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //- DELETE FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode AND InvoiceNum = 5000 AND ItemCode = 'A'
        public static string RemoveItem(int InvoiceNum, string ItemCode)
        {
            try
            {
                string sSQL  = $"DELETE FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode ";
                       sSQL += $"AND InvoiceNum = {InvoiceNum} AND ItemCode = '{ItemCode}'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //- DELETE FROM LineItems WHERE InvoiceNum = 5000
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

        /*
         * The below SQL statement might make more sense in a different file
         */

        //- SELECT ItemCode, ItemDesc, Cost from ItemDesc
        public static string GetItemDescriptions()
        {
            try
            {
                string sSQL = $"SELECT ItemCode, ItemDesc, Cost from ItemDesc";
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
