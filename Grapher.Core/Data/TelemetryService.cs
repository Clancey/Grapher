using System;
using WebSocket4Net;
using SimpleJson;


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
		public void StartListening()
		{
			Console.WriteLine("Started");
		}

		public TelemetryService() {
			Console.WriteLine("Initializing Service..");

#if true
			webSocket = new WebSocket("ws://10.0.1.33:8080/");
#else
			
			webSocket = new WebSocket("ws://127.0.0.1:8080/");
#endif
			webSocket.Opened += new EventHandler(websocket_Opened);
			webSocket.MessageReceived += webSocket_MessageReceived;
			webSocket.Open();
		}

		void webSocket_MessageReceived (object sender, MessageReceivedEventArgs e)
		{

			var obj = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(e.Message);


			//var obj = new JsonObject();
			//obj.
			if (obj == null) {
				return;
			}

			var tele = new Telemetry();
			tele.Name = (string)obj ["Name"];
			tele.SourceId = Int32.Parse(obj["SourceId"].ToString());
			tele.Value = float.Parse(obj["Value"].ToString());
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

