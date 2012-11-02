using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Grapher
{
	public class iPadView : UIView
	{
		GraphViewController graphVc;
		TableViewController tableVc;
		public iPadView (iPadViewController parent)
		{
			graphVc = new GraphViewController();
			tableVc = new TableViewController();
			parent.AddChildViewController(graphVc);
			parent.AddChildViewController(tableVc);
			this.AddSubview(graphVc.View);
			this.AddSubview(tableVc.View);
		}
		const float padding = 10f;
		public override void LayoutSubviews ()
		{
			var half = (this.Bounds.Height - padding) /2f;
			var rect = new RectangleF(0,0,this.Bounds.Width,half);
			graphVc.View.Frame = rect;
			rect.Y = rect.Bottom + padding;
			tableVc.View.Frame = rect;
		}
	}
}

