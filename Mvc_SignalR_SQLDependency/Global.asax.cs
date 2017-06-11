using Microsoft.AspNet.SignalR;
using Mvc_SignalR_SQLDependency.Models;
using Mvc_SignalR_SQLDependency.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mvc_SignalR_SQLDependency
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //------------
            //--on vide la table des users et chat à chaque de demarrae de l'appli...
            new NotificationRespository().CLearTable_Chat();
            //-------------
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["mySQLDependencyEntities"].ConnectionString);
            RegisterNotification();
        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["mySQLDependencyEntities"].ConnectionString);
        }

        private void RegisterNotification()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["mySQLDependencyEntities"].ConnectionString;

            string commandText = @"SELECT [ID]
                                  ,[Text]
                                  ,[UserID]
                                  ,[CreatedDate]
                                  FROM [dbo].[NotificationList]";

            //SqlDependency.Start(connectionString);
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand command=new SqlCommand(commandText,connection))
                {
                    command.Notification = null;

                    var sqlDependency = new SqlDependency(command);
                    sqlDependency.OnChange += new OnChangeEventHandler(sqlDependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    IEnumerable<NotificationList> _lstReturn;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                     /*   
                        _lstReturn= reader.Cast<IDataRecord>()
                            .Select(x => new NotificationList
                            {
                                ID = x.GetInt32(0),
                                Text = x.GetString(1),
                                UserID = x.GetString(2)
                                //,CreatedDate =DateTime.Parse(x.GetString(3))
                            }).ToList();
                        */
                    }
                }
                
            }


        }

        DateTime LastRun;
         
        private void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if(e.Info==SqlNotificationInfo.Insert)
            {

                //This is how signalrHub can be accessed outside the SignalR Hub Notification.cs file
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>(); ///regarder ici pr la suite
                NotificationRespository objRepos = new NotificationRespository();

                List<NotificationList> objList = objRepos.GetLatestNotifications(LastRun);
               //List<NotificationList> objList = objRepos.GetLatestNotifications(LastRun,"IB");

                LastRun = DateTime.Now.ToUniversalTime();
                

                foreach (var item in objList)
                {
                    UsersChat _userRecipient = objRepos.GetUser_ofUserID(item.UserID);
                    //context.Clients.User("<DomaineName>" + item.UserID).addLatestNotification(item);
                    
                    
                    //context.Clients.User(item.UserID).addLatestNotification(item);
                    //context.Clients.All.addLatestNotification(item);

                    //context.Clients.Client(item.UserID).addLatestNotification(item);
                    context.Clients.Client(_userRecipient.IDConnection.ToString()).addLatestNotification(item);
                    //context.Clients.Cl
                }

                
                //NotificationHub.DisplayAllNotifications();
            }

            RegisterNotification();
        }
    }
}