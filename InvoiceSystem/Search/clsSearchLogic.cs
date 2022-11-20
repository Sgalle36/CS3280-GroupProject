using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

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
        public static List<clsInvoice> FilterInvoices(int invoiceNum, DateTime invoiceDate, decimal invoiceCost)
        {
            try
            {
                //get distinct invoice number
                //get distinct date
                //get distinct invoice cost
                return new List<clsInvoice>();

            }
            catch (Exception ex)
            {

                throw;
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
                clsDataAccess db = new clsDataAccess(); // instance of class to access db

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
                dsInvoices = db.ExecuteSQLStatement(getInvoicesSQL, ref getInvoiceRet);

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

                    dsItems = db.ExecuteSQLStatement(getItemsSQL, ref getItemRet);

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

    }
}
