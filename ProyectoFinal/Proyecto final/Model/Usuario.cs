using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int Rol { get; set; }

        public override string ToString()
        {
            return Id + " " + Nombre + " " + Email + " " + Rol.ToString();
        }
    }
}
