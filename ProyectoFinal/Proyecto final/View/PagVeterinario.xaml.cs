using MongoDB.Bson;
using Plugin.Maui.Calendar.Models;
using Proyecto_final.Model;
using Proyecto_final.Resources.Idiomas;
using Proyecto_final.Resources.Temas;
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

    /// <summary>
    /// Constructor para inicilizar los controles y consultar en la base de datos
    /// </summary>
    /// <param name="id">identificador del usuario</param>
    /// <param name="apps">instancia del AppShell</param>
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
        
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            RefreshPage();
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
            Appearing += MainPage_Appearing;
        }

    }

    /// <summary>
    /// Método asociado a la aparición de la contentpage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainPage_Appearing(object sender, EventArgs e)
    {
        RefreshPage();
    }

    /// <summary>
    /// Método para rellenar los datos del calendario
    /// </summary>
    private void RefreshPage()
    {
        Ejecutar1();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            calendario1.Events.Clear();
            calendario1.ClearLogicalChildren();
            calendario1.EventTemplate = eventTemplate1;
            calendario1.Events = viewModelEvento.eventos;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            calendario2.Events.Clear();
            calendario2.ClearLogicalChildren();
            calendario2.EventTemplate = eventTemplate2;
            calendario2.Events = viewModelEvento.eventos;
        }
    }

    /// <summary>
    /// Método para cargar los eventos
    /// </summary>
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

    /// <summary>
    /// Método asociado al botón acceso a mascotas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        btn_borrar2.IsVisible = true;
        mascotas2.ItemsSource = null;
        mascotas2.ItemsSource = viewModelMascota.Mascotas;
        mascotas2.IsVisible = true;
        citas2.IsVisible = false;
    }

    /// <summary>
    /// Método asociado al botón de acceso a citas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        btn_borrar2.IsVisible = false;
        mascotas2.IsVisible = false;
        citas2.IsVisible = true;
    }

    /// <summary>
    /// Método asociado a la selección de un elemento del listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Mascotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas();
    }

    /// <summary>
    /// Método asociado al selector de citas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Citas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    /// <summary>
    /// Método asociado al botón de ajustes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_2(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Ajustes(id, apps));
    }

    /// <summary>
    /// Método asociado al botón para borrar una mascota
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        if (mascotas2.SelectedItem != null)
        {
            string idMascota = "", nombre = "";
            foreach (var item in viewModelMascota.Mascotas)
            {
                if (item.Equals(mascotas2.SelectedItem))
                {
                    idMascota = item.Id;
                    nombre = item.Nombre;
                }
            }
            var msg = await DisplayAlert("Borrar mascota", "¿Está seguro que desea borrar la mascota " + nombre + "?", "Sí", "No");
            if (msg)
            {
                if (viewModelBBDD.BorrarMascota(idMascota))
                {
                    await DisplayAlert("Información", "Mascota correctamente borrada de la base de datos", "Ok");
                    mascotas2.ItemsSource = null;
                    mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotasSinId();
                }
                else
                {
                    await DisplayAlert("Información", "No se ha podido borrar", "Ok");
                }
            }
        }
        else
        {
            await DisplayAlert("Información", "No se ha seleccionado ningún usuario", "OK");
        }
    }

    /// <summary>
    /// Método asociado al botón de acceso a mascotas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_4(object sender, EventArgs e)
    {
        //viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotasSinId();

        //mascotas.IsVisible = true;
        //citas.IsVisible = false;
        btn_borrar1.IsVisible = true;
        mascotas1.ItemsSource = null;
        mascotas1.ItemsSource = viewModelMascota.Mascotas;
        mascotas1.IsVisible = true;
        citas1.IsVisible = false;
    }

    /// <summary>
    /// Método asociado al botón de ajustes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_5(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Ajustes(id, apps));
    }

    /// <summary>
    /// Método asociado al botón para borrar una mascota
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void btn_borrar1_Clicked(object sender, EventArgs e)
    {
        if (mascotas1.SelectedItem != null)
        {
            string idMascota = "", nombre = "";
            foreach (var item in viewModelMascota.Mascotas)
            {
                if (item.Equals(mascotas1.SelectedItem))
                {
                    idMascota = item.Id;
                    nombre = item.Nombre;
                }
            }
            var msg = await DisplayAlert("Borrar mascota", "¿Está seguro que desea borrar la mascota " + nombre + "?", "Sí", "No");
            if (msg)
            {
                if (viewModelBBDD.BorrarMascota(idMascota))
                {
                    await DisplayAlert("Información", "Mascota correctamente borrada de la base de datos", "Ok");
                    mascotas1.ItemsSource = null;
                    mascotas1.ItemsSource = viewModelBBDD.ObtenerListaMascotasSinId();
                }
                else
                {
                    await DisplayAlert("Información", "No se ha podido borrar", "Ok");
                }
            }
        }
        else
        {
            await DisplayAlert("Información", "No se ha seleccionado ningún usuario", "OK");
        }
    }

    /// <summary>
    /// Método asociado al botón acceso a citas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_6(object sender, EventArgs e)
    {
        //viewModelCita.Citas = viewModelBBDD.ObtenerListaCitasVeterinario(id);

        //mascotas.IsVisible = false;
        //citas.IsVisible = true;
        //citas.ItemsSource = null;
        //citas.ItemsSource = viewModelCita.Citas;
        btn_borrar1.IsVisible = false;
        mascotas1.IsVisible = false;
        citas1.IsVisible = true;
    }

    /// <summary>
    /// Método asociado al botón de cerrar sesión
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_Salir(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        miListaDiccionarios.Add(new Espanol());
        Shell.Current.GoToAsync("//info");
    }

    /// <summary>
    /// Método para asignarle la función al botón onback
    /// </summary>
    /// <returns></returns>
    protected override bool OnBackButtonPressed()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
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
                    await Navigation.PopAsync();
                    apps.FlyoutBehavior = FlyoutBehavior.Flyout;
                }
            });
        }
        return true;
    }
}