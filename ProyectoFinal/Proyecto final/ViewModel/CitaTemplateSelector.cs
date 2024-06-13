﻿using Proyecto_final.Model;
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

        /// <summary>
        /// Método para comparar si la fecha es anterior a la del día de hoy o no
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DateTime hoy = DateTime.Today.Date;
            int comparador = DateTime.Compare(((ViewModelMostrarCita)item).Fecha.Date, hoy);
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
