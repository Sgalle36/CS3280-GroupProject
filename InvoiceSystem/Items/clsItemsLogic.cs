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



    internal class clsItemsLogic
    {
        /// <summary>
        /// Instance of clsDataAccess for use in all query results
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// Gets a list of all items
        /// </summary>
        //public static void List<clsItem> GetAllItems()
    

        /// <summary>
        /// Adds an item to database.
        /// </summary>
        //public static void AddItem(string sItemCode, string sItemCost, string sItemDesc)

 

        /// <summary>
        /// Updates the Item in database.
        /// </summary>

        public static void EditItem(string sItemCode, string sItemCost, string sItemDesc)
        {

        }
        /// <summary>
        /// Deletes the Item in database.
        /// </summary>
        //public static bool DeleteItem(string sItemCode)

    }
}
