using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using SignalR.Hubs;

namespace SignalRMap
{
	/// <summary>
	/// This is the only class you need to create to speak with the client.  It's methods are directly callable
	/// from javascript (neato)
	/// </summary>
	public class MapHub : Hub, IDisconnect
	{
		/// <summary>
		/// list of connected clients and their geo data
		/// </summary>
		private static readonly Dictionary<string, MapClient> _clients = new Dictionary<string, MapClient>();

		/// <summary>
		/// called the first time a user shows up to the page
		/// </summary>
		/// <param name="message"></param>
		public void Join(MapClient message)
		{
			// add the user to the list of clients
			_clients.Add(this.Context.ClientId, message);

			// let all of the clients know a new user is here
			Clients.addClient(message);

			// tell the caller about the full list
			this.Caller.addClients(_clients.ToArray());
		}

		/// <summary>
		/// when a user leaves, remove them from the list and notify all clients
		/// </summary>
		public void Disconnect()
		{
			// get a reference to the client who disconnected
			MapClient client = _clients[Context.ClientId];

			// remove the client from the internal list
			_clients.Remove(Context.ClientId);

			// notify all clients the user disconnected
			Clients.removeClient(client);
		}
		
		/// <summary>
		/// model class for the join message.  I tried to use dynamic here, but 
		/// it didn't work.
		/// </summary>
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