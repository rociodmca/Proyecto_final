using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    /// <summary>
    /// Clase Mascota
    /// </summary>
    public class Mascota
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public int Peso { get; set; }
        public string Vacunas { get; set; }
        public Uri Imagen { get; set; }
        public ObjectId Id_Cliente { get; set; }

        public override string ToString()
        {
            return Nombre + " " + Tipo + " " + Raza + " " + Sexo;
        }
    }
}
