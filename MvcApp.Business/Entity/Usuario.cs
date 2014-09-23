using IrisLib.Architecture.Entity.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp.Business.Entity
{
    public class Usuario : GenericEntity
    {
        public virtual string Email { get; set; }

        public virtual string Clave { get; set; }

        public virtual string Nombre { get; set; }

        public virtual int NumInvitaciones { get; set; }

        public virtual DateTime FechaCreacion { get; set; }

        public virtual string Logo { get; set; }

        public virtual string PrivateKeyCryptsy { get; set; }

        public virtual string PublicKeyCryptsy { get; set; }

        public virtual bool Premium { get; set; }

        public virtual DateTime FechaFinPremium { get; set; }

        public virtual bool Administrador { get; set; }

        //No persistida
        public virtual bool LicenciaCaducada
        {
            get
            {
                if (FechaFinPremium.CompareTo(DateTime.Now) < 0)
                {
                    return true;
                }

                return false;
            }
        }

        public virtual int NumConexiones { get; set; }

        public virtual DateTime UltConexion { get; set; }
    }
}
