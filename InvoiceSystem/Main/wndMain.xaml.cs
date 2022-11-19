using System.Windows;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// <author>Natalie Mueller</author>
    /// </summary>
    public partial class wndMain : Window
    {
        clsMainLogic clsMainLogic;

        /// <summary>
        /// Load the main window and bind data to GUI objects.
        /// </summary>
        public wndMain()
        {
            clsMainLogic = new clsMainLogic();
            DataContext = clsMainLogic;
            InitializeComponent();
            DataGridInvoiceItems.ItemsSource = clsMainLogic.InvoiceItems;
            DataGridComboBoxItems.ItemsSource = clsMainLogic.Items;
        }

        /// <summary>
        /// Take an invoice number and load its data into the GUI
        /// </summary>
        /// <param name="invoiceNum">The invoice number</param>
        public void LoadInvoice(int invoiceNum)
        {
            clsMainLogic = new clsMainLogic(invoiceNum);
        }

        /// <summary>
        /// Hide the current window and show the search window
        /// </summary>
        private void ShowSearchWindow(object sender, RoutedEventArgs e)
        {
            App.ShowSearchWindow(this);
        }

        /// <summary>
        /// Hide the current window and show the items window
        /// </summary>
        private void ShowItemsWindow(object sender, RoutedEventArgs e)
        {   
            App.ShowItemsWindow(this);
        }

        /// <summary>
        /// Clear the GUI and allow the user to create a new invoice
        /// </summary>
        private void NewInvoice(object sender, RoutedEventArgs e)
        {
            //clear information about the current invoice in all controls
            //enable editing
            //set the invoice number to TBD
        }

        /// <summary>
        /// Enable editing of the invoice that is shown in the GUI
        /// </summary>
        private void EditInvoice(object sender, RoutedEventArgs e)
        {
            //enable GUI controls to change invoice data
        }

        /// <summary>
        /// Save changes made to the current invoice into the database
        /// </summary>
        private void SaveInvoice(object sender, RoutedEventArgs e)
        {
            //write changes into the database
            //if a new invoice was saved, get the invoice number and update the GUI
        }
    }
}
