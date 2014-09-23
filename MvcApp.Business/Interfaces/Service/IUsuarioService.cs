using IrisLib.Architecture.Interface.Service;
using MvcApp.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp.Business.Interfaces.Service
{
    public interface IUsuarioService : IGenericService<Usuario, int>
    {
        Usuario Login(string email, string clave);

        Usuario GetEmail(string email);

        Usuario GetUsername(string username);

        int NumUsuarios();
    }
}
