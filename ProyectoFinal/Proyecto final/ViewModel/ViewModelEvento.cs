using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelEvento
    {
        public EventCollection eventos;

        public ViewModelEvento()
        {
            eventos = new EventCollection();
        }

    }
}
