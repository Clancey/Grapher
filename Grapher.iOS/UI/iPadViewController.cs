using System;
using MonoTouch.UIKit;

namespace Grapher
{
	public class iPadViewController : UIViewController
	{
		public iPadViewController ()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View = new iPadView(this);
		}
	}
}

