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
using System.Reflection;
using System.Data;

//This is the front end code for the search window
//Author: Austin Griffith

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// Initializes the items window
        /// </summary>
        public wndItems()
        {


            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                resetWindow();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Handles any exception at the top level method
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {

                //MessageBox.Show("HandleError Exception thrown: " + sMessage);
        }
        /// <summary>
        /// AddTrue, Bool to tell saveBtn to either add a new entry or edit an entry.  True for Add, False for edit
        /// </summary>
        bool bAddTrue = true;
        /// <summary>
        /// bool to return if item changed
        /// </summary>
        bool bItemChanged = false;

        /// <summary>
        /// Fills the Item table with fresh data
        /// </summary>
        private void fillData()
        {
            try
            {
                //Number of return values
                int iRet = 0;

                //Execute the statement and get the data
                string sSQL = clsItemsSQL.GetAllItems();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                
                List<clsItem> AllItems = new List<clsItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    string ItemCode = dr[0].ToString();
                    string ItemDesc = dr[1].ToString();
                    decimal Cost = Convert.ToDecimal(dr[2]);
                    clsItem item = new clsItem(ItemCode,ItemDesc,Cost);
                    AllItems.Add(item);
                }
                ItemTable.ItemsSource = AllItems;

            }
            catch (Exception ex)
            {
                //throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resets the window to the default
        /// </summary>
        private void resetWindow()
        {
            try
            {
                addBtn.IsEnabled = true;
                editBtn.IsEnabled = false;
                saveBtn.IsEnabled = false;
                deleteBtn.IsEnabled = false;
                CodeBox.IsEnabled = false;
                DescBox.IsEnabled = false;
                CostBox.IsEnabled = false;
                ItemTable.IsEnabled = true;
                CodeBox.Text = "";
                CostBox.Text = "";
                DescBox.Text = "";
                inputLbl.Visibility = Visibility.Hidden;

                fillData();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add button, clears text boxes and sets AddTrue to true, enables save button.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bAddTrue = true;
                addBtn.IsEnabled = false;
                deleteBtn.IsEnabled = false;
                editBtn.IsEnabled = false;
                inputLbl.Visibility = Visibility.Visible;
                //set itemTable selection to -1
                CodeBox.IsEnabled = true;
                DescBox.IsEnabled = true;
                CostBox.IsEnabled = true;
                CodeBox.Text = "";
                CostBox.Text = "";
                DescBox.Text = "";
                ItemTable.IsEnabled = false;
                CodeBox.Focus();
                saveBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Edit button, allows textbox text to be edited, enables save button and sets AddTrue to false.
        /// </summary>
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                addBtn.IsEnabled = false;
                deleteBtn.IsEnabled = false;
                editBtn.IsEnabled = false;
                DescBox.IsEnabled = true;
                CostBox.IsEnabled = true;
                bAddTrue = false;
                CodeBox.Focus();
                saveBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Save button, calls logic function to either add a new item or edit an item based on AddTrue bool, refreshes table
        /// </summary>
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (bAddTrue)
                {
                    if (CodeBox.Text == "" || CostBox.Text == "" || DescBox.Text == "")
                    {
                        inputLbl.Content = "Please fill all fields before saving";
                        return;
                    }
                    clsItemsLogic.AddItem(CodeBox.Text, CostBox.Text, DescBox.Text);
                    saveBtn.IsEnabled = false;

                }
                else
                {
                    if (CodeBox.Text == "" || CostBox.Text == "" || DescBox.Text == "")
                    {
                        inputLbl.Content = "Please fill all fields before saving";
                        return;
                    }
                    clsItemsLogic.EditItem(CodeBox.Text, CostBox.Text, DescBox.Text);
                    
                }
                resetWindow();
                bAddTrue = false;
                bItemChanged = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Delete button, calls logic to delete the selected row.
        /// </summary>
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clsItemsLogic.DeleteItem(CodeBox.Text))
                {
                    bItemChanged=true;
                }
                resetWindow();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void ItemTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemTable.SelectedCells.Count > 0)
            {
                CodeBox.Text = (ItemTable.SelectedCells[0].Column.GetCellContent(ItemTable.SelectedItem) as TextBlock).Text;
                CostBox.Text = (ItemTable.SelectedCells[2].Column.GetCellContent(ItemTable.SelectedItem) as TextBlock).Text;
                DescBox.Text = (ItemTable.SelectedCells[1].Column.GetCellContent(ItemTable.SelectedItem) as TextBlock).Text;
                deleteBtn.IsEnabled = true;
                editBtn.IsEnabled = true;
            }

        }
        /// <summary>
        /// Returns bool if item changed
        /// </summary>
        public bool ItemChanged()
        {
            try
            {
                return bItemChanged;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void ShowMainWindow(object sender, EventArgs e)
        {
            App.ShowMainWindow(this);
        }
        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            App.ShowMainWindow(this);
        }
    }
}
