using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Proyecto_final.Model
{
    public class Cita
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public ObjectId Id_cliente { get; set; }
        public ObjectId Id_mascota { get; set; }
        public DateTime Fecha { get; set; }
        public ObjectId Id_veterinario { get; set; }

        public override string ToString()
        {
            return Id + " " + Id_cliente + " " + Id_mascota + " " + Fecha.ToString();
        }
    }
}
