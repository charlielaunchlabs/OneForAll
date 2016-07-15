using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace OneForAll
{
	public class TableAcces
	{
		SQLite.Net.SQLiteConnection db;
		public TableAcces()
		{
			db = DependencyService.Get<SQLConnect>().GetConnection();
			db.CreateTable<LogContent>();
		}

		public int AddLog(LogContent i)
		{
			return db.Insert(i);
		}

		public List<LogContent> LogAll()
		{
			return db.Query<LogContent>("Select * From [LogContent]");
		}
		public int DeleteLog(LogContent i)
		{
			return db.Delete(i);
		}

	}
}

