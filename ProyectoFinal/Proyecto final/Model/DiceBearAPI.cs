using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    /// <summary>
    /// Clase DiceBearAPI
    /// </summary>
    public class DiceBearAPI
    {
        DiceBear diceBear;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiceBearAPI()
        {
            diceBear = new DiceBear();
        }

        /// <summary>
        /// Método para generar un avatar dada una semilla
        /// </summary>
        /// <param name="seed">semilla a partir de la cual se generará el avatar</param>
        /// <returns>una ImageSource a partir de la semilla pasada por parámetro</returns>
        public ImageSource OnGenerateAvatar(string seed)
        {
            var avatarUrl = diceBear.Adventurer("png", seed, 100, 100);
            return ImageSource.FromUri(new Uri(avatarUrl));
        }
    }
}
