using System;
using NHibernate;
using NHibernate.Context;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Common.Logging;
using System.Web;
using MvcApp.Business.Entity;

namespace MvcApp.Business.Data.Conf
{
    /// <summary>
    /// Clase encargada de gestionar la sesión con NHibernate por petición para ASP.NET
    /// </summary>
    public class NHibernateSessionPerRequest : IHttpModule
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(NHibernateSessionPerRequest));
        private static readonly ISessionFactory SessionFactory;
      
        /// <summary>
        /// Constructor
        /// </summary>
        static NHibernateSessionPerRequest()
        {
            SessionFactory = CreateSessionFactory();
        }

        /// <summary>
        /// Función que se encarga de inicializar los disparadores de las peticiones
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        /// <summary>
        /// Función que devuelve la sesión actual
        /// </summary>
        /// <returns></returns>
        public static ISession GetCurrentSession()
        {
            return SessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// Función que devuelve la factoría de la sesión
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory GetSessionFactory()
        {
            return SessionFactory;
        }

        public void Dispose() { }

        /// <summary>
        /// Función que al iniciar una petición gestiona la sesión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BeginRequest(object sender, EventArgs e)
        {
            try
            {
                var session = SessionFactory.OpenSession();
                session.BeginTransaction();
                CurrentSessionContext.Bind(session);
            }
            catch (Exception ex)
            {
                Log.Error("BeginRequest: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Función que cuando finaliza la petición se encarga
        /// de gestionar la sesión
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EndRequest(object sender, EventArgs e)
        {            
            var session = CurrentSessionContext.Unbind(SessionFactory);
            if (session == null) return;
            try
            {
                session.Transaction.Commit();
            }
            catch (Exception ex)
            {
                session.Transaction.Rollback();
                Log.Error("EndRequest: " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }

        /// <summary>
        /// Función encargada de crear la factoría de la sesión de NHibernate
        /// </summary>
        /// <returns></returns>
        public static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var conf = Fluently
                    .Configure()                    
                    .Database(MySQLConfiguration.Standard.ConnectionString(c => c
                        .Server(@"192.168.0.1")
                        .Database("dbo")
                        .Username("amw")
                        .Password("a7uvarapu")
                        ))                    
                    .Mappings(
                        m => m.FluentMappings    
                            .AddFromAssemblyOf<Usuario>()                            
                    )
                    .ExposeConfiguration(
                        cfg => new SchemaUpdate(cfg).Execute(false, true))
                    .ExposeConfiguration(
                        cfg => cfg.SetProperty("current_session_context_class", "web"))
                    .BuildSessionFactory();
                return conf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " - " + ex.InnerException.Message);
            }
        }
    }
}
