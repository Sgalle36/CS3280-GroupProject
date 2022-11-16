using InvoiceSystem.Main;
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

        public App()
        {
            InitializeComponent();

            //invoice numbers start at 5000
            clsInvoice invoice = clsMainLogic.GetInvoice(5000);
        }
    }
}
