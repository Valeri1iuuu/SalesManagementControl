using SalesManagementControl.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace SalesManagementControl.Windows.Sales
{
	/// <summary>
	/// Логика взаимодействия для SalesAddWindow.xaml
	/// </summary>
	public partial class SalesAddWindow : Window
	{
		class ClientComboBoxEntry
		{
			public int ClientID { get; set; }
			public string FullName { get; set; }
		}

		public SalesAddWindow()
		{
			InitializeComponent();

			ProductTypeComboBox.ItemsSource = Globals.Context.ProductCategory.ToList();
			ProductTypeComboBox.DisplayMemberPath = "Name";
			ProductTypeComboBox.SelectedIndex = 0;

			ProductName.DisplayMemberPath = "Name";

			StatusComboBox.ItemsSource = Globals.Context.PurchaseStatus.ToList();
			StatusComboBox.DisplayMemberPath = "Name";
			StatusComboBox.SelectedIndex = 0;

			ClientNameComboBox.ItemsSource = Globals.Context.Client
				.Select(e => new ClientComboBoxEntry()
				{
					ClientID = e.ID,
					FullName = e.LastName + " " + e.FirstName + " " + e.Patronymic,

				})
				.ToList();
			ClientNameComboBox.DisplayMemberPath = "FullName";
			ClientNameComboBox.SelectedIndex = 0;
		}

		private void _updateProductComboBox(int categoryId)
		{
			ProductName.ItemsSource = Globals.Context.Product
				.Where(e => e.CategoryID == categoryId)
				.ToList();
			ProductName.SelectedIndex = 0;
		}

		private void OnExit(object sender, MouseButtonEventArgs e)
		{
			Close();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void OnExitStopDrag(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (!ProductValidation.AmountValidation(AmountTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённое количество",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			try
			{
				var prod = (ProductName.SelectedItem as Product);
				var clientID = (ClientNameComboBox.SelectedItem as ClientComboBoxEntry).ClientID;

				byte amount = Byte.Parse(AmountTextBox.Text);

				if (amount > prod.InStock)
				{
					MessageBox.Show(
						"На складе недостаточно товара",
						"Ошибка",
						MessageBoxButton.OK,
						MessageBoxImage.Error
						);

					return;
				}

				prod.InStock -= amount;
				Purchase p = new Purchase()
				{
					ProductVendorCode = prod.VendorCode,
					ClientID = clientID,
					Timestamp = DateTime.Now,
					Amount = amount,
					PurchaseStatusID = (StatusComboBox.SelectedItem as PurchaseStatus).ID,
				};

				Globals.Context.Purchase.Add(p);
				Globals.Context.SaveChanges();
			}

			catch (Exception ex)
			{
				MessageBox.Show(
					"Проверьте правильность введённых данных:\n" + ex.Message,
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			MessageBox.Show(
				"Продажа успешно оформлена",
				"Успех",
				MessageBoxButton.OK,
				MessageBoxImage.Asterisk
				);

			Close();
		}

		private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var prod = (ProductName.SelectedItem as Product);

			if (prod == null)
			{
				TotalPriceTextBox.Text = "";
				return;
			}

			if ((sender as TextBox).Text == "")
			{
				TotalPriceTextBox.Text = "";
			}

			else
			{
				int num = Int32.Parse((sender as TextBox).Text);
				TotalPriceTextBox.Text = (prod.Price * num).ToString();
			}
		}

		private void ProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var prod = ((sender as ComboBox).SelectedItem as Product);
			if (prod == null)
			{
				PriceTextBox.Text = "";
				VendorCodeTextBox.Text = "";
			}

			else
			{
				PriceTextBox.Text = prod.Price.ToString();
				VendorCodeTextBox.Text = prod.VendorCode;
			}

		}

		private void ProductTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_updateProductComboBox(((sender as ComboBox).SelectedItem as ProductCategory).ID);
		}
	}
}
