using System;

using Xamarin.Forms;

namespace OneForAll
{
	public class Main : ContentPage
	{
		public Main()
		{
			Button btn = new Button();
			btn.Text = "Press";
			Content = new StackLayout
			{
				Children = 
				{
					new Label { Text = "Hello MaiPage" },
					btn
				}
			};

			btn.Clicked += async (sender, e) =>
			{
				await Navigation.PushAsync(new LoginPage());
			};
		}
	}
}


