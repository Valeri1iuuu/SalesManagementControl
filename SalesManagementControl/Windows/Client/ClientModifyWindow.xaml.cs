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

namespace SalesManagementControl.Windows.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientModifyWindow.xaml
    /// </summary>
    public partial class ClientModifyWindow : Window
    {
		Model.Client _client;

        public ClientModifyWindow(Model.Client client)
        {
            InitializeComponent();
			_client = client;

			GenderComboBox.ItemsSource = Globals.Context.Gender.ToList();
			GenderComboBox.DisplayMemberPath = "Name";
			GenderComboBox.SelectedItem = _client.Gender;

			BirthDatePicker.SelectedDate = _client.BirthDate;

			FirstNameTextBox.Text = _client.FirstName;
			LastNameTextBox.Text = _client.LastName;
			PatronymicTextBox.Text = _client.Patronymic;
			PhoneNumberTextBox.Text = _client.PhoneNumber;
			EmailTextBox.Text = _client.Email;
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
			if (!ClientValidation.NameValidation(FirstNameTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённое имя",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ClientValidation.NameValidation(LastNameTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую фамилию",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ClientValidation.NameValidation(PatronymicTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённое отчество",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ClientValidation.PhoneNumberValidation(PhoneNumberTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённый номер телефона",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}


			if (!ClientValidation.EmailValidation(EmailTextBox.Text))
			{
				MessageBox.Show(
					"Проверьте введённую почту",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			if (!ClientValidation.DateValidation(BirthDatePicker.SelectedDate.Value))
			{
				MessageBox.Show(
					"Проверьте введённую дату рождения",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			try
			{
				_client.FirstName = FirstNameTextBox.Text;
				_client.LastName = LastNameTextBox.Text;
				_client.Patronymic = PatronymicTextBox.Text == "" ? null : PatronymicTextBox.Text;
				_client.PhoneNumber = PhoneNumberTextBox.Text;
				_client.Email = EmailTextBox.Text;
				_client.GenderID = (GenderComboBox.SelectedItem as Gender).ID;
				_client.BirthDate = BirthDatePicker.SelectedDate.Value;

				Globals.Context.SaveChanges();
			}

			catch (Exception ex)
			{
				MessageBox.Show(
					"Что-то пошло не так.\n Возможно, клиент с такими данными уже существует",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			MessageBox.Show(
				"Клиент успешно изменён",
				"Успех",
				MessageBoxButton.OK,
				MessageBoxImage.Asterisk
			);

			Close();
		}
	}
}
