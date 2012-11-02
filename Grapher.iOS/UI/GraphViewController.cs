using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Timers;
using System.Linq;


namespace Grapher
{
	public class GraphViewController : UIViewController
	{
		GraphView graphView;
		public GraphViewController ()
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			graphView = new GraphView(){Orientation = this.InterfaceOrientation};
			this.View = graphView;

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
				var coData = Database.Main.GetCo2(1,since).Cast<Telemetry>().ToList();
				this.InvokeOnMainThread(delegate{
					graphView.OxygenGraph.Data = oData;
					graphView.Co2Graph.Data = coData;
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
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			graphView.Orientation = this.InterfaceOrientation;
		}
	}
}

