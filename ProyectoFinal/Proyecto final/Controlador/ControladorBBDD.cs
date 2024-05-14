using Proyecto_final.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Controlador
{
    public class ControladorBBDD
    {
        AccesoBBDD bbdd;

        public ControladorBBDD() 
        { 
            bbdd = new AccesoBBDD();
        }

        public bool InsertarUsuario(string nombre, string ape, string email, string pass, int rol)
        {
            try
            {
                bbdd.AddUser(nombre, ape, email, pass, rol);
                return true;
            } catch { return false; }
        }
    }
}
