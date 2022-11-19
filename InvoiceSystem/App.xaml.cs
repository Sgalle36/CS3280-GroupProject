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

        internal static wndMain wndMain = (wndMain)Current.MainWindow;

        public App()
        {
            InitializeComponent();
        }

        public static void ShowMainWindow()
        {
            wndMain.Show();
        }

        public static void ShowSearchWindow()
        {
            wndMain.Hide();
            new wndSearch().Show();
        }

        public static void ShowItemsWindow()
        {
            wndMain.Hide();
            new wndItems().Show();
        }
    }
}
