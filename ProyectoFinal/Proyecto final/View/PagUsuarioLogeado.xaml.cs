using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Proyecto_final.ViewModel;
using MongoDB.Bson;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;
using Proyecto_final.Model;
using Plugin.Maui.Calendar.Enums;
using Plugin.Maui.Calendar.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Proyecto_final.Resources.Temas;
using Proyecto_final.Resources.Idiomas;


namespace Proyecto_final.View;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PagUsuarioLogeado : ContentPage
{
    ObjectId id;
    AppShell apps;
    ViewModelBBDD viewModelBBDD;
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;
    DiceBearAPI diceBearAPI;
    string nombre;
    List<DateTime> citas;
    ViewModelEvento viewModelEvento;
    bool flag = true;

    /// <summary>
    /// Constructor para inicializar los controles y hacer consultas a la base de datos
    /// </summary>
    /// <param name="id">identificador del usuario</param>
    /// <param name="apps">instancia de la AppShell</param>
    public PagUsuarioLogeado(ObjectId id, AppShell apps)
    {
        this.id = id;
        this.apps = apps;
        viewModelBBDD = new ViewModelBBDD();
        viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();
        diceBearAPI = new DiceBearAPI();
        citas = new List<DateTime>();
        viewModelEvento = new ViewModelEvento();

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas(id);
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitas(id);
        nombre = viewModelBBDD.ObtenerNombre(id);

        InitializeComponent();

        mascotas.IsVisible = false;
        mascotas2.IsVisible = false;

        string idioma = viewModelBBDD.ObtenerAjuste(id).Idioma;
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            if (idioma == "Espanol")
            {
                calendario.Culture = new System.Globalization.CultureInfo("es-ES");
            }
            if (idioma == "Ingles")
            {
                calendario.Culture = new System.Globalization.CultureInfo("en-GB");
            }
        }

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            avatar1.Source = diceBearAPI.OnGenerateAvatar(nombre);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            avatar2.Source = diceBearAPI.OnGenerateAvatar(nombre);
        }

        Appearing += MainPage_Appearing;
    }

    /// <summary>
    /// Método para cargar los controles desde la base de datos
    /// </summary>
    public void CargarPag()
    {
        /*if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
                mascotas.IsVisible = true;
            });
        }*/
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
                mascotas2.IsVisible = true;
                double size = double.Parse(viewModelBBDD.ObtenerAjuste(id).Tam_letra);
                btnCita.FontSize = size;
                btnDesconectar.FontSize = size;
                btnPass.FontSize = size;
            });
        }
        Thread.Sleep(1);
        Thread thread1 = new Thread(new ThreadStart(Ejecutar3));
        thread1.IsBackground = true;
        thread1.Start();
    }

    /// <summary>
    /// Método asíncrono para visualizar los controles que ya se han cargado
    /// </summary>
    /// <returns></returns>
    public async Task VisualizarTabla()
    {
        await Task.Delay(2);
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                cargando.IsRunning = false;
                cargando.IsVisible = false;
                phone.IsVisible = true;
                desktop.IsVisible = false;
                mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
                mascotas.IsVisible = true;
            });
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            calendario.Events.Clear();
            calendario.ClearLogicalChildren();
            calendario.EventTemplate = eventTemplate;
            
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                cargando.IsRunning = false;
                cargando.IsVisible = false;
                phone.IsVisible = false;
                desktop.IsVisible = true;
                calendario.Events = viewModelEvento.RellenarEventos(id);
            });
        }
    }

    /// <summary>
    /// Método asíncrono que llama al método anterior
    /// </summary>
    public async void Ejecutar3()
    {
        Thread.Sleep(1);
        await VisualizarTabla();
    }

    /// <summary>
    /// Método asociado a la aparición de la ContentPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainPage_Appearing(object sender, EventArgs e)
    {
        calendario.IsVisible = false;
        calendario.SelectedDate = DateTime.Now;
        CargarPag();
        Ejecutar3();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            cargando.IsRunning = true;
            cargando.IsVisible = true;
            phone.IsVisible = false;
            desktop.IsVisible = false;
            calendario.IsVisible = true;
        });
    }

    /// <summary>
    /// Método asociado al botón de cerrar sesión
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        miListaDiccionarios.Add(new Espanol());
        Shell.Current.GoToAsync("//info");
    }

    /// <summary>
    /// Método asociado al botón de guardar cita
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CuestionarioCita(id));
    }

    /// <summary>
    /// Método asociado al botón para cambiar la contraseña
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_2(object sender, EventArgs e)
    {
        var pop = new PopupPass(id);
        this.ShowPopup(pop);
    }

    /// <summary>
    /// Método asociado al click de la imagen del avatar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool msg = await DisplayAlert("Guardar mascota", "¿Quieres guardar una mascota?", "Aceptar", "Cancelar");
        if (msg)
        {
            await Navigation.PushAsync(new RegistroMascota(id));
        }
    }

    /// <summary>
    /// Método asociado al selector del listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void mascotas2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        DisplayAlert("Info", mascotas2.SelectedItem.ToString(), "Ok");
    }

    /// <summary>
    /// Método asociado al selector del listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void mascotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        string text = mascotas.SelectedItem.ToString();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        Toast.Make(text, duration, fontSize).Show();
    }

    /// <summary>
    /// Método asociado al borrado de un elemento en el listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void mascotas_ChildRemoved(object sender, ElementEventArgs e)
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            mascotas.ItemsSource = null;
            mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            mascotas2.ItemsSource = null;
            mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
        }
    }

    /// <summary>
    /// Método asociado a la selección del listview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lista2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //DisplayAlert("Info", lista2.SelectedItem.ToString(), "Ok");

    }

    /// <summary>
    /// Método asociado al menú contextual del avatar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        await Task.Delay(10);
        await Navigation.PushAsync(new Ajustes(id, apps));
    }

    /// <summary>
    /// Método sobreescrito de OnBackButtonPressed para asignar funciones al botón onback
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
                if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                {
                    await Shell.Current.GoToAsync("//login");
                }
                if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
                {
                    await Navigation.PopAsync();
                    apps.FlyoutBehavior = FlyoutBehavior.Flyout;
                }              
            }
        });

        return true;
    }

    /// <summary>
    /// Método asociado al botón de desconexión
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_3(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        miListaDiccionarios.Add(new Espanol());
        apps.FlyoutBehavior = FlyoutBehavior.Flyout;
        Navigation.PopAsync();
    }

}