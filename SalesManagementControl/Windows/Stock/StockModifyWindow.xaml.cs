using SalesManagementControl.Model;
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

namespace SalesManagementControl.Windows.Stock
{
    /// <summary>
    /// Логика взаимодействия для StockModifyWindow.xaml
    /// </summary>
    public partial class StockModifyWindow : Window
    {

		Model.Product _product;

        public StockModifyWindow(Model.Product product)
        {
            InitializeComponent();
			_product = product;

			ProductTypeComboBox.ItemsSource = Globals.Context.ProductCategory.ToList();
			ProductTypeComboBox.DisplayMemberPath = "Name";
			ProductTypeComboBox.SelectedItem = _product.ProductCategory;

			CountryComboBox.ItemsSource = Globals.Context.Country.ToList();
			CountryComboBox.DisplayMemberPath = "Name";
			CountryComboBox.SelectedItem = _product.Country;

			NameTextBox.Text = _product.Name;
			InStockTextBox.Text = _product.InStock.ToString();
			WidthTextBox.Text = _product.WidthCM.ToString();
			HeightTextBox.Text = _product.HeightCM.ToString();
			LengthTextBox.Text = _product.LengthCM.ToString();
			DescriptionTextBox.Text = _product.Description.ToString();
			PriceTextBox.Text = _product.Price.ToString();
			ProductCodeTextBox.Text = _product.ProductCode;
			VendorCodeTextBox.Text = _product.VendorCode;
			WidthTextBox.Text = _product.WidthCM.ToString();
			WeightTExtBox.Text = _product.WeightKG.ToString();
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
				_product.Name = NameTextBox.Text;
				_product.CategoryID = (ProductTypeComboBox.SelectedItem as ProductCategory).ID;
				_product.ManufacturerCountryID = (CountryComboBox.SelectedItem as Country).ID;
				_product.InStock = Int16.Parse(InStockTextBox.Text);
				_product.WidthCM = decimal.Parse(WidthTextBox.Text);
				_product.HeightCM = decimal.Parse(HeightTextBox.Text);
				_product.LengthCM = decimal.Parse(LengthTextBox.Text);
				_product.Description = DescriptionTextBox.Text;
				_product.Price = decimal.Parse(PriceTextBox.Text);
				_product.ProductCode = ProductCodeTextBox.Text;
				_product.VendorCode = VendorCodeTextBox.Text;
				_product.WeightKG = decimal.Parse(WeightTExtBox.Text);

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
				"Товар успешно изменён",
				"Успех",
				MessageBoxButton.OK,
				MessageBoxImage.Asterisk
			);

			Close();
		}
	}
}
