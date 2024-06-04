using MongoDB.Bson;
using Plugin.Maui.Calendar.Models;
using Proyecto_final.Model;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PagVeterinario : ContentPage
{
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;
    ViewModelBBDD viewModelBBDD;
    ViewModelEvento viewModelEvento;
    ObjectId id;
    AppShell apps;

	public PagVeterinario(ObjectId id, AppShell apps)
	{
		viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();
        viewModelBBDD = new ViewModelBBDD();
        viewModelEvento = new ViewModelEvento();
        this.id = id;
        this.apps = apps;

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotasSinId();
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitasVeterinario(id);       

        InitializeComponent();
        Appearing += MainPage_Appearing;
        pagVet.Title = "Bienvenid@  " + viewModelBBDD.ObtenerNombre(id);
        
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        RefreshPage();
    }

    private void RefreshPage()
    {
        Ejecutar1();
        calendario.Events = viewModelEvento.eventos;
    }

    private async void Ejecutar1()
    {
        viewModelEvento.eventos = new EventCollection();
        foreach (var cita in viewModelCita.Citas)
        {
            string nombre = viewModelBBDD.ObtenerNombre(cita.Id_cliente);
            string mascota = viewModelBBDD.ObtenerNombreMascota(cita.Id_mascota);
            if (!viewModelEvento.eventos.ContainsKey(cita.Fecha))
            {
                viewModelEvento.eventos.Add(cita.Fecha, new List<EventModel>
                {
                    new EventModel { Name = $"Cliente: {nombre}", Message = $"Mascota: {mascota}" }
                });
            }
            else if (viewModelEvento.eventos.ContainsKey(cita.Fecha))
            {
                ICollection<EventModel> eventos = (ICollection<EventModel>)viewModelEvento.eventos[cita.Fecha];
                eventos.Add(new EventModel { Name = $"Cliente: {nombre}", Message = $"Mascota: {mascota}" });
            }
        }
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
        //citas.ItemsSource = null;
        //citas.ItemsSource = viewModelCita.Citas;
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