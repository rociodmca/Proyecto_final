using MongoDB.Bson;
using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelMostrarCita
    {
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Vet {  get; set; }
        public string Pet { get; set; }
        public string Client { get; set; }

        public List<ViewModelMostrarCita> ObtenerCitas(ObjectId id) 
        { 
            AccesoBBDD bbd = new AccesoBBDD();
            List<ViewModelMostrarCita> lista = new List<ViewModelMostrarCita>();
            foreach (var item in bbd.GetDates(id))
            {
                Id = item.Id;
                string fech = item.Fecha.Date.ToString("dd/MM/yyyy");
                Fecha = DateTime.Parse(fech);
                Vet = bbd.GetUser(item.Id_veterinario).Nombre;
                Pet = bbd.GetPet(item.Id_mascota).Nombre;
                Client = bbd.GetUser(item.Id_cliente).Nombre;
                ViewModelMostrarCita cita = new ViewModelMostrarCita();
                cita.Id = Id;
                cita.Fecha = Fecha;
                cita.Vet = Vet;
                cita.Pet = Pet;
                cita.Client = Client;
                lista.Add(cita); 
            }
            return lista;
        }

        public List<ViewModelMostrarCita> ObtenerCitasVet(ObjectId id)
        {
            AccesoBBDD bbd = new AccesoBBDD();
            List<ViewModelMostrarCita> lista = new List<ViewModelMostrarCita>();
            foreach (var item in bbd.GetDatesVet(id))
            {
                Id = item.Id;
                string fech = item.Fecha.Date.ToString("dd/MM/yyyy");
                Fecha = DateTime.Parse(fech);
                Vet = bbd.GetUser(item.Id_veterinario).Nombre;
                Pet = bbd.GetPet(item.Id_mascota).Nombre;
                Client = bbd.GetUser(item.Id_cliente).Nombre;
                ViewModelMostrarCita cita = new ViewModelMostrarCita();
                cita.Id = Id;
                cita.Fecha = Fecha;
                cita.Vet = Vet;
                cita.Pet = Pet;
                cita.Client = Client;
                lista.Add(cita);
            }
            return lista;
        }
    }
}
