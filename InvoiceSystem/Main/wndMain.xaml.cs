using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        clsMainLogic clsMainLogic;

        public wndMain()
        {
            clsMainLogic = new clsMainLogic();
            InitializeWindow();
        }

        public wndMain(int invoiceNum)
        {
            clsMainLogic = new clsMainLogic(invoiceNum);
            InitializeWindow();
        }

        private void InitializeWindow()
        {
            DataContext = clsMainLogic;
            InitializeComponent();
        }

        private void ShowSearchWindow(object sender, RoutedEventArgs e)
        {
            App.ShowSearchWindow();
        }

        private void ShowItemsWindow(object sender, RoutedEventArgs e)
        {
            App.ShowItemsWindow();
        }
    }
}
