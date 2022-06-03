using SalesManagementControl.Model;
using SalesManagementControl.Windows.Sales;
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

namespace SalesManagementControl.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page
	{
		class Entry
		{
			public string VendorCode { get; set; }
			public int ClientID { get; set; }
			public DateTime Timestamp { get; set; }
			public string Status { get; set; }

			public int Amount { get; set; }
			public string ProductType { get; set; }
			public string ProductName { get; set; }
			public string WalletCode { get; set; }
			public decimal Price { get; set; }
			public string FormattedPrice { get => $"{Price} (руб.)"; }
			public decimal TotalPrice { get; set; }
			public string FormattedTotalPrice { get => $"{TotalPrice} (руб.)"; }
			public string ClientFullName { get; set; }
		}

		public SalesPage(ref MainWindow.SearchHandler searchEvent)
		{
			InitializeComponent();
			LoginTextBlock.Text = Globals.CurrentUser.Login;

			UpdateList("");
			searchEvent += UpdateList;
		}

		public void UpdateList(string text)
		{
			PurchaseList.ItemsSource = Globals.Context.Purchase
				.Select(e => new Entry()
				{
					Status = e.PurchaseStatus.Name,
					Timestamp = e.Timestamp,
					Amount = e.Amount,
					ProductType = e.Product.ProductCategory.Name,
					ProductName = e.Product.Name,
					WalletCode = e.Client.WalletNumber,
					VendorCode = e.Product.VendorCode,
					Price = e.Product.Price,
					TotalPrice = e.TotalPrice.Value,
					ClientID = e.ClientID,
					ClientFullName = e.Client.LastName + " " + e.Client.LastName + " " + e.Client.Patronymic
				})
				.Where(e => e.ClientFullName.ToLower().Contains(text))
				.ToList();

			EntryCountTextBlock.Text = PurchaseList.Items.Count.ToString();
		}

		private void OnAddSales(object sender, RoutedEventArgs e)
        {
			SalesAddWindow salesAddWindow = new SalesAddWindow();
			salesAddWindow.ShowDialog();
			UpdateList("");
		}

		private void OnDelete(object sender, RoutedEventArgs e)
		{
			Entry entry = (PurchaseList.SelectedItem as Entry);
			if (entry == null)
			{
				MessageBox.Show(
					"Выберите запись для удаления.",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);
				return;
			}

			else
			{
				MessageBoxResult res = MessageBox.Show(
					"Вы действительно хотите удалить запись?",
					"?",
					MessageBoxButton.YesNo,
					MessageBoxImage.Question
					);

				if (res == MessageBoxResult.Yes)
				{
					Globals.Context.Purchase.Remove(
						Globals.Context.Purchase
							.SingleOrDefault(x => 
								x.ProductVendorCode == entry.VendorCode &&
								x.ClientID == entry.ClientID &&
								x.Timestamp == entry.Timestamp));

					Globals.Context.SaveChanges();
					UpdateList("");
				}
			}
		}
	}
}
