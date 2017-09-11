using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR;

namespace GBFTwitterFinderWeb.Hubs
{
    public class TwitterHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.pushNewTwitterMessage(message);
        }

        public static void SendBattleInfo(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<TwitterHub>();
            context.Clients.All.pushNewTwitterMessage(message);
        }
    }
}