using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    /// <summary>
    /// Clase Ajuste
    /// </summary>
    public class Ajuste
    {
        public ObjectId Id { get; set; }
        public string Tema { get; set; }
        public string Tam_letra { get; set; }
        public string Idioma { get; set; }
    }
}
