using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.Main;


//This file has the business logic for the Items window

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Holds all the business logic for the search window
    /// </summary>
    ///         ////Used to access the database


    internal class clsItemsLogic
    {
        /// <summary>
        /// Instance of clsDataAccess for use in all query results
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// Gets a list of all items
        /// </summary>
        public static List<clsItem> GetAllItems()
        {
            try
            {
                //Number of return values
                int iRet = 0;

                //Execute the statement and get the data
                string sSQL = clsItemsSQL.GetAllItems();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                List<clsItem> AllItems = new List<clsItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string ItemCode = dr[0].ToString();
                    string ItemDesc = dr[1].ToString();
                    decimal Cost = Convert.ToDecimal(dr[2]);
                    clsItem item = new clsItem(ItemCode, ItemDesc, Cost);
                    AllItems.Add(item);
                }
                return AllItems;
            }
            catch (Exception ex)
            {
                //throw error message
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Adds an item to database.
        /// </summary>
        public static void AddItem(string sItemCode, string sItemCost, string sItemDesc)
            
            {
            try
            {
                string sSQL = clsItemsSQL.InsertNewItem(sItemDesc, sItemCode, sItemCost);
                clsDataAccess db = new clsDataAccess();

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                //throws error message
                System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the Item in database.
        /// </summary>

        public static void EditItem(string sItemCode, string sItemCost, string sItemDesc)
            {
                try
                {
                string sSQL = clsItemsSQL.UpdateItemDescAndCost(sItemCode, sItemCost, sItemDesc);
                clsDataAccess db = new clsDataAccess();
       
                db.ExecuteNonQuery(sSQL);
                }
                catch (Exception ex)
                {
                    //throws error message
                    System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        /// <summary>
        /// Deletes the Item in database.
        /// </summary>
        public static bool DeleteItem(string sItemCode)
            {
                try
                {
                //Calls sql to get potential invoice number fro
                string sSQL = clsItemsSQL.GetInvoiceNumberFromItemCode(sItemCode);
                clsDataAccess db = new clsDataAccess();
                int iRet = 0;
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                if (ds.Tables[0].Rows.Count != 0)            
                    {
                    string title = ds.Tables[0].Rows[0]["InvoiceNum"].ToString();
                    System.Windows.MessageBox.Show("This Item code is in the invoice " + title + " and can't be deleted");
                        return false;
                   }
                
                sSQL = clsItemsSQL.DeleteItem(sItemCode);
                db.ExecuteNonQuery(sSQL);
                return true;
            }
                catch (Exception ex)
                {
                    //throws error message
                    System.Windows.MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return false;
                }
            }

        }    
    } 
