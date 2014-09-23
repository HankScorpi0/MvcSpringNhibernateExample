using IrisLib.Architecture.Service;
using MvcApp.Business.Entity;
using MvcApp.Business.Interfaces.Dao;
using MvcApp.Business.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp.Business.Service
{
    public class UsuarioService : GenericService<Usuario, int>, IUsuarioService
    {
        public IUsuarioDao UsuarioDao { get; set; }

        public Usuario Login(string email, string clave)
        {
            UsuarioDao.SessionFactory = _SessionFactory;
            var result = UsuarioDao.Login(email, clave);
            return result;
        }

        public Usuario GetEmail(string email)
        {
            UsuarioDao.SessionFactory = _SessionFactory;
            var result = UsuarioDao.GetEmail(email);
            return result;
        }

        public int NumUsuarios()
        {
            UsuarioDao.SessionFactory = _SessionFactory;
            var result = UsuarioDao.NumUsuarios();
            return result;
        }

        public Usuario GetUsername(string username)
        {
            UsuarioDao.SessionFactory = _SessionFactory;
            var result = UsuarioDao.GetUsername(username);
            return result;
        }
    }
}
