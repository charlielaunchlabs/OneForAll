using System;
using System.IO;
using OneForAll.Droid;

using SQLite.Net;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteService))]
namespace OneForAll.Droid
{
	public class SQLiteService : SQLConnect
	{
		
		public SQLiteService() { }

		public SQLiteConnection GetConnection()
		{
			var sqliteFilename = "SQLiteEx.db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			Console.WriteLine(path);
			if (!File.Exists(path)) File.Create(path);
			var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
			var conn = new SQLite.Net.SQLiteConnection(plat, path);
			// Return the database connection 
			return conn;
		}
		}
	}


