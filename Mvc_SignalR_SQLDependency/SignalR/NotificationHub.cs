using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Mvc_SignalR_SQLDependency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
 

 

namespace Mvc_SignalR_SQLDependency.SignalR
{
    public class NotificationHub : Hub
    {
        public static string _user;
        myUser user = new myUser();
         NotificationRespository objRepository = new NotificationRespository();
           
        //methode que chaque clients invoquera afin d'envoyer des notification au server de BDD
        public void SendNotification(string message, string user)
        {

           // NotificationRespository objRepository = new NotificationRespository();
            var xx = Context.ConnectionId;

            objRepository.AddNotification(message, user);

            
        }
        
        //[HubMethodName("set_UserConnected)")]
        public void setUserConnected(string _UserConnected)
        {
            
           _user= _UserConnected;
           //string _userValue = Context.QueryString["myUserName"].ToString();
            NotificationRespository objRepository = new NotificationRespository();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            //context.Clients.All.refreshNotification(objRepository.GetNotifications_All());
            //context.Clients.User(_userValue).refreshNotification(objRepository.GetNotifications(_userValue));
            context.Clients.Client(user.ConnectionID).refreshNotification(objRepository.GetNotifications(user.Login));
           
        }


        //get the notifications for the logged in user and send the response back
        public override Task OnConnected()
        {
            /*
            NotificationRespository objRepository = new NotificationRespository();
            Clients.User(Context.User.Identity.Name).refreshNotification(objRepository.GetNotifications(Context.User.Identity.GetLogin()));
            //Clients.User(Context.User.Identity.Name).refreshNotification(objRepository.GetNotifications("@IB"));
            */
            //@me-----Ok pour All clients ,meme affichage...
            /*
            NotificationRespository objRepository = new NotificationRespository();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            //context.Clients.All.refreshNotification(objRepository.GetNotifications_All());
            context.Clients.All.refreshNotification(objRepository.GetNotifications(_user));
            */
            /*--OK 22052017
            NotificationRespository objRepository = new NotificationRespository();
            user.ConnectionID= Context.ConnectionId;*/
            user.ConnectionID = Context.ConnectionId;
            user.Login = Context.QueryString["myUserName"].ToString();
            //-----------Chargement de toutes les notifications Concernée....
            Clients.Client(user.ConnectionID).refreshNotification(objRepository.GetNotifications(user.Login));

            //user in BDD
            objRepository.AddUser(user);
            //Clients.Client(user.ConnectionID+"/"+user.Login).refreshNotification(objRepository.GetNotifications(user.Login));
            
            //string userssss = Context.User.Identity.Name;

            return base.OnConnected();
        }

        public static void DisplayAllNotifications()
        {
            NotificationRespository objRepository = new NotificationRespository();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.refreshNotification(objRepository.GetNotifications_All());
        }

    }

    public static class Extensions
    {
        public static string GetDomain(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(0, stop) : string.Empty;
        }

        public static string GetLogin(this IIdentity identity)
        {
            string s = identity.Name;
            int stop = s.IndexOf("\\");
            return (stop > -1) ? s.Substring(stop + 1, s.Length - stop - 1) : string.Empty;
        }
    }
}
