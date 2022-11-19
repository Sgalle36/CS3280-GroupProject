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
    /// </summary>
    public partial class App : Application
    {
        //use for all database calls
        internal static clsDataAccess db = new clsDataAccess();

        //
        internal static wndMain wndMain = (wndMain)Current.MainWindow;

        /// <summary>
        /// 
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prevWindow"></param>
        public static void ShowMainWindow(Window prevWindow)
        {
            wndMain.WindowState = prevWindow.WindowState;
            MatchWindowSize(wndMain, prevWindow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prevWindow"></param>
        public static void ShowSearchWindow(Window prevWindow)
        {
            wndSearch wndSearch = new wndSearch();
            MatchWindowSize(wndSearch, prevWindow);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prevWindow"></param>
        public static void ShowItemsWindow(Window prevWindow)
        {
            wndItems wndItems = new wndItems();
            MatchWindowSize(wndItems, prevWindow);
        }

        /// <summary>
        /// Matches previous window state, size, and position
        /// </summary>
        /// <param name="wndNext">The next window to display</param>
        /// <param name="wndPrev">The previous window to match</param>
        public static void MatchWindowSize(Window wndNext, Window wndPrev)
        {
            wndNext.WindowState = wndPrev.WindowState;
            wndNext.Top = wndPrev.Top;
            wndNext.Left = wndPrev.Left;
            wndNext.Height = wndPrev.ActualHeight;
            wndNext.Width = wndPrev.ActualWidth;
            wndNext.Show();
            wndPrev.Hide();

        }
    }
}
