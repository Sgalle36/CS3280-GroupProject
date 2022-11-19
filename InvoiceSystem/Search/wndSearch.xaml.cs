using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// Author: Stephanie Gallegos
    /// Date: 11/19/22
    /// </summary>
    public partial class wndSearch : Window
    {
        private List<clsInvoice> lstInvoices;
        /// <summary>
        /// 
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            lstInvoices = clsSearchLogic.GetInvoices();
            dtgSearchInvoice.ItemsSource = lstInvoices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, RoutedEventArgs e)
        {
           App.ShowMainWindow(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, EventArgs e)
        {
            App.ShowMainWindow(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear all combo box selections
            cboInvoiceNumber.SelectedIndex = -1;
            cboInvoiceDate.SelectedIndex = -1;
            cboInvoiceCost.SelectedIndex = -1;

            //clear filters from invoices in the datagrid (show all)

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected number
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected date
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected cost
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            //get the instance of the selected invoice
                //show main window with the selected invoice
        }
    }
}
