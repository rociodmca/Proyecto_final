using MongoDB.Bson;
using Proyecto_final.ViewModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using Proyecto_final.Resources.Temas;
using Proyecto_final.Resources.Idiomas;
using Microsoft.Maui.HotReload;

namespace Proyecto_final.View;

public partial class Calendario : ContentPage
{
    ObjectId id;
    ViewModelMostrarCita viewModelMostrarCita;
    ViewModelBBDD viewModelBBDD;
    AppShell apps;

    /// <summary>
    /// Constructor que inicializa las variables
    /// </summary>
    /// <param name="id">identificador del usuario</param>
    /// <param name="apps">instancia de la AppShell</param>
    public Calendario(ObjectId id, AppShell apps)
    {
        this.id = id;
        this.apps = apps;
        viewModelMostrarCita = new ViewModelMostrarCita();
        viewModelBBDD = new ViewModelBBDD();

        InitializeComponent();
        
    }

    /// <summary>
    /// Método asociado al collectionview para borrar una cita
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void lista2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (lista2.SelectedItem != null)
        {
            string idCita = "", fecha = "";
            List<ViewModelMostrarCita> citas = viewModelMostrarCita.ObtenerCitas(id);
            ViewModelMostrarCita dateSelected = lista2.SelectedItem as ViewModelMostrarCita;
            foreach (ViewModelMostrarCita item in citas)
            {
                if (item.Id == dateSelected.Id)
                {
                    idCita = item.Id;
                    fecha = item.Fecha.ToShortDateString();
                }
            }
            var msg = await DisplayAlert("Borrar cita", "¿Está seguro que desea borrar la cita para el dia " + fecha + "?", "Sí", "No");
            if (msg)
            {
                if (viewModelBBDD.BorrarCita(idCita))
                {
                    await DisplayAlert("Información", "Cita correctamente borrada de la base de datos", "Ok");
                    lista2.ItemsSource = null;
                    lista2.ItemsSource = viewModelMostrarCita.ObtenerCitas(id);
                }
                else
                {
                    await DisplayAlert("Información", "No se ha podido borrar", "Ok");
                }
            }
        }
        else
        {
            await DisplayAlert("Información", "No se ha seleccionado ninguna cita", "OK");
        }
    }

    /// <summary>
    /// Método sobreescrito de OnBackButtonPressed para inutilizar el botón de onback en Android
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            var recurso1 = (string)Application.Current.Resources["salir1"];
            var recurso2 = (string)Application.Current.Resources["salir2"];
            var recurso3 = (string)Application.Current.Resources["salir3"];
            var recurso4 = (string)Application.Current.Resources["salir4"];
            var leave = await DisplayAlert(recurso1, recurso2, recurso3, recurso4);

            if (leave)
            {
                await Shell.Current.GoToAsync("//info");
            }
        });
        return true;
    }

    /// <summary>
    /// Método asociado a la aparición de la contentpage en la pila
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        lista2.ItemsSource = viewModelMostrarCita.ObtenerCitas(id);
    }
}