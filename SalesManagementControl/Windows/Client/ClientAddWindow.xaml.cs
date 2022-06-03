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
    public partial class ClientAddWindow : Window
    {
        public ClientAddWindow()
        {
            InitializeComponent();
			GenderComboBox.ItemsSource = Globals.Context.Gender.ToList();
			GenderComboBox.DisplayMemberPath = "Name";
			GenderComboBox.SelectedIndex = 0;

			BirthDatePicker.SelectedDate = DateTime.Now;
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
			if(!ClientValidation.NameValidation(FirstNameTextBox.Text))
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
				Model.Client client = new Model.Client()
				{
					FirstName = FirstNameTextBox.Text,
					LastName = LastNameTextBox.Text,
					Patronymic = PatronymicTextBox.Text == "" ? null : PatronymicTextBox.Text,
					PhoneNumber = PhoneNumberTextBox.Text,
					Email = EmailTextBox.Text,
					GenderID = (GenderComboBox.SelectedItem as Gender).ID,
					BirthDate = BirthDatePicker.SelectedDate.Value,
					WalletNumber = _generateWalletCode(EmailTextBox.Text.GetHashCode())
				};

				Globals.Context.Client.Add(client);
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
					"Клиент успешно добавлен",
					"Успех",
					MessageBoxButton.OK,
					MessageBoxImage.Asterisk
					);

			Close();
		}

		private string _generateWalletCode(int key)
		{
			Random rand = new Random(key);
			string str = "";
			for (int i = 1; i <= 23; i++)
			{
				if(i % 6 == 0)
				{
					str = str + "-";
					continue;
				}

				int choice = rand.Next(1, 4);
				if (choice == 1)
					str += (char)rand.Next(48, 58);

				else if (choice == 2)
					str += (char)rand.Next(65, 91);

				else
					str += (char)rand.Next(97, 123);
			}

			return str;
		}
	}
}
