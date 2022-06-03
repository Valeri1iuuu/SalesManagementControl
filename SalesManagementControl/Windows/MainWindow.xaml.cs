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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SalesManagementControl.Pages;

namespace SalesManagementControl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public delegate void SearchHandler(string text);

		public event SearchHandler SearchEvent;

        public MainWindow()
        {
            InitializeComponent();

            PageContainer.Content = new ClientPage(ref SearchEvent);
        }

        private void OnClientPage(object sender, RoutedEventArgs e)
        {
			PageContainer.Content = new ClientPage(ref SearchEvent);
		}

        private void OnStockPage(object sender, RoutedEventArgs e)
        {
			PageContainer.Content = new StockPage(ref SearchEvent);
		}

        private void OnsalesPage(object sender, RoutedEventArgs e)
        {
			PageContainer.Content = new SalesPage(ref SearchEvent);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SearchEvent?.Invoke(SearchTextBox.Text.ToLower());
		}
	}
}
