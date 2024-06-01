using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelDogAPI
    {
        DogAPI catAPI = new DogAPI();
        JsonDocument miJson1;
        bool flag;

        public ViewModelDogAPI()
        {
            RellenarJson();
        }

        public async Task<bool> RellenarJson()
        {
            flag = await catAPI.ListarRazasAsync();
            if (flag)
            {
                miJson1 = catAPI.miJson1;
            }
            return flag;
        }

        public List<string> RazasList()
        {
            List<string> list = new List<string>();
            if (flag)
            {
                foreach (JsonElement elemento in miJson1.RootElement.EnumerateArray())
                {
                    foreach (JsonProperty propiedad in elemento.EnumerateObject())
                    {
                        if (propiedad.Name == "name")
                        {
                            list.Add(propiedad.Value.ToString());
                        }
                    }
                }
            }
            return list;
        }

        public Dictionary<string,string> DiccionarioRefImagenes()
        {
            List<string> listRazas = new List<string>();
            List<string> listRef = new List<string>();
            Dictionary<string,string> diccRef = new Dictionary<string,string>();
            if (flag)
            {
                listRazas = RazasList();
                foreach (JsonElement elemento in miJson1.RootElement.EnumerateArray())
                {
                    foreach (JsonProperty propiedad in elemento.EnumerateObject())
                    {
                        if (propiedad.Name == "reference_image_id")
                        {
                            listRef.Add(propiedad.Value.ToString());
                        }
                    }
                }
            }
            for (int i = 0; i < listRef.Count; i++)
            {
                diccRef[listRazas[i]] = listRef[i].ToString();
            }
            return diccRef;
        }
    }
}
