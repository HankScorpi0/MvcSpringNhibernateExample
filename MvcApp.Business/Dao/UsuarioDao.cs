using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using IrisLib.Architecture.Data;
using MvcApp.Business.Interfaces.Dao;
using MvcApp.Business.Entity;

namespace MvcApp.Business.Dao
{
    public class UsuarioDao : GenericDao<Usuario, int>, IUsuarioDao
    {
        public Usuario Login(string email, string clave)
        {
            try
            {
                var claveCifrada = "";
                var result = (from item in SessionFactory.GetCurrentSession().Query<Usuario>()
                              where item.Email.Equals(email) && item.Clave.Equals(claveCifrada)
                              select item).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public Usuario GetEmail(string email)
        {
            try
            {
                var result = (from item in SessionFactory.GetCurrentSession().Query<Usuario>()
                              where item.Email.Equals(email)
                              select item).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int NumUsuarios()
        {
            try
            {
                var result = (from item in SessionFactory.GetCurrentSession().Query<Usuario>()
                              select item).Count();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Usuario GetUsername(string username)
        {
            try
            {
                var result = (from item in SessionFactory.GetCurrentSession().Query<Usuario>()
                              where item.Nombre.Equals(username)
                              select item).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
