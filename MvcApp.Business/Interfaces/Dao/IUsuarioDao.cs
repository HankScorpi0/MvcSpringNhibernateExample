using IrisLib.Architecture.Interface.Dao;
using MvcApp.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp.Business.Interfaces.Dao
{
    public interface IUsuarioDao : IGenericDao<Usuario, int>
    {
        Usuario Login(string email, string clave);

        Usuario GetEmail(string email);

        Usuario GetUsername(string username);

        int NumUsuarios();
    }
}
