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

using SalesManagementControl.Model;


namespace SalesManagementControl.Windows.Stock
{
	/// <summary>
	/// Логика взаимодействия для StockAddWindow.xaml
	/// </summary>
	public partial class StockAddWindow : Window
	{
		public StockAddWindow()
		{
			InitializeComponent();

			ProductTypeComboBox.ItemsSource = Globals.Context.ProductCategory.ToList();
			ProductTypeComboBox.DisplayMemberPath = "Name";
			ProductTypeComboBox.SelectedIndex = 0;

			CountryComboBox.ItemsSource = Globals.Context.Country.ToList();
			CountryComboBox.DisplayMemberPath = "Name";
			CountryComboBox.SelectedIndex = 0;
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

			if (!ProductValidation.NameValidation(NameTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённое название",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.AmountValidation(InStockTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённое количество",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.DimensionValidation(WidthTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую ширину.\nВозможно, был использован неправильный разделитель",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.DimensionValidation(HeightTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую высоту.\nВозможно, был использован неправильный разделитель",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.DimensionValidation(LengthTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую глубину.\nВозможно, был использован неправильный разделитель",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.PriceValidation(PriceTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую цену.\nВозможно, был использован неправильный разделитель",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.ProductCodeValidation(ProductCodeTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённый код товара.\nОн должен состоять из 10-ти символов",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.VendorCodeValidation(VendorCodeTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённый артикул.\nОн должен состоять из 8-ми символов",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ProductValidation.WeightValidation(WeightTExtBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённый вес.\nВозможно, был использован неправильный разделитель",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			try
			{
				Model.Product product = new Model.Product()
				{
					Name = NameTextBox.Text,
					CategoryID = (ProductTypeComboBox.SelectedItem as ProductCategory).ID,
					ManufacturerCountryID = (CountryComboBox.SelectedItem as Country).ID,
					InStock = Int16.Parse(InStockTextBox.Text),
					WidthCM = decimal.Parse(WidthTextBox.Text),
					HeightCM = decimal.Parse(HeightTextBox.Text),
					LengthCM = decimal.Parse(LengthTextBox.Text),
					Description = DescriptionTextBox.Text,
					Price = decimal.Parse(PriceTextBox.Text),
					ProductCode = ProductCodeTextBox.Text,
					VendorCode = VendorCodeTextBox.Text,
					WeightKG = decimal.Parse(WeightTExtBox.Text)
				};

				Globals.Context.Product.Add(product);
				Globals.Context.SaveChanges();
			}

			catch (Exception ex)
			{
				MessageBox.Show(
					"Что-то пошло не так.\n Возможно, товар с такими данными уже существует",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			MessageBox.Show(
				"Товар успешно добавлен",
				"Успех",
				MessageBoxButton.OK,
				MessageBoxImage.Asterisk
			);

			Close();
		}
	}
}
