using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PagVeterinario : ContentPage
{
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;
    ViewModelBBDD viewModelBBDD;
    ObjectId id;

	public PagVeterinario(ObjectId id)
	{
		viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();
        viewModelBBDD = new ViewModelBBDD();
        this.id = id;

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotasSinId();
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitasVeterinario(id);

        InitializeComponent();
        pagVet.Title = "Bienvenid@  " + viewModelBBDD.ObtenerNombre(id);
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        //viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotasSinId();
        
        //mascotas.IsVisible = true;
        //citas.IsVisible = false;
        mascotas.ItemsSource = null;
        mascotas.ItemsSource = viewModelMascota.Mascotas;
        mascotas.IsVisible = true;
        citas.IsVisible = false;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        //viewModelCita.Citas = viewModelBBDD.ObtenerListaCitasVeterinario(id);
        
        //mascotas.IsVisible = false;
        //citas.IsVisible = true;
        citas.ItemsSource = null;
        citas.ItemsSource = viewModelCita.Citas;
        mascotas.IsVisible = false;
        citas.IsVisible = true;
    }

    private void Mascotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas();
    }

    private void Citas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}