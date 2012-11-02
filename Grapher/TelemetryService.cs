using System;
using WebSocket4Net;

namespace Grapher
{
	public class WebSocketService
	{
		WebSocket webSocket;

		private static WebSocketService instance;
		public static WebSocketService Instance {
			get {
				if(instance == null) {
					instance = new WebSocketService();
				}
				return instance;
			}
		}

		public WebSocketService() {
			Console.WriteLine("Initializing Service..");

			webSocket = new WebSocket("ws://127.0.0.1:1337/");
			webSocket.Opened += new EventHandler(websocket_Opened);
			webSocket.MessageReceived += webSocket_MessageReceived;
			webSocket.Open();
		}

		void webSocket_MessageReceived (object sender, MessageReceivedEventArgs e)
		{
			Console.WriteLine(e.Message.ToString());
		}
				
		private void websocket_Opened(object sender, EventArgs e) {
			Console.WriteLine("New Opened Connection");
			webSocket.Send("message");
		}

		public void GetStatus() {


			Console.WriteLine(webSocket.State);
		}

	}
}

