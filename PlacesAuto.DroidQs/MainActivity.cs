using Android.App;
using Android.Widget;
using Android.OS;

namespace PlacesAuto.DroidQs
{
	[Activity(Label = "PlacesAuto", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		const string API_KEY = "AIzaSyDs_6pOJoKIUtgSuhndO_5EUKicEwvaxtQ";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			//button.Click += delegate { button.Text = $"{count++} clicks!"; };
		}
	}
}

