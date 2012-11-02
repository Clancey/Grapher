using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Grapher.Droid
{
	[Activity (Label = "Grapher.Droid")]
	public class Activity1 : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.GraphView);

			// Get our button from the layout resource,
			// and attach an event to it

			//button.Click += delegate {
			//	button.Text = string.Format ("{0} clicks!", count++);
			//};
		}
	}
}


