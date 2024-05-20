using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelCita
    {
        public List<Cita> Citas { get; set; }

        public ViewModelCita()
        {
            Citas = new List<Cita>();
        }
    }
}
