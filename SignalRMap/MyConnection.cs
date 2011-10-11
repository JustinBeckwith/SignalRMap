using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using SignalR;

namespace SignalRMap
{
	public class MyConnection : PersistentConnection
	{

		protected override Task OnReceivedAsync(string clientId, string data)
		{
			// Broadcast data to all clients
			return Connection.Broadcast(data);
		}

		protected override Task OnConnectedAsync(HttpContextBase context, string clientId)
		{
			return base.OnConnectedAsync(context, clientId);
		}

		protected override Task OnDisconnectAsync(string clientId)
		{
			return base.OnDisconnectAsync(clientId);
		}
	}
}