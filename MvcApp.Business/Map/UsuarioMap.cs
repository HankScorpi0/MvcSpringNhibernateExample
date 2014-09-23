using FluentNHibernate.Mapping;
using MvcApp.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp.Business.Map
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("USUARIO");
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.Clave);
            Map(x => x.Nombre);           
            Map(x => x.FechaCreacion);            
        }
    }
}
