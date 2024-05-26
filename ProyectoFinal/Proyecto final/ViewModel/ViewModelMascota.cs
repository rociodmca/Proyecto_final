using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelMascota
    {
        public List<Mascota> Mascotas { get; set; }

        public ViewModelMascota()
        {
            Mascotas = [];
        }
    }
}
