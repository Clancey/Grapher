using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using System.Timers;
using Android.Util;

namespace Grapher
{
	public class TelemetryGraph
		: View
	{
		public TelemetryGraph (Context context)
			: base (context)
		{
			Console.WriteLine("Graph");
			this.SetBackgroundColor(Color.Black);
			LineColor = Color.White;
		}
		public TelemetryGraph(Context context, IAttributeSet attr) : base(context,attr)
		{
			Console.WriteLine("Graph");
			this.SetBackgroundColor(Color.Black);
			LineColor = Color.White;
		}
		public List<Telemetry> Data{
			get{return data;}
			set{
				data = value;
				SetupMinMax();
			}
		}

		public Color LineColor
		{
			get;
			set;
		}

		private float min, max;
		private Paint paint;
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
		protected override void OnDraw (Canvas canvas)
		{
			if (this.data == null || this.data.Count() == 0)
			{
				base.OnDraw (canvas);
				return;
			}

			if (this.paint == null)
			{
				this.paint = new Paint();
				this.paint.SetStyle (Paint.Style.Stroke);
			}

			this.paint.Color = LineColor;

			float mx = this.max;
			float yscale = canvas.Height / (mx - this.min);
			float xscale = (float)canvas.Width / 60;

			Path path = new Path();
			path.MoveTo (0, (mx - this.data[0].Value) * yscale);

			for (int i = 1; i < this.data.Count(); i++)
			{
				var telemetry = data[i];
				var x = ((float)(telemetry.Timestamp - DateTime.UtcNow).TotalSeconds * xscale) + (float)canvas.Width;
				var y = (mx - telemetry.Value) * yscale;
				path.LineTo (x,y);
			}

			canvas.DrawPath (path, this.paint);
		}
	}
}