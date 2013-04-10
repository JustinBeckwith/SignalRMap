using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SignalRMap
{
    /// <summary>
    /// This is the only class you need to create to speak with the client.  It's methods are directly callable
    /// from javascript (neato)
    /// </summary>
    public class MapHub : Hub
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
            _clients.Add(this.Context.ConnectionId, message);

            // let all of the clients know a new user is here
            this.Clients.All.addClient(message);

            // tell the caller about the full list
            this.Clients.Caller.addClients(_clients.ToArray());
        }

        public override Task OnDisconnected()
        {
            _clients.Remove(Context.ConnectionId);
            return base.OnDisconnected();
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