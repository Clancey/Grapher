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
			View = new iPadView();
		}
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			((iPadView)this.View).DidRotate(fromInterfaceOrientation);
		}
	}
}

