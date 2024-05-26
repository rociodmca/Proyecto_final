using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class CitaTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DateTime hoy = DateTime.Today;
            int comparador = DateTime.Compare(((Cita)item).Fecha, hoy);
            if (comparador < 0)
            {
                return InvalidTemplate;
            } else
            {
                return ValidTemplate;
            }
        }
    }
}
