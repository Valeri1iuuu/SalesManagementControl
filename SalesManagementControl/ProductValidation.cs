using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagementControl
{
	public static class ProductValidation
	{
		public static bool NameValidation(string text)
		{
			return !String.IsNullOrWhiteSpace(text);
		}

		public static bool PriceValidation(string text)
		{
			return Double.TryParse(text, out double num) && num > 0;
		}

		public static bool AmountValidation(string text)
		{
			return Int32.TryParse(text, out int num) && num > 0;
		}

		public static bool ProductCodeValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (text.Length != 10 || text.Any(c => !Char.IsLetterOrDigit(c)))
				return false;

			return true;
		}

		public static bool VendorCodeValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (text.Length != 8 || text.Any(c => !Char.IsLetterOrDigit(c)))
				return false;

			return true;
		}

		public static bool DimensionValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (!Double.TryParse(text, out double num) || num > 1000 || num < 0)
				return false;

			return true;
		}

		public static bool WeightValidation(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
				return false;

			if (!Double.TryParse(text, out double num) || num > 10 || num < 0)
				return false;

			return true;
		}
	}
}