using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OneForAll
{
	public class App : Application
	{

		static TableAcces dbUtil;

		public static TableAcces DAUtil
		{
			get
			{
				if (dbUtil == null)
				{
					dbUtil = new TableAcces();
				}
				return dbUtil;
			}
		}

		public static Action HideLoginView
		{
			get
			{
				return new Action(() => App.Current.MainPage.Navigation.PopModalAsync());
			}
		}

		public async static Task NavigateToHome(string message)
		{
			await App.Current.MainPage.Navigation.PushAsync(new HomePage(message));
		}


		public async static Task NavigateToLogin()
		{
			await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
		}
		public App()
		{

			MainPage = new NavigationPage(new Main());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

