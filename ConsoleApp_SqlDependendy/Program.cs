using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SqlDependendy
{
    class Program
    {
        public static string MyConnectionString = ConfigurationManager.ConnectionStrings["mySQLDependencyEntities"].ConnectionString;
        //**requete de notification qui va etre executée à chaque mouvement dans la base de données (table à suivre)!
        //private const string MyFirstNotificationRequest = @"SELECT [ID],[Text],[UserID],[CreatedDate] FROM [SQLDependency].[dbo].[NotificationList] ";
        private const string MyFirstNotificationRequest = @"SELECT [ID],[Text],[UserID],[CreatedDate] FROM [dbo].[NotificationList] ";
        //private const string MyFirstNotificationRequest = @"SELECT [Text] FROM [dbo].[NotificationList] ";
        static  SqlDependency dependency ;
        static void Main(string[] args)
        {
            // start dependency listener
            SqlDependency.Start(Program.MyConnectionString);//--abonnement aux notifications
            /*
             * \todo mon code "myExceution"
             * 
             */

            myExceution(true);
            Console.ReadLine();
            SqlDependency.Stop(Program.MyConnectionString);

            //--------------------------
            //Console.ReadLine();
        }

        //À chaque fois que les données ciblées par les conditions de la requête vont changer (par un ajout, une modification ou une suppression), 
        //l'instance de SqlDependency liée à la commande le saura
        //En fait, un évènement est levé à ce moment. C'est l'évènement OnChange que l'on peut retrouver sur le SqlDependency.
        private static void myExceution(bool list)
        {
            //Remove existing dependency, if necessary
            //if (dependency != null)
            //{
            //    dependency.OnChange-=dependency_OnChange;
            //    dependency = null;
            //}


            using(SqlConnection connection=new SqlConnection(MyConnectionString))
            {
                connection.Open();

                using(SqlCommand command=new SqlCommand(MyFirstNotificationRequest,connection))
                {
                    // Attention : il faut que command soit lié à une instance de SqlConnection (ici, je l'ai fait dans le constructeur de SqlCommand)

                    // Notez que pour s'abonner à une commande , on peut aussi passer par la méthode AddCommandDependency()
                    //----abonnement (incription) à la requête de notification...
                    //À chaque fois que les données ciblées par les conditions de la requête vont changer (par un ajout, une modification ou une suppression),
                    //l'instance de  SqlDependency liée à la commande le saura via l'evenement "OnChange"
                   // SqlDependency dependency = new SqlDependency(command);
                    //dependency = new SqlDependency(command);

                     
                    if (dependency != null)
                       dependency.OnChange -= dependency_OnChange;
                    command.Notification = null;
                    //dependency = new SqlDependency(command,null,5);
                    // Subscribe to the SqlDependency event.
                    dependency = new SqlDependency(command);
                    dependency.OnChange += dependency_OnChange;

                    // start dependency listener
                    //SqlDependency.Start(MyConnectionString);

                    //--execute command et referesh data
                    refreshData(command,list);
                    /*
                    using (SqlDataReader reader = command.ExecuteReader())// C'est ici que se fait l'inscription aux notifications
                    {
                        if (list == true)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["Text"]);
                            }



                            Console.WriteLine();
                        }
                    }
                    */
                }
            }
        }

        private static void refreshData(SqlCommand command,bool list)
        {
           
                using (SqlDataReader reader = command.ExecuteReader())// C'est ici que se fait l'inscription aux notifications
                {
                    if (list == true)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Text"]);
                        }

                        Console.WriteLine();
                    }

                   

                }
            
        }

        static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //SqlDependency dependency = sender as SqlDependency;
            //if(dependency!=null)
            //dependency.OnChange-=dependency_OnChange;
            /*...Gestion des Notifications...*/
            //Notez que l'application et l'instance de SqlDependency se désabonnent à chaque fois qu'une notification est traitée. Il faut donc se réabonner 
            //si on veut une connexion permanente (dans le Handler de l'évènement par exemple).
           
            
            switch (e.Type)
            {
                case SqlNotificationType.Change:
                    Console.WriteLine("Refeshing data due to {0}", e.Source);
                    myExceution(true);
                    string test = SqlNotificationSource.Data.ToString();
                    break;
                case SqlNotificationType.Subscribe:
                    //Console.WriteLine("Data not refresh due to unexcepted SqlNotificationEventArgs:Source={0},Info={1},Type={2}", e.Source, e.Info, e.Type.ToString());
                    myExceution(false);
                    break;
            }
           /* */
            //11062017
            //if((e.Source.ToString()=="Data") || (e.Source.ToString()=="Timeout"))
            //{
                //Console.WriteLine("Refeshing data due to {0}", e.Source);
                //myExceution(true);
            //}
           /* else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Data not refresh due to unexcepted SqlNotificationEventArgs:Source={0},Info={1},Type={2}", e.Source, e.Info, e.Type.ToString());
                Console.ForegroundColor=ConsoleColor.Gray;
            }
            */
        }
    }
}
