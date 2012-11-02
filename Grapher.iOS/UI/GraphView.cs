using System;
using MonoTouch.UIKit;
using System.Collections.Generic;


namespace Grapher
{
	public class GraphView : UIViewController
	{
		SparklineChart chart;
		public GraphView ()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Random rand = new Random();
			var data = new List<Telemetry>();
			for(int i = 0;i <= 100;i++)
			{
				float d = (float)Math.Sqrt(i);
				Console.WriteLine(d);
				data.Add(new Oxygen(){Value = d,Timestamp = DateTime.Now.AddSeconds(i * .5)});
			}
			chart = new SparklineChart()
			{
				Data = data,
				LineColor = UIColor.Black,
			};
			chart.BackgroundColor = UIColor.White;
			this.View = chart;
		}
		public override void ViewDidAppear (bool animated)
		{
			chart.StartAnimating();
		}
		public override void ViewDidDisappear (bool animated)
		{
			chart.StopAnimating();
		}
	}
}

