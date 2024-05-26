using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class Manual : ContentPage
{
	ViewModelBBDD viewModelBBDD;

	public Manual()
	{
		viewModelBBDD = new ViewModelBBDD();

		InitializeComponent();
        prueba.Text = "Hola";
        foreach (KeyValuePair<string, Object> item in viewModelBBDD.ObtenerCitasP("6651c6307b6821391798f66f"))
        {
            if (item.Key == "Mascota")
            {
                //foreach (BsonElement item1 in item.Value)
                //{
                    prueba.Text += item.Value + "\n";
                //}
            }
            
        }
        prueba.Text += viewModelBBDD.ObtenerCitasP("6651c6307b6821391798f66f").Count.ToString();
    }
}