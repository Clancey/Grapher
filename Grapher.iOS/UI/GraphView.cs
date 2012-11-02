using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace Grapher
{
	public class GraphView : UIView
	{

		public TelemetryGraph OxygenGraph;
		public TelemetryGraph Co2Graph;
		UIInterfaceOrientation orientation;
		public UIInterfaceOrientation Orientation{
			get{return orientation;}
			set{
				if(orientation == value)return;
				orientation = value;
				this.LayoutSubviews();
				this.SetNeedsDisplay();
			}
		}
		public GraphView ()
		{
			OxygenGraph = new TelemetryGraph()
			{
				LineColor = UIColor.Red,
				BackgroundColor = UIColor.White,
			};
			Co2Graph = new TelemetryGraph()
			{
				LineColor = UIColor.Blue,
				BackgroundColor = UIColor.White,
			};
			this.AddSubview(OxygenGraph);
			this.AddSubview(Co2Graph);
		}
		public override void LayoutSubviews ()
		{
			switch(orientation)
			{
			case UIInterfaceOrientation.LandscapeLeft:
			case UIInterfaceOrientation.LandscapeRight:
				layoutLandScape();
				break;
			default:
				layoutPortrait();
				break;
			}
		}
		const float padding = 10;
		void layoutLandScape()
		{
			var x = 0;
			var y = padding;
			var width = (this.Bounds.Width - padding)/2;
			var height = Bounds.Height;

			var rect = new RectangleF(x,y,width,height);
			OxygenGraph.Frame = rect;

			rect.X = rect.Right + padding;
			Co2Graph.Frame = rect;

		}
		void layoutPortrait()
		{
			
			var x = 0;
			var y = padding;
			var height = (this.Bounds.Height - padding)/2;
			var width = Bounds.Width;

			var rect = new RectangleF(x,y,width,height);
			OxygenGraph.Frame = rect;

			rect.Y = rect.Bottom + padding;
			Co2Graph.Frame = rect;
		}
	}
}

