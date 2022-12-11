using InvoiceSystem.Items;
using InvoiceSystem.Main;
using InvoiceSystem.Search;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace InvoiceSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Used for all database calls.
        /// </summary>
        internal static clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Used to ensure the app stays open until the main window is closed.
        /// </summary>
        internal static wndMain wndMain = (wndMain)Current.MainWindow;

        /// <summary>
        /// Run the app.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the main window.
        /// </summary>
        /// <param name="prevWindow">The previous window to match.</param>
        public static void ShowMainWindow(Window prevWindow)
        {
            try
            {
                wndMain.RefreshItems();
                wndMain.WindowState = prevWindow.WindowState;
                MatchWindowSize(wndMain, prevWindow);
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Show the search window.
        /// </summary>
        public static void ShowSearchWindow()
        {
            try
            {
                wndSearch wndSearch = new wndSearch();
                MatchWindowSize(wndSearch, wndMain);
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Show the items window.
        /// </summary>
        public static void ShowItemsWindow()
        {
            try
            {
                wndItems wndItems = new wndItems();
                MatchWindowSize(wndItems, wndMain);
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Matches previous window state, size, and position
        /// </summary>
        /// <param name="wndNext">The next window to display</param>
        /// <param name="wndPrev">The previous window to match</param>
        public static void MatchWindowSize(Window wndNext, Window wndPrev)
        {
            try
            {
                wndNext.WindowState = wndPrev.WindowState;
                wndNext.Top = wndPrev.Top;
                wndNext.Left = wndPrev.Left;
                wndNext.Height = wndPrev.ActualHeight;
                wndNext.Width = wndPrev.ActualWidth;
                wndNext.Show();
                wndPrev.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Show a popup with error information when an error occurs.
        /// </summary>
        /// <param name="sClass">The name of the class.</param>
        /// <param name="sMethod">The name of the method.</param>
        /// <param name="sMessage">The error message.</param>
        private static void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                Debug.WriteLine(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("HandleError Exception: " + ex.Message);
            }
        }
    }
}
