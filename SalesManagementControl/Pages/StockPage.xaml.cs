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
using SalesManagementControl.Model;
using SalesManagementControl.Windows;
using SalesManagementControl.Windows.Stock;

namespace SalesManagementControl.Pages
{
	class Entry
	{
		public string VendorCode { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }
		public decimal Price { get; set; }
		public string FormattedPrice { get => $"{Price} (руб.)"; }
		public string TypeName { get; set; }
	}

	public partial class StockPage : Page
    {
        public StockPage(ref MainWindow.SearchHandler searchEvent)
		{
			InitializeComponent();
			LoginTextBlock.Text = Globals.CurrentUser.Login;

			UpdateList("");
			searchEvent += UpdateList;
		}
		public void UpdateList(string text)
		{
			ProductList.ItemsSource = Globals.Context.Product
				.Where(e => e.InStock > 0)
				.Select(e => new Entry()
				{
					VendorCode = e.VendorCode,
					Name = e.Name,
					Amount = e.InStock,
					Price = e.Price,
					TypeName = e.ProductCategory.Name,
				})
				.Where(e => e.Name.ToLower().Contains(text))
				.ToList();

			EntryCountTextBlock.Text = ProductList.Items.Count.ToString();
		}

		private void OnAddStock(object sender, RoutedEventArgs e)
        {
            StockAddWindow stockAddWindow = new StockAddWindow();
            stockAddWindow.ShowDialog();
			UpdateList("");
		}

        private void OnModifyStock(object sender, RoutedEventArgs e)
        {
			Entry entry = (ProductList.SelectedItem as Entry);
			if (entry == null)
			{
				MessageBox.Show(
					"Выберите запись для редактирования.",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);
				return;
			}

			StockModifyWindow stockModifyWindow = new StockModifyWindow(
				Globals.Context.Product
					.SingleOrDefault(x => x.VendorCode == entry.VendorCode)
			);

			stockModifyWindow.ShowDialog();
			UpdateList("");
		}

		private void OnDelete(object sender, RoutedEventArgs e)
		{
			Entry entry = (ProductList.SelectedItem as Entry);
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
					Globals.Context.Product.Remove(
						Globals.Context.Product
							.SingleOrDefault(x => x.VendorCode == entry.VendorCode));

					Globals.Context.SaveChanges();
					UpdateList("");
				}
			}
		}

		private void InfoClick(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
			Entry entry = btn.DataContext as Entry;
			StockInfoWindow w = new StockInfoWindow(
				Globals.Context.Product
					.SingleOrDefault(x => x.VendorCode == entry.VendorCode)
			);
			w.ShowDialog();
		}
	}
}
