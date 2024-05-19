using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_final.Model
{
    public class CatAPI
    {
        public JsonDocument miJson1;

        //Método para listar categorias
        public async Task<bool> ListarRazasAsync()
        {
            string respuesta;
            var direccion = new Uri("https://api.thecatapi.com/v1/breeds");
            using (var httpClient = new HttpClient { BaseAddress = direccion })
            {
                string consulta = "?&page=0";
                using (var response = await httpClient.GetAsync(consulta))
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    miJson1 = JsonDocument.Parse(respuesta);
                    if (miJson1 != null) return true;
                    else return false;
                }
            }
        }

    }
}
