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
        //CargarPag();
    }

    public void CargarPag()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                //mascotas2.ItemsSource = null;
                mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
                mascotas2.IsVisible = true;
            });
        }
        Thread.Sleep(10);
        Thread thread1 = new Thread(new ThreadStart(Ejecutar3));
        thread1.Start();
    }

    public async Task VisualizarTabla()
    {
        await Task.Delay(20);
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                cargando.IsRunning = false;
                cargando.IsVisible = false;
                phone.IsVisible = true;
                desktop.IsVisible = false;
                mascotas.ItemsSource = viewModelMascota.Mascotas;
                mascotas.IsVisible = true;
            });
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                cargando.IsRunning = false;
                cargando.IsVisible = false;
                phone.IsVisible = false;
                desktop.IsVisible = true;
                calendario.Events = viewModelEvento.RellenarEventos(id);
                calendario.IsVisible = true;
            });
        }
    }

    public async void Ejecutar3()
    {
        Thread.Sleep(10);
        await VisualizarTabla();
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        CargarPag();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            cargando.IsRunning = true;
            cargando.IsVisible = true;
            phone.IsVisible = false;
            desktop.IsVisible = false;
        });
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        miListaDiccionarios.Add(new Espanol());
        Shell.Current.GoToAsync("//info");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CuestionarioCita(id));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        var pop = new PopupPass(id);
        this.ShowPopup(pop);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool msg = await DisplayAlert("Guardar mascota", "¿Quieres guardar una mascota?", "Aceptar", "Cancelar");
        if (msg)
        {
            await Navigation.PushAsync(new RegistroMascota(id));
            //await Navigation.PushAsync(new Ajustes(id));
        }
    }

    private void mascotas2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        DisplayAlert("Info", mascotas2.SelectedItem.ToString(), "Ok");
    }

    private void mascotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        string text = mascotas.SelectedItem.ToString();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        Toast.Make(text, duration, fontSize).Show();
    }

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

    private void lista2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //DisplayAlert("Info", lista2.SelectedItem.ToString(), "Ok");

    }

    private async void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        await Task.Delay(10);
        await Navigation.PushAsync(new Ajustes(id, apps));
    }

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
                //await Navigation.PushAsync(new MainPage());
                await Navigation.PopAsync();
                apps.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        });

        return true;
    }

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