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
    /// <author>Stephanie Gallegos</author>
    /// </summary>
    public partial class wndSearch : Window
    {
        private List<clsInvoice> lstInvoices; // list of invoices
        private clsInvoice selectedInvoice; //instance of selected invoice
        private int filterByInvoiceNum;
        private DateTime filterByInvoiceDate;
        private decimal filterByInvoiceCost;


        /// <summary>
        /// Initialize window and bind invoice datagrid
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            lstInvoices = clsSearchLogic.GetInvoices(); //create list of unfiltered invoices
            dtgSearchInvoice.ItemsSource = lstInvoices; //bind datagrid to invoice list
        }

        /// <summary>
        /// Show main window on window closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, RoutedEventArgs e)
        {
           App.ShowMainWindow(this);
        }

        /// <summary>
        /// Show main window on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, EventArgs e)
        {
            App.ShowMainWindow(this);
        }

        /// <summary>
        /// Clear combo boxes when clear button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //clear all combo box selections
            cboInvoiceNumber.SelectedIndex = -1;
            cboInvoiceDate.SelectedIndex = -1;
            cboInvoiceCost.SelectedIndex = -1;

            //clear filters from invoices in the datagrid (show all invoices)

        }

        /// <summary>
        /// Filter invoices by invoice number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected number
        }

        /// <summary>
        /// Filter invoices by invoice date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected date
        }

        /// <summary>
        /// Filter invoices by invoice cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected invoice number
            //use sql class to get statement to get all invoices with selected cost
        }

        /// <summary>
        /// Gets the selected invoice instance and send to main window to be displayed
        /// </summary>
        /// <param name="sender">Select Invoice Button</param>
        /// <param name="e">Button click</param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            //get and set the instance of the selected invoice
                //show main window with the selected invoice (pass selectedInvoice)
        }
    }
}
