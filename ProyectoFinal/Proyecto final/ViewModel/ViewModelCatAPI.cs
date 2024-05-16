using Proyecto_final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final.ViewModel
{
    public class ViewModelCatAPI
    {
        CatAPI catAPI = new CatAPI();

        
        /*public async List<Mascota> MascotaList()
        {
            bool flag = await catAPI.ListarRazasAsync();
            if (flag)
            {
                List<Mascota> list = new List<Mascota>();
                int tipos = miJson1.RootElement.GetProperty("meals").GetArrayLength();
                for (int i = 0; i < tipos; i++)
                {
                    listaCategorias.Add(miJson1.RootElement.GetProperty("meals")[i].GetProperty("strCategory").ToString());
                }
                return listaCategorias;
            }
        }*/
    }
}
