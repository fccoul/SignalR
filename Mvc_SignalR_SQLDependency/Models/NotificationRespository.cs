using Mvc_SignalR_SQLDependency.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc_SignalR_SQLDependency.Models
{
    public class NotificationRespository
    {
        myUser user = new myUser();
       

        public void CLearTable_Chat()
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                ent.Database.ExecuteSqlCommand("TRUNCATE TABLE NotificationList ");
                ent.Database.ExecuteSqlCommand("TRUNCATE TABLE UsersChat ");

                
            }
        }

        public void AddUser(myUser _user)
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                UsersChat obj = new UsersChat();

                obj.IDConnection =Guid.Parse(_user.ConnectionID);
                obj.loginName = _user.Login;                 
                ent.UsersChats.Add(obj);
                ent.SaveChanges();
            }
        }

        public void AddNotification(string Text, string UserName)
        {
            using (SQLDependencyEntities ent=new SQLDependencyEntities())
            {
                NotificationList obj = new NotificationList();
                obj.Text=Text;
                obj.UserID=UserName;
                obj.CreatedDate = DateTime.Now.ToUniversalTime();
                ent.NotificationLists.Add(obj);
                ent.SaveChanges();
            }
        }

        public List<NotificationList> GetNotifications(string userName)
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                return ent.NotificationLists.Where(e => e.UserID == userName).ToList();
            }

        }

        public UsersChat GetUser_ofUserID(string userName)
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                return ent.UsersChats.Where(u => u.loginName == userName).FirstOrDefault();
            }
        }

        public List<NotificationList> GetNotifications_All()
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                return ent.NotificationLists.ToList();
            }

        }

        /// <summary>
        /// selects the list of the notification since the last run, to get the added record after every run, 
        /// which will be called on the SQL dependency event.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<NotificationList> GetLatestNotifications(DateTime dt)
        //public List<NotificationList> GetLatestNotifications(DateTime dt,string _user)
        {
            using (SQLDependencyEntities ent = new SQLDependencyEntities())
            {
                if (dt == DateTime.MinValue)
                    return ent.NotificationLists.ToList();
                else
                {
                    DateTime dtUTC = dt.ToUniversalTime();
                    return ent.NotificationLists.Where(e => e.CreatedDate > dtUTC).ToList();
                    //return ent.NotificationLists.Where(e => e.UserID == _user && e.CreatedDate > dtUTC).ToList();
                }
            }
        }
    }
}
