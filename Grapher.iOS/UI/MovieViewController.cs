using System;
using MonoTouch.UIKit;
using MonoTouch.MediaPlayer;
using MonoTouch.Foundation;

namespace Grapher
{
	public class MovieViewController : UIViewController
	{
		public MovieViewController ()
		{
		}
		MPMoviePlayerController moviePlayer;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			moviePlayer = new MPMoviePlayerController (new NSUrl ("http://ia600507.us.archive.org/25/items/Cartoontheater1930sAnd1950s1/PigsInAPolka1943.mp4"));
			moviePlayer.View.Frame = new System.Drawing.RectangleF(0,0,View.Bounds.Width, 100);
			View.AddSubview (moviePlayer.View);
			moviePlayer.SetFullscreen (true, true);
			moviePlayer.Play ();
		}
	}
}

