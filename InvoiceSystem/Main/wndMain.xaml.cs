using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// <author>Natalie Mueller</author>
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// The main logic class.
        /// </summary>
        clsMainLogic clsMainLogic;

        /// <summary>
        /// Whether a new invoice is being created or an existing invoice is being modified.
        /// </summary>
        bool newInvoice = false;

        /// <summary>
        /// Load the main window and bind data to GUI objects.
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();
                clsMainLogic = new clsMainLogic();
                LabelInvoiceNumber.Content = "";
                TextBoxDate.Text = "";
                LabelTotalCost.Content = "$0";
                DataGridInvoiceItems.ItemsSource = clsMainLogic.Invoice.Items;
                ComboBoxItems.ItemsSource = clsMainLogic.Items;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Take an invoice number and load its data into the GUI
        /// </summary>
        /// <param name="invoiceNum">The invoice number</param>
        public void LoadInvoice(int invoiceNum)
        {
            try
            {
                //when the search window selects an invoice, call this method with the given invoice number.
                clsMainLogic = new clsMainLogic(invoiceNum);
                ButtonEditInvoice.IsEnabled = true;

                LabelInvoiceNumber.Content = clsMainLogic.Invoice.InvoiceNum;
                TextBoxDate.Text = clsMainLogic.Invoice.InvoiceDate.ToShortDateString();
                LabelTotalCost.Content = $"${clsMainLogic.Invoice.TotalCost}";
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Hide the current window and show the search window
        /// </summary>
        private void ShowSearchWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ShowSearchWindow();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Hide the current window and show the items window
        /// </summary>
        private void ShowItemsWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                App.ShowItemsWindow();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Refreshes item information when the program returns to the main window.
        /// </summary>
        public void RefreshItems()
        {
            clsMainLogic.RefreshItemData();
            DataGridInvoiceItems.ItemsSource = clsMainLogic.Invoice.Items;
            ComboBoxItems.ItemsSource = clsMainLogic.Items;
        }

        /// <summary>
        /// Clear the GUI and allow the user to create a new invoice
        /// </summary>
        private void ClickNewInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                clsMainLogic = new clsMainLogic();
                newInvoice = true;
                ButtonEditInvoice.IsEnabled = false;
                LabelInvoiceNumber.Content = "TBD";
                TextBoxDate.Text = clsMainLogic.Invoice.InvoiceDate.ToShortDateString();
                LabelTotalCost.Content = "$0";

                DataGridInvoiceItems.ItemsSource = clsMainLogic.Invoice.Items;
                ComboBoxItems.ItemsSource = clsMainLogic.Items;

                ToggleControlsEnabled();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Enable editing of the invoice that is shown in the GUI
        /// </summary>
        private void ClickEditInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                newInvoice = false;
                ToggleControlsEnabled();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Save changes made to the current invoice into the database
        /// </summary>
        private void ClickSaveInvoice(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime.TryParse(TextBoxDate.Text, out DateTime date);
                clsMainLogic.Invoice.InvoiceDate = date;

                if (newInvoice)
                {
                    clsMainLogic.CreateInvoice();
                    LabelInvoiceNumber.Content = clsMainLogic.Invoice.InvoiceNum;
                    newInvoice = false;
                }
                else //edit current invoice
                {
                    clsMainLogic.SaveInvoice();
                }

                ToggleControlsEnabled();
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Updates the shown price when an item is selected.
        /// </summary>
        private void SelectionChangedNewItem(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ComboBoxItems.SelectedItem != null)
                {
                    LabelItemPrice.Content = ((clsItem)ComboBoxItems.SelectedItem).Cost;
                }
                else
                {
                    LabelItemPrice.Content = "0";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Adds a new item to the invoice.
        /// </summary>
        private void ClickSaveItem(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteItemErrorLabel.Visibility = Visibility.Hidden;

                //don't do anything if an item is not selected
                if (ComboBoxItems.SelectedItem == null)
                {
                    AddItemErrorLabel.Visibility = Visibility.Visible;
                    return;
                }

                AddItemErrorLabel.Visibility = Visibility.Hidden;
                clsMainLogic.AddItem((clsItem)ComboBoxItems.SelectedItem);
                LabelTotalCost.Content = $"${clsMainLogic.Invoice.TotalCost}";

                ComboBoxItems.SelectedItem = null;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes an item from the invoice.
        /// </summary>
        private void ClickDeleteItem(object sender, RoutedEventArgs e)
        {
            try
            {
                AddItemErrorLabel.Visibility = Visibility.Hidden;

                //don't do anything if an item is not selected
                if (DataGridInvoiceItems.SelectedItem == null)
                {
                    DeleteItemErrorLabel.Visibility = Visibility.Visible;
                    return;
                }

                DeleteItemErrorLabel.Visibility = Visibility.Hidden;
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    $"Are you sure you want to delete {(clsItem)DataGridInvoiceItems.SelectedItem} from the invoice?",
                    "Confirm Item Deletion",
                    System.Windows.MessageBoxButton.YesNo);

                //delete the item
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    clsMainLogic.RemoveItem((clsItem)DataGridInvoiceItems.SelectedItem);
                    LabelTotalCost.Content = $"${clsMainLogic.Invoice.TotalCost}";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Resets what elements can be interacted with.
        /// </summary>
        private void ResetControlsEnabled()
        {
            try
            {
                ButtonNewInvoice.IsEnabled = true;
                ButtonEditInvoice.IsEnabled = false;
                ButtonSaveInvoice.IsEnabled = false;
                TextBoxDate.IsEnabled = false;
                DataGridInvoiceItems.IsEnabled = false;
                ButtonDeleteItem.IsEnabled = false;
                ButtonAddItem.IsEnabled = false;
                ItemsGroupBox.IsEnabled = false;
                AddItemErrorLabel.Visibility = Visibility.Hidden;
                DeleteItemErrorLabel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Toggles what elements can be interacted with when creating or editing an invoice.
        /// </summary>
        private void ToggleControlsEnabled()
        {
            try
            {
                ButtonNewInvoice.IsEnabled = !ButtonNewInvoice.IsEnabled;
                if (!newInvoice)
                {
                    ButtonEditInvoice.IsEnabled = !ButtonEditInvoice.IsEnabled;
                }
                ButtonSaveInvoice.IsEnabled = !ButtonSaveInvoice.IsEnabled;
                TextBoxDate.IsEnabled = !TextBoxDate.IsEnabled;
                DataGridInvoiceItems.IsEnabled = !DataGridInvoiceItems.IsEnabled;
                ButtonDeleteItem.IsEnabled = !ButtonDeleteItem.IsEnabled;
                ButtonAddItem.IsEnabled = !ButtonAddItem.IsEnabled;
                ItemsGroupBox.IsEnabled = !ItemsGroupBox.IsEnabled;
                AddItemErrorLabel.Visibility = Visibility.Hidden;
                DeleteItemErrorLabel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                HandleError(MethodBase.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Show a popup with error information when an error occurs.
        /// </summary>
        /// <param name="sClass">The name of the class.</param>
        /// <param name="sMethod">The name of the method.</param>
        /// <param name="sMessage">The error message.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
