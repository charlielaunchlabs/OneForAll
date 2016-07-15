using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace OneForAll
{


	public class HomePage : CarouselPage
	{
		
		Entry lll = new Entry();
		ContentPage firstContent = new ContentPage();
		ListView list = new ListView()
		{
			ItemTemplate = new DataTemplate(() =>
			{
				var textCell = new TextCell();
				textCell.SetBinding(TextCell.TextProperty, "Name");
				textCell.SetBinding(TextCell.DetailProperty, "Category");
				return textCell;
			})
		};

		WebView web = new WebView()
		{

			Source = new UrlWebViewSource { Url = "http://www.facebook.com" },
			HeightRequest = 1000,
			WidthRequest = 1000
		};


		Button xa = new Button();
		ObservableCollection<ListContent> item = new ObservableCollection<ListContent>();


		public HomePage(string message)
		{
			DateTime now = DateTime.Now.ToLocalTime();

			try
			{ 
					var log = new LogContent()
				{
					Name = message,
					Time = now.ToString()
				};
					App.DAUtil.AddLog(log);
			}
			catch { }

			StackLayout first = new StackLayout
			{
				Children = 
				{
					new Label 
					{ 
						HorizontalTextAlignment = TextAlignment.Center, 
						Text = message 
					},
					lll,
					list
				}
			};


			firstContent.Content = first;

			Children.Add(firstContent);
			Children.Add(new LogPage());
			lll.TextChanged += watda;

		}


		async void watda(object sender, TextChangedEventArgs e)
		{

			//ResultLabel.Text = "Loading...";

			// await! control returns to the caller
			var Result = await DownloadHomepage(e.NewTextValue);

			// when the Task<int> returns, the value is available and we can display on the UI
			//ResultLabel.Text = "Result: " + Result;

			if (Result == "Null")
			{
				item.Clear();
			}
			else
			{

				List<RootObject> roles = new List<RootObject>();

				JsonTextReader reader = new JsonTextReader(new StringReader(Result));
				reader.SupportMultipleContent = true;

				while (true)
				{
					if (!reader.Read())
					{
						break;
					}

					JsonSerializer serializer = new JsonSerializer();
					RootObject role = serializer.Deserialize<RootObject>(reader);

					roles.Add(role);

				}
				item.Clear();
				foreach (RootObject role in roles)
				{

					for (int i = 0; i < role.data.Count; i++)
					{
						try
						{ 
							item.Add(new ListContent() { Name = role.data[i].name, Category = role.data[i].category, Locations = role.data[i].location.city + "," + role.data[i].location.state, ID = role.data[i].id });
							list.ItemsSource = item;
							list.BindingContext = item;
						}
						catch 
						{
							
						}
					}

				}
			}

		}



		public async Task<string> DownloadHomepage(string q)
		{
			try
			{
				string url = "https://graph.facebook.com/search?q=" + q + "&type=place&access_token=296505937365008|QpUUBZQ5215gYnTvDgdwK4r3OI0";

				var httpClient = new HttpClient(); // Xamarin supports HttpClient!

				Task<string> contentsTask = httpClient.GetStringAsync(url); // async method!

				// await! control returns to the caller and the task continues to run on another thread
				string contents = await contentsTask;

				//ResultEditText.Text += "DownloadHomepage method continues after async call. . . . .\n";

				// After contentTask completes, you can calculate the length of the string.
				//int exampleInt = contents.Length;

				//ResultEditText.Text += "Downloaded the html and found out the length.\n\n\n";

				//ResultEditText.Text += contents; // just dump the entire HTML

				return contents;
			}
			catch
			{
				return "Null";
			}// Task<TResult> returns an object of type TResult, in this case int
		}
	}
}


