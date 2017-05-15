using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_ChatOnline.SignalR
{
    public class ChatHub :Hub
    {
        public void LetsChat(string CI_Name, string CI_Message)
        {
            //Clients.Others.NewMessage(CI_Name, CI_Message);
            Clients.All.NewMessage(CI_Name, CI_Message);
        }
    }
}
