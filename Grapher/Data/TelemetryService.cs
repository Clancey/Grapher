using System;
using WebSocket4Net;
using System.Json;

namespace Grapher
{
	public class TelemetryService
	{
		WebSocket webSocket;

		private static TelemetryService instance;
		public static TelemetryService Instance {
			get {
				if(instance == null) {
					instance = new TelemetryService();
				}
				return instance;
			}
		}

		public TelemetryService() {
			Console.WriteLine("Initializing Service..");

#if DROID
			webSocket = new WebSocket("ws://10.0.1.15:1337/");
#else
			
			webSocket = new WebSocket("ws://127.0.0.1:1337/");
#endif
			webSocket.Opened += new EventHandler(websocket_Opened);
			webSocket.MessageReceived += webSocket_MessageReceived;
			webSocket.Open();
		}

		void webSocket_MessageReceived (object sender, MessageReceivedEventArgs e)
		{
			var obj = JsonObject.Parse (e.Message);
			if (obj == null) {
				return;
			}

			var tele = new Telemetry();
			tele.Name = (string)obj ["Name"];
			tele.SourceId = (int)obj["SourceId"];
			tele.Value = (float)obj["Value"];
			tele.Timestamp = DateTime.Parse((string)obj["TimeStamp"]);
			
			Database.Main.InsertTelemetryAsync(tele);

			Console.WriteLine(e.Message.ToString());
		}
				
		private void websocket_Opened(object sender, EventArgs e) {
			Console.WriteLine("New Connection Ready");
		}

		public void GetStatus() {
			Console.WriteLine(webSocket.State);
		}



	}
}

