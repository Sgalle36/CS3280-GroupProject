using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Business logic for search window
    /// <author>Stephanie Gallegos</author>
    /// </summary>
    internal class clsSearchLogic
    {
        /// <summary>
        /// List of invoices
        /// </summary>
        public static List<clsInvoice> lstInvoices;

        /// <summary>
        /// list of current invoice numbers
        /// </summary>
        public static List<int> lstInvoiceNumbers;

        /// <summary>
        /// list of current invoice total costs
        /// </summary>
        public static List<int> lstInvoiceTotalCosts;

        /// <summary>
        /// list of current invoice dates
        /// </summary>
        public static List<DateTime> lstInvoiceDates;

        /// <summary>
        /// list of invoices filtered by invoice number
        /// </summary>
        public static List<clsInvoice> lstInvoiceFilterbyNum;

        /// <summary>
        /// list of invoices filtered by invoice date
        /// </summary>
        public static List<clsInvoice> lstInvoiceFilterbyDate;

        /// <summary>
        /// list of invoices filtered by invoice total cost
        /// </summary>
        public static List<clsInvoice> lstInvoiceFilterbyCost;

        /// <summary>
        /// 
        /// </summary>
        public static List<clsInvoice> lstFiltered;

        /// <summary>
        /// List of items for an invoice
        /// </summary>
        public static List<clsItem> lstItems;

        /// <summary>
        /// Creates a filtered invoice list based on provided parameters
        /// </summary>
        /// <param name="invoiceNum">Number of invoice to search</param>
        /// <param name="invoiceDate">Date of invoice to search</param>
        /// <param name="invoiceCost">Cost of invoice to search</param>
        /// <returns></returns>
        public static List<clsInvoice> FilterInvoices(int num, DateTime date, int cost)
        {
            try
            {
                lstInvoiceFilterbyNum = new List<clsInvoice> ();
                lstInvoiceFilterbyDate = new List<clsInvoice> ();
                lstInvoiceFilterbyCost = new List<clsInvoice> ();
                lstFiltered = lstInvoices;
                DateTime defaultDate = new DateTime(01/01/0001);
                string SQL;


                if (num > 0)
                {
                    SQL = clsSearchSQL.GetDistictInvoicebyNum(num);
                    lstInvoiceFilterbyNum = GetDistinctInvoices(SQL);
                    lstFiltered = lstInvoiceFilterbyNum.Intersect(lstFiltered).ToList();
                    Debug.WriteLine(SQL);
                }

                if(cost > 0)
                {
                    SQL = clsSearchSQL.GetDistictInvoicebyCost(cost);
                    lstInvoiceFilterbyCost = GetDistinctInvoices(SQL);
                    lstFiltered = lstInvoiceFilterbyCost.Intersect(lstFiltered).ToList();
                    Debug.WriteLine(SQL);
                }

                if(date > defaultDate)
                {
                    SQL = clsSearchSQL.GetDistictInvoicebyDate(date);
                    lstInvoiceFilterbyDate = GetDistinctInvoices(SQL);
                    lstFiltered = lstInvoiceFilterbyDate.Intersect(lstFiltered).ToList();
                    Debug.WriteLine(SQL);
                }

                return lstFiltered;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Create list of all invoices and associated items
        /// </summary>
        /// <returns>List of all invoices</returns>
        /// <exception cref="Exception"></exception>
        public static List<clsInvoice> GetInvoices()
        {
            try
            {
                lstInvoices = new List<clsInvoice>(); // List to hold invoices

                string getInvoicesSQL;    //SQL statement to get invoices
                string getItemsSQL;     //SQL statement to get items
                int getInvoiceRet = 0;   //Number of returned invoices

                DataSet dsInvoices = new DataSet(); //Holds the returned invoices
                DataSet dsItems  = new DataSet(); //Holds the returned items
                clsInvoice invoice; //hold instance of invoice
                clsItem item; //hold instance of item

                //get all invoices sql statement
                getInvoicesSQL = clsSearchSQL.GetAllInvoices();


                //Extract the invoices and put them into the DataSet
                dsInvoices = App.db.ExecuteSQLStatement(getInvoicesSQL, ref getInvoiceRet);

                //Loop through the data and create invoices classes
                for (int i = 0; i < getInvoiceRet; i++)
                {
                    int invoiceNum = (int)dsInvoices.Tables[0].Rows[i][0];
                    DateTime invoiceDate = (DateTime)dsInvoices.Tables[0].Rows[i][1];
                    int invoiceCost = (int)dsInvoices.Tables[0].Rows[i][2];

                    //create list of items for each invoice  
                    getItemsSQL = clsSearchSQL.GetInvoiceItems(invoiceNum);
                    int getItemRet = 0; //number of returned items
                    lstItems = new List<clsItem>(); //List to hold items

                    dsItems = App.db.ExecuteSQLStatement(getItemsSQL, ref getItemRet);

                        //loop through each item for the invoice and add each to the item list
                        for (int x = 0; x < getItemRet; x++)
                        {
                            string itemCode = dsItems.Tables[0].Rows[x][0].ToString();
                            string itemDesc = dsItems.Tables[0].Rows[x][1].ToString();
                            decimal itemCost = (decimal)dsItems.Tables[0].Rows[x][2];

                            item = new clsItem(itemCode, itemDesc, itemCost);

                            lstItems.Add(item);
                        }

                    invoice = new clsInvoice(invoiceNum,invoiceDate,invoiceCost,lstItems);

                    lstInvoices.Add(invoice);//add invoice to list of invoices
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Set the lists that hold the invoice filter options
        /// </summary>
        /// <param name="invoices">List of invoices</param>
        public static void SetInvoiceFilterLists(List<clsInvoice> invoices)
        {
            try
            {
                lstInvoiceNumbers = new List<int>();
                lstInvoiceDates = new List<DateTime>();
                lstInvoiceTotalCosts = new List<int>();

                foreach (clsInvoice invoice in invoices)
                {
                    lstInvoiceNumbers.Add((int)invoice.InvoiceNum);
                    lstInvoiceDates.Add((DateTime)invoice.InvoiceDate);
                    lstInvoiceTotalCosts.Add((int)invoice.TotalCost);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets distinct invoices based on provided sql statement
        /// </summary>
        /// <param name="SQL">Distinct sql statement</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<clsInvoice> GetDistinctInvoices(string SQL)
        {
            try
            {
                lstInvoices = new List<clsInvoice>(); // List to hold invoices

                string getInvoicesSQL;    //SQL statement to get invoices
                string getItemsSQL;     //SQL statement to get items
                int getInvoiceRet = 0;   //Number of returned invoices

                DataSet dsInvoices = new DataSet(); //Holds the returned invoices
                DataSet dsItems = new DataSet(); //Holds the returned items
                clsInvoice invoice; //hold instance of invoice
                clsItem item; //hold instance of item


                
                //get all invoices sql statement
                getInvoicesSQL = SQL;

                //Extract the invoices and put them into the DataSet
                dsInvoices = App.db.ExecuteSQLStatement(getInvoicesSQL, ref getInvoiceRet);

                //Loop through the data and create invoices classes
                for (int i = 0; i < getInvoiceRet; i++)
                {
                    int invoiceNum = (int)dsInvoices.Tables[0].Rows[i][0];
                    DateTime invoiceDate = (DateTime)dsInvoices.Tables[0].Rows[i][1];
                    int invoiceCost = (int)dsInvoices.Tables[0].Rows[i][2];

                    //create list of items for each invoice  
                    getItemsSQL = clsSearchSQL.GetInvoiceItems(invoiceNum);
                    int getItemRet = 0; //number of returned items
                    lstItems = new List<clsItem>(); //List to hold items

                    dsItems = App.db.ExecuteSQLStatement(getItemsSQL, ref getItemRet);

                    //loop through each item for the invoice and add each to the item list
                    for (int x = 0; x < getItemRet; x++)
                    {
                        string itemCode = dsItems.Tables[0].Rows[x][0].ToString();
                        string itemDesc = dsItems.Tables[0].Rows[x][1].ToString();
                        decimal itemCost = (decimal)dsItems.Tables[0].Rows[x][2];

                        item = new clsItem(itemCode, itemDesc, itemCost);

                        lstItems.Add(item);
                    }

                    invoice = new clsInvoice(invoiceNum, invoiceDate, invoiceCost, lstItems);

                    lstInvoices.Add(invoice);//add invoice to list of invoices
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
