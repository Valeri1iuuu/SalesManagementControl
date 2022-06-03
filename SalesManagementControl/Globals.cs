using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalesManagementControl.Model;

namespace SalesManagementControl
{
	public static class Globals
	{
		public static User CurrentUser { get; set; }

		public static SalesManagementEntities Context { get; private set; }

		static Globals()
		{
			Context = new SalesManagementEntities();
		}
	}
}
