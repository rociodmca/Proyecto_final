using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelBBDD
    {
        AccesoBBDD bbdd;

        public ViewModelBBDD() 
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

        public string ObtenerId(string email, string pass)
        {
            try
            {
                Usuario user = bbdd.GetUser(email, pass);
                return user.Id;
            } catch (Exception ex) { return "1"; }
            
        }
    }
}
