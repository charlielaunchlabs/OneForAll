using System;
using Newtonsoft.Json.Linq;
using OneForAll;
using OneForAll.iOS;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace OneForAll.iOS
{
	public class LoginPageRenderer : PageRenderer 
	{
		bool done = false;
		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			if (done)
				return;

			var auth = new OAuth2Authenticator(
				clientId: "266634353713286", // your OAuth2 client id
				scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
				authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
				redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

			auth.Completed += async (sender, eventArgs) =>
			{
				DismissViewController(true, null);

				App.HideLoginView();

				if (eventArgs.IsAuthenticated)
				{
					var accessToken = eventArgs.Account.Properties["access_token"].ToString();
					var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
					var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

					var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
					var response = await request.GetResponseAsync();
					var obj = JObject.Parse(response.GetResponseText());

					var id = obj["id"].ToString().Replace("\"", "");
					var name = obj["name"].ToString().Replace("\"", "");

					await App.NavigateToHome(name);
				}
				else {
					await App.NavigateToLogin();
				}
			};

			done = true;
			PresentViewController(auth.GetUI(), true, null);
		}
	}
}


