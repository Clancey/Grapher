using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.Foundation;
using System.Timers;

namespace Grapher
{
	public class TelemetryGraph
		: UIControl
	{
		public TelemetryGraph()
		{
		}

		public TelemetryGraph (RectangleF frame)
			: base (frame)
		{

		}
		Timer timer;
		public void StartAnimating()
		{
			if(timer != null)
				return;
			timer = new Timer(500);
			timer.Elapsed += delegate {
				this.BeginInvokeOnMainThread(delegate{
					this.SetNeedsDisplay();
				});
			};
			timer.Start();

		}
		public void StopAnimating()
		{
			if(timer == null)
				return;
			timer.Stop();
			timer = null;
		}

		public UIColor LineColor
		{
			get { return this.lineColor; }
			set {
				if(this.lineColor == value)
					return;
				this.lineColor = value; 
				this.SetNeedsDisplay();
			}
		}

		public List<Telemetry> Data{
			get{return data;}
			set{
				data = value.Where(x=> x.Timestamp >= (DateTime.Now.AddMinutes(-1))).ToList();
				SetupMinMax();
				this.SetNeedsDisplay();
			}
		}
		public override void SetNeedsLayout ()
		{
			base.SetNeedsLayout ();
			SetupMinMax();
		}
	

		private UIColor lineColor;
		private float min, max;
		List<Telemetry> data = new List<Telemetry>();

		private void SetupMinMax()
		{
			this.max = Single.MinValue;
			this.min = Single.MaxValue;

			for (int i = 0; i < this.data.Count(); i++)
			{
				float value = this.data[i].Value;
				if (value < min)
					this.min = value;
				if (value > max)
					this.max = value;
			}
		}

		public override void Draw (RectangleF area)
		{
			if (this.data == null || this.data.Count() == 0)
			{
				base.Draw (area);
				return;
			}

			var color = LineColor.CGColor;
			if (color == null)
				color = new CGColor (255, 255, 255);

			CGContext context = UIGraphics.GetCurrentContext();

			context.BeginPath();
			context.SetStrokeColor (color);
			
			var d = this.data;

			float mx = this.max;
			float yscale = Bounds.Height / (mx - this.min);
			float xscale = Bounds.Width/60;
			context.MoveTo (0, (mx - d[0].Value) * yscale);

			for (int i = 0; i < d.Count(); i++)
			{
				var telemetry = d[i];
				var x = ((float)(telemetry.Timestamp - DateTime.Now).TotalSeconds * xscale) + Bounds.Width;
				var y = (mx - telemetry.Value) * yscale;
				//Console.WriteLine("X:{0} , Y:{1}",x,y);
				context.AddLineToPoint (x,y );
			}

			context.DrawPath (CGPathDrawingMode.Stroke);
		}
	}
}