using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelUsuario
    {
        public List<Usuario> Usuarios { get; set; }

        public ViewModelUsuario()
        {
            Usuarios = new List<Usuario>();
        }
    }
}
