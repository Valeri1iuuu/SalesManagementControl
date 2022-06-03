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

using SalesManagementControl.Windows.Client;

namespace SalesManagementControl.Pages
{
	public partial class ClientPage : Page
	{
		class Entry
		{
			public int ID { get; set; }
			public string FullName { get; set; }
			public Gender Gender { get; set; }
			public char GenderCode { get => Gender.Name[0]; }
			public string PhoneNumber { get; set; }
			public string Email { get; set; }
			public DateTime BirthDate { get; set; }
			public string WalletNumber { get; set; }
		}

		public ClientPage(ref MainWindow.SearchHandler searchEvent)
		{
			InitializeComponent();
			LoginTextBlock.Text = Globals.CurrentUser.Login;

			UpdateList("");
			searchEvent += UpdateList;
		}

		public void UpdateList(string text)
		{
			ClientList.ItemsSource = Globals.Context.Client
				.Select(e => new Entry()
				{
					ID = e.ID,
					FullName = e.LastName + " " + e.FirstName + " " + e.Patronymic,
					Gender = e.Gender,
					PhoneNumber = e.PhoneNumber,
					Email = e.Email,
					BirthDate = e.BirthDate,
					WalletNumber = e.WalletNumber

				})
				.Where(e => e.FullName.ToLower().Contains(text))
				.ToList();

			EntryCountTextBlock.Text = ClientList.Items.Count.ToString();
		}


		private void OnAddClient(object sender, RoutedEventArgs e)
		{
			ClientAddWindow clientAddWindow = new ClientAddWindow();
			clientAddWindow.ShowDialog();
			UpdateList("");
		}

		private void OnModifyClient(object sender, RoutedEventArgs e)
		{
			Entry entry = (ClientList.SelectedItem as Entry);
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

			ClientModifyWindow clientModifyWindow = new ClientModifyWindow(
				Globals.Context.Client.SingleOrDefault(x => x.ID == entry.ID)
			);

			clientModifyWindow.ShowDialog();
			UpdateList("");
		}

		private void OnDeleteClick(object sender, RoutedEventArgs e)
		{
			Entry entry = (ClientList.SelectedItem as Entry);
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
					Globals.Context.Client.Remove(
						Globals.Context.Client
							.SingleOrDefault(x => x.ID == entry.ID));

					Globals.Context.SaveChanges();
					UpdateList("");
				}
			}
		}
	}
}
