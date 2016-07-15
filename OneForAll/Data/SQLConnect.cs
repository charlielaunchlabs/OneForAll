using System;
using SQLite.Net;
using Xamarin.Forms;

namespace OneForAll
{
	public interface SQLConnect 
	{
		SQLiteConnection GetConnection();
	}
}


 