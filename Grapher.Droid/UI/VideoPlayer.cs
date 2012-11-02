
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Grapher
{
	[Activity (Label = "VideoPlayer")]			
	public class VideoPlayer : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.VideoPlayer);
			var videoView = FindViewById<VideoView> (Resource.Id.SampleVideoView);
			var uri = Android.Net.Uri.Parse ("http://ia600507.us.archive.org/25/items/Cartoontheater1930sAnd1950s1/PigsInAPolka1943.mp4");
			videoView.SetVideoURI (uri);
			videoView.Start ();
		}
	}
}

