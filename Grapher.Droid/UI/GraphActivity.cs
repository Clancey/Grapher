
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Timers;
using Android.Graphics;

namespace Grapher
{
	[Activity (Label = "GraphActivity")]			
	public class GraphActivity : Activity
	{
		TelemetryGraph oxygenGraph;
		TelemetryGraph co2Graph;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			SetContentView (Resource.Layout.GraphView);
			
			oxygenGraph = FindViewById<TelemetryGraph> (Resource.Id.graph1);
			oxygenGraph.LineColor = Color.Red;
			co2Graph = FindViewById<TelemetryGraph> (Resource.Id.graph2);
			co2Graph.LineColor = Color.LightSteelBlue;
			
			TelemetryService.Instance.StartListening();
		}
		protected override void OnResume ()
		{
			base.OnResume ();
			StartAnimating();
		}
		protected override void OnPause ()
		{
			base.OnPause ();
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
				this.RunOnUiThread(delegate{
					oxygenGraph.Data = oData;
					co2Graph.Data = coData;
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

