using InvoiceSystem.Main;
using System;
using System.Collections;
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
        /// <summary>
        /// The current list of invoices bound to the datagrid
        /// </summary>
        private List<clsInvoice> lstInvoices;

        /// <summary>
        /// The invoice selected in the datagrid
        /// </summary>
        private clsInvoice selectedInvoice;

        /// <summary>
        /// The invoice number selected in the filter dropdown
        /// </summary>
        private int selectedInvoiceNum; 

        /// <summary>
        /// The invoice date selected in the filter dropdown
        /// </summary>
        private DateTime selectedInvoiceDate;

        /// <summary>
        /// The invoice cost selected in the filter dropdown
        /// </summary>
        private int selectedInvoiceCost;


        /// <summary>
        /// Initialize window and bind invoice datagrid
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            lstInvoices = clsSearchLogic.GetInvoices(); //create list of unfiltered invoices
            clsSearchLogic.SetInvoiceFilterLists(lstInvoices); //set the filter options

            //bind filter options to dropdowns
            cboInvoiceNumber.ItemsSource = clsSearchLogic.lstInvoiceNumbers; 
            cboInvoiceDate.ItemsSource = clsSearchLogic.lstInvoiceDates;
            cboInvoiceCost.ItemsSource = clsSearchLogic.lstInvoiceTotalCosts;

            dtgSearchInvoice.ItemsSource = lstInvoices; //bind datagrid to invoice list
        }

        /// <summary>
        /// Show main window on window closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ShowMainWindow(this);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// Show main window on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMainWindow(object sender, EventArgs e)
        {
            try
            {
                App.ShowMainWindow(this);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Clear combo boxes when clear button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //clear all combo box selections
                cboInvoiceNumber.SelectedIndex = -1;
                cboInvoiceDate.SelectedIndex = -1;
                cboInvoiceCost.SelectedIndex = -1;

                selectedInvoiceNum = 0;
                selectedInvoiceCost = 0;
                selectedInvoiceDate = new DateTime(01 / 01 / 0001);


                //clear filters from invoices in the datagrid (show all invoices)
                dgUpdateInvoicesDisplayed();
                lstInvoices = clsSearchLogic.GetInvoices();
                dtgSearchInvoice.ItemsSource = lstInvoices; //bind datagrid to invoice list
                cboUpdateDropdownOptions();

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Filter invoices by invoice number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cboInvoiceNumber.SelectedValue != null)
                {
                    //get selected invoice number
                    selectedInvoiceNum = (int)cboInvoiceNumber.SelectedValue;
                    dgUpdateInvoicesDisplayed();
                    cboUpdateDropdownOptions();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Filter invoices by invoice date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cboInvoiceDate.SelectedItem != null)
                {
                    //get selected invoice number
                    selectedInvoiceDate = (DateTime)cboInvoiceDate.SelectedItem;
                    dgUpdateInvoicesDisplayed();
                    cboUpdateDropdownOptions();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Filter invoices by invoice cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cboInvoiceCost.SelectedItem != null)
                {
                    //get selected invoice number
                    selectedInvoiceCost = (int)cboInvoiceCost.SelectedItem;
                    dgUpdateInvoicesDisplayed();
                    cboUpdateDropdownOptions();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Gets the selected invoice instance and send to main window to be displayed
        /// </summary>
        /// <param name="sender">Select Invoice Button</param>
        /// <param name="e">Button click</param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get and set the instance of the selected invoice
                selectedInvoice = (clsInvoice)dtgSearchInvoice.SelectedItem;
                //show main window with the selected invoice (pass selectedInvoice)
                App.wndMain.LoadInvoice((int)selectedInvoice.InvoiceNum);
                App.ShowMainWindow(this);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Filter the current invoice list and update the datagrid with the results
        /// </summary>
        private void dgUpdateInvoicesDisplayed()
        {
            try
            {
                lstInvoices = clsSearchLogic.FilterInvoices(selectedInvoiceNum, selectedInvoiceDate, selectedInvoiceCost);
                dtgSearchInvoice.ItemsSource = lstInvoices;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Set the filter options for the dropdowns and update the dropdowns
        /// </summary>
        private void cboUpdateDropdownOptions()
        {
            try
            {
                clsSearchLogic.SetInvoiceFilterLists(lstInvoices);
                cboInvoiceNumber.ItemsSource = clsSearchLogic.lstInvoiceNumbers;
                cboInvoiceDate.ItemsSource = clsSearchLogic.lstInvoiceDates;
                cboInvoiceCost.ItemsSource = clsSearchLogic.lstInvoiceTotalCosts;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
