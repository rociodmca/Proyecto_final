using MongoDB.Bson;
using Proyecto_final.Model;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class Manual : ContentPage
{
	ViewModelBBDD viewModelBBDD;
    DiceBearAPI diceBearAPI;

	public Manual()
	{
		viewModelBBDD = new ViewModelBBDD();
        diceBearAPI = new DiceBearAPI();

		InitializeComponent();
        pruebaimg.Source = diceBearAPI.OnGenerateAvatar("Pepe");
    }
}