using System;
using System.Collections.Generic;
using SQLite;
using SQLite.Net.Attributes;
using Xamarin.Forms;

namespace OneForAll
{

	public class CategoryList
	{
		public string id { get; set; }
		public string name { get; set; }
	}

	public class Location
	{
		public string street { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string country { get; set; }
		public string zip { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public string located { get; set; }
	}

	public class Datum
	{
		public string category { get; set; }
		public List<CategoryList> category_list { get; set; }
		public Location location { get; set; }
		public string name { get; set; }
		public string id { get; set; }
	}

	public class Paging
	{
		public string next { get; set; }
	}

	public class RootObject
	{
		public List<Datum> data { get; set; }
		public Paging paging { get; set; }
	}

	public class ListContent
	{
		public string Name { get; set; }
		public string Locations { get; set; }
		public string Category { get; set; }
		public string ID { get; set; }
	}

	public class LogContent
	{
		[PrimaryKey, AutoIncrement]
		public long ID{ get; set; }
		public string Name { get; set; }
		public string Time { get; set; }


	}
}


