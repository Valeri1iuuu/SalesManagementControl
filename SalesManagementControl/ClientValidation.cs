using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesManagementControl
{
	public static class ClientValidation
	{
		private static string _phoneNumberSymbs = "()-+";
		private static string _emailregexpr = "..*@..*\\...*";

		public static bool NameValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (text.Any(c => !Char.IsLetter(c)))
				return false;

			return true;
		}

		public static bool PhoneNumberValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (text.Any(c => !Char.IsDigit(c) && !_phoneNumberSymbs.Contains(c)))
				return false;

			return true;
		}

		public static bool EmailValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if(!Regex.IsMatch(text, _emailregexpr))
				return false;

			return true;
		}

		public static bool DateValidation(DateTime datetime)
		{
			return datetime.Date < DateTime.Now.Date;
		}
	}
}
