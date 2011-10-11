using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using SignalR.Hubs;

namespace SignalRMap
{
	public class MapHub : Hub, IDisconnect
	{

		private static readonly Dictionary<string, MapClient> _clients = new Dictionary<string, MapClient>();


		public void Join(MapClient message)
		{
			_clients.Add(this.Context.ClientId, message);
			Clients.addClient(message);
			this.Caller.addClients(_clients.ToArray());
		}


		public void Disconnect()
		{
			MapClient client = _clients[Context.ClientId];
			_clients.Remove(Context.ClientId);
			Clients.removeClient(client);
		}
		
		public class MapClient
		{
			public string clientId { get; set; }
			public Location location { get; set; }

			public class Location
			{
				public float latitude { get; set; }
				public float longitude { get; set; }
			}


		}


	}

	
}