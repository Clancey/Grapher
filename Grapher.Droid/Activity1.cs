using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Grapher.Droid
{
	[Activity (Label = "Grapher.Droid", MainLauncher = true)]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var button = FindViewById<Button> (Resource.Id.myButton);
			button.Click += delegate {
				StartActivity(typeof(GraphActivity));
			};
			
			var button2 = FindViewById<Button> (Resource.Id.button2);
			button2.Click += delegate {
				StartActivity(typeof(ListViewActivity));
			};

			var button3 = FindViewById<Button>(Resource.Id.movieBtn);
			button3.Click += (sender, e) => {
				StartActivity(typeof(VideoPlayer));
			};

			TelemetryService.Instance.StartListening();
		}
	}
}


