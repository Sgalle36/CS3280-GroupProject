using InvoiceSystem.Items;
using InvoiceSystem.Main;
using InvoiceSystem.Search;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// <author>Natalie Mueller</author>
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Use for all database calls.
        /// </summary>
        internal static clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Use a single instance of the main window and load invoices
        /// </summary>
        internal static wndMain wndMain = (wndMain)Current.MainWindow;

        /// <summary>
        /// Initialize the App.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the main window with the last loaded invoice, or blank if no invoice has been loaded.
        /// </summary>
        public static void ShowMainWindow()
        {
            wndMain.Show();
        }

        /// <summary>
        /// Sends an invoice number to the main window so it can load that invoice's information.
        /// </summary>
        /// <param name="invoiceNum">The invoice number.</param>
        public static void ShowMainWindow(int invoiceNum)
        {
            wndMain.LoadInvoice(invoiceNum);
            wndMain.Show();
        }

        /// <summary>
        /// Hide the main window and show the search window.
        /// </summary>
        public static void ShowSearchWindow()
        {
            wndMain.Hide();
            new wndSearch().Show();
        }

        /// <summary>
        /// Hide the main window and show the items window.
        /// </summary>
        public static void ShowItemsWindow()
        {
            wndMain.Hide();
            new wndItems().Show();
        }
    }
}
