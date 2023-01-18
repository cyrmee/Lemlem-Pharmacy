using LemlemPharmacy.DTOs;
using SMSClient;
using ErrorEventArgs = SMSClient.ErrorEventArgs;
using ErrorEventHandler = SMSClient.ErrorEventHandler;

namespace LemlemPharmacy.Services
{
	public static class SMSService
	{
		private static Response WriteEvent(string myEvent)
		{
			string status;
			if (myEvent.Contains("could not")) status = "Error";
			else status = "Success";

			return new Response()
			{
				Status = status,
				Message = myEvent
			};
		}

		#region Events
		static void MySMSClient_OnMessageReceived(object sender, DeliveryEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + "Message received. Sender address: " + e.Senderaddress + " Message text: " + e.Messagedata + "\r\n");
		}

		static void MySMSClient_OnMessageDeliveryError(object sender, DeliveryErrorEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + "Message could not be delivered. ID: " + e.Messageid + " Error message: " + e.ErrorMessage + "\r\n");
		}

		static void MySMSClient_OnMessageDeliveredToHandset(object sender, DeliveryEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + "Message delivered to handset. ID: " + e.Messageid + "\r\n");
		}

		static void MySMSClient_OnMessageDeliveredToNetwork(object sender, DeliveryEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + "Message delivered to network. ID: " + e.Messageid + "\r\n");
		}

		static void MySMSClient_OnMessageAcceptedForDelivery(object sender, DeliveryEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + "Message accepted for delivery. ID: " + e.Messageid + "\r\n");
		}

		static void MySMSClient_OnClientConnectionError(object sender, ErrorEventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " " + e.ErrorMessage + "\r\n");
		}

		static void MySMSClient_OnClientDisconnected(object sender, EventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " Disconnected from the SMS gateway " + "\r\n");
		}

		static void MySMSClient_OnClientConnected(object sender, EventArgs e)
		{
			WriteEvent(DateTime.Now.ToString() + " Successfully connected to the SMS gateway " + "\r\n");
		}
		#endregion

		public static void SendSMS(string phoneNo, string message)
		{
			ozSMSClient mySMSClient = new ozSMSClient();
			mySMSClient.OnClientConnected += new SimpleEventHandler(MySMSClient_OnClientConnected);
			mySMSClient.OnClientDisconnected += new SimpleEventHandler(MySMSClient_OnClientDisconnected);
			mySMSClient.OnClientConnectionError += new ErrorEventHandler(MySMSClient_OnClientConnectionError);
			mySMSClient.OnMessageAcceptedForDelivery += new DeliveryEventHandler(MySMSClient_OnMessageAcceptedForDelivery);
			mySMSClient.OnMessageDeliveredToNetwork += new DeliveryEventHandler(MySMSClient_OnMessageDeliveredToNetwork);
			mySMSClient.OnMessageDeliveredToHandset += new DeliveryEventHandler(MySMSClient_OnMessageDeliveredToHandset);
			mySMSClient.OnMessageDeliveryError += new DeliveryErrorEventHandler(MySMSClient_OnMessageDeliveryError);
			mySMSClient.OnMessageReceived += new DeliveryEventHandler(MySMSClient_OnMessageReceived);

			mySMSClient.Username = "admin";
			mySMSClient.Password = "Mercyrme.";
			mySMSClient.Host = "100.126.115.108";
			mySMSClient.Port = 9500;

			mySMSClient.Connected = true;

			mySMSClient.sendMessage(
				phoneNo, 
				message, 
				"vp=" + DateTime.Now + 
				"&ttt=werwerwe rewwe34232 1");

			mySMSClient.Connected = false;
		}
	}
}
