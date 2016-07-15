using System;

using Xamarin.Forms;

namespace OneForAll
{
	public class LogPage : ContentPage
	{
		


		public LogPage()
		{
			ListView lista = new ListView();

			lista.ItemTemplate = new DataTemplate(typeof(CustomListChuchu));
			var logs = App.DAUtil.LogAll();
			lista.ItemsSource = logs;
			lista.BindingContext = logs;
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" },lista
				}
			};
		}



		class CustomListChuchu : ViewCell
		{
			

			public CustomListChuchu() 
			{ 

				Label titlelbl = new Label
				{
					TextColor = Color.Accent,
					FontSize = 25,
					FontAttributes = Xamarin.Forms.FontAttributes.Italic,
				};
				titlelbl.SetBinding(Label.TextProperty, "Name");

				Label deslbl = new Label
				{
					TextColor = Color.Accent,
					FontSize = 10,
					FontAttributes = Xamarin.Forms.FontAttributes.Italic,
				};
				deslbl.SetBinding(Label.TextProperty, "Time");

				Button btn = new Button
				{
					Text ="Delete",
					TextColor = Color.White,
					BackgroundColor = Color.Black
				};
				btn.SetBinding(Button.CommandParameterProperty, new Binding("."));

				StackLayout firstStack = new StackLayout
				{
					Orientation = StackOrientation.Vertical,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					Children = {titlelbl,deslbl}
				};

				StackLayout main = new StackLayout 
				{
					Orientation = StackOrientation.Horizontal,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					Children = { firstStack,btn}
				};

				btn.Clicked += async(sender, e) =>
				 {
					var b = (Button)sender;
					var item = (LogContent)b.CommandParameter;
					bool choice = await ((ContentPage)((ListView)((StackLayout)((StackLayout)b.ParentView)//Wrong cast
		   								.ParentView).ParentView).ParentView).DisplayAlert("Clicked",
					                     item. Name + " button was clicked", "Yes","Connect");
					 if (choice)
					 {
						 App.DAUtil.DeleteLog(item);
					}
					
				 };

				View = main;
			}


		}

	}
}


