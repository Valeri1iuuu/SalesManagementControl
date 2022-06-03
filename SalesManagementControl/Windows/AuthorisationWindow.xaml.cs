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

namespace SalesManagementControl.Windows
{
	public partial class AuthorisationWindow : Window
	{
		public AuthorisationWindow()
		{
			InitializeComponent();
		}

		private void OnLogIn(object sender, RoutedEventArgs e)
		{
			var user = Globals.Context.User
				.FirstOrDefault(x => 
					x.Login == LoginTextBox.Text && 
					x.Password == PasswordBox.Password);

			if (user == null)
			{
				MessageBox.Show(
					"Проверьте правильность введённых данных",
					"Ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
					);

				return;
			}

			Globals.CurrentUser = user;
			new MainWindow().Show();
			Close();
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
	}
}