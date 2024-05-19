﻿using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{

    public class ViewModelCatAPI
    {
        CatAPI catAPI = new CatAPI();
        JsonDocument miJson1;
        bool flag;

        public ViewModelCatAPI()
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
    }
}
