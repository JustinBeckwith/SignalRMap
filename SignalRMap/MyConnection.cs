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
	}
}