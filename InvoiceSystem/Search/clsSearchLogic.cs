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

    internal class clsSearchLogic
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<clsInvoice> lstInvoices;

        /// <summary>
        /// 
        /// </summary>
        public static List<clsItem> lstItems;

        //get distinct invoice number
        //get distinct date
        //get distinct invoice cost
        
        //get invoices(invoice number, date, cost) -> return list of invoices
        //bind list to dtg
        public static List<clsInvoice> GetInvoices()
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                clsDataAccess db = new clsDataAccess();

                string getInvoicesSQL;    //Holds an SQL statement
                string getItemsSQL;
                int getInvoiceRet = 0;   //Number of return values
                  //Number of return values
                DataSet ds = new DataSet(); //Holds the return values
                DataSet dsGetItems = new DataSet(); //Holds the return values
                clsInvoice invoice; //Used to load the return values into the combo box

                //access get flights sql statement
                getInvoicesSQL = clsSearchSQL.GetAllInvoices();


                //Extract the passengers and put them into the DataSet
                ds = db.ExecuteSQLStatement(getInvoicesSQL, ref getInvoiceRet);

                //Loop through the data and create invoices classes
                for (int i = 0; i < getInvoiceRet; i++)
                {
                    int invoiceNum = (int)ds.Tables[0].Rows[i][0];
                    DateTime invoiceDate = (DateTime)ds.Tables[0].Rows[i][1];
                    int invoiceCost = (int)ds.Tables[0].Rows[i][2];

                    //create list of items for each invoice
                    
                    getItemsSQL = clsSearchSQL.GetInvoiceItems(invoiceNum);

                    int getItemRet = 0;
                    lstItems = new List<clsItem>();

                    dsGetItems = db.ExecuteSQLStatement(getItemsSQL, ref getItemRet);

                        for (int x = 0; x < getItemRet; x++)
                        {
                            string itemCode = dsGetItems.Tables[0].Rows[x][0].ToString();
                            string itemDesc = dsGetItems.Tables[0].Rows[x][1].ToString();
                            decimal itemCost = (decimal)dsGetItems.Tables[0].Rows[x][2];

                        Console.WriteLine(itemCost + itemDesc + itemCode);

                            clsItem item = new clsItem(itemCode, itemDesc, itemCost);

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
