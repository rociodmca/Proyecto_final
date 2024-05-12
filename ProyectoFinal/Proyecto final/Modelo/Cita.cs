using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Modelo
{
    public class Cita
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Id_cliente { get; set; }
        public string Id_mascota { get; set; }
        public string Fecha { get; set; }
        public string Id_veterinario { get; set; }
    }
}
