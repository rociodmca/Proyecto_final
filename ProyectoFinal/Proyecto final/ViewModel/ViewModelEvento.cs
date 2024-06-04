using MongoDB.Bson;
using Plugin.Maui.Calendar.Models;
using Proyecto_final.Model;
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

        public EventCollection RellenarEventos(ObjectId id)
        {
            eventos.Clear();
            ViewModelBBDD viewModelBBDD = new ViewModelBBDD();
            foreach (var cita in viewModelBBDD.ObtenerListaCitas(id))
            {
                string nombre = viewModelBBDD.ObtenerNombre(cita.Id_veterinario);
                string mascota = viewModelBBDD.ObtenerNombreMascota(cita.Id_mascota);
                if (!eventos.ContainsKey(cita.Fecha))
                {
                    eventos.Add(cita.Fecha, new List<EventModel>
                    {
                        new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" }
                    });
                }
                else if (eventos.ContainsKey(cita.Fecha))
                {
                    ICollection<EventModel> eventos2 = (ICollection<EventModel>)eventos[cita.Fecha];
                    eventos2.Add(new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" });
                }
            }
            return eventos;
        }

    }
}
