using System;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Views;
using System.Threading;

namespace Grapher
{
	public class SparklineChart
		: View
	{
		public SparklineChart (Context context)
			: base (context)
		{
		}

		public List<Telemetry> Data{
			get{return data;}
			set{
				data = value.Where(x=> x.Timestamp >= (DateTime.Now.AddMinutes(-1))).ToList();
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
				float value = this.data[i];
				if (value < min)
					min = value;
				if (value > max)
					max = value;
			}
		}

		Timer timer;
		public void StartAnimating()
		{
			if(timer != null)
				return;
			timer = new Timer(500);
			timer.Elapsed += delegate {
				//this.Context.Run(delegate{
					this.Invalidate();
				//});
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

		protected override void OnDraw (Canvas canvas)
		{
			if (this.data == null || this.data.Length == 0)
			{
				base.OnDraw (canvas);
				return;
			}

			if (this.paint == null)
			{
				this.paint = new Paint();
				this.paint.SetStyle (Paint.Style.Stroke);
			}

			Color color = LineColor;
			if (color == default (Color))
			{
				TypedArray attrs = Context.Theme.ObtainStyledAttributes (new[] { Resource.Attribute.TextColorPrimary });
				this.paint.Color = attrs.GetColor (0, 0xFFFFFF);
			}
			else
				this.paint.Color = color;

			float mx = this.max;
			float yscale = canvas.Height / (mx - this.min);
			float xscale = (float)canvas.Width / 60;

			Path path = new Path();
			path.MoveTo (0, (mx - this.data[0]) * yscale);

			for (int i = 1; i < this.data.Length; i++)
			{
				var telemetry = d[i];
				var x = ((float)(telemetry.Timestamp - DateTime.Now).TotalSeconds * xscale) + (float)canvas.Width;
				var y = (mx - telemetry.Value) * yscale;
				path.LineTo (x,y);
			}

			canvas.DrawPath (path, this.paint);
		}
	}
}