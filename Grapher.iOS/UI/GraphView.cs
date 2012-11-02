using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Timers;
using System.Linq;


namespace Grapher
{
	public class GraphView : UIViewController
	{
		TelemetryGraph oxygenGraph;
		TelemetryGraph co2Graph;
		public GraphView ()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			oxygenGraph = new TelemetryGraph()
			{
				LineColor = UIColor.Black,
				BackgroundColor = UIColor.White,
			};
			this.View = oxygenGraph;
		}
		public override void ViewDidAppear (bool animated)
		{
			StartAnimating();
		}
		public override void ViewDidDisappear (bool animated)
		{
			StopAnimating();
		}
		
		Timer timer;
		public void StartAnimating()
		{
			if(timer != null)
				return;
			timer = new Timer(1000);
			timer.Elapsed += delegate {
				var since = DateTime.UtcNow.AddMinutes(-1);
				var oData = Database.Main.GetOxygen(1,since).Cast<Telemetry>().ToList();
				//var coData = Database.Main.GetCo2(1,since).Cast<Telemetry>().ToList();
				this.InvokeOnMainThread(delegate{
					oxygenGraph.Data = oData;
					//co2Graph.Data = coData;
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
	}
}

