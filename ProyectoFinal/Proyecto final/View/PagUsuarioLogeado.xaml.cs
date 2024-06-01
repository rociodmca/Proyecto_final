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


namespace Proyecto_final.View;

public partial class PagUsuarioLogeado : ContentPage
{
    ObjectId id;
    ViewModelBBDD viewModelBBDD;
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;
    DiceBearAPI diceBearAPI;
    string nombre;
    List<DateTime> citas;
    ViewModelEvento viewModelEvento;

    public PagUsuarioLogeado(ObjectId id)
    {
        viewModelBBDD = new ViewModelBBDD();
        viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();
        diceBearAPI = new DiceBearAPI();
        citas = new List<DateTime>();
        viewModelEvento = new ViewModelEvento();

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas(id);
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitas(id);
        /*foreach (var cita in viewModelCita.Citas)
        {
            citas.Add(cita.Fecha);
        }*/

        nombre = viewModelBBDD.ObtenerNombre(id);

        InitializeComponent();
        //this.Appearing += MainPage_Appearing;
        avatar1.Source = diceBearAPI.OnGenerateAvatar(nombre);
        avatar2.Source = diceBearAPI.OnGenerateAvatar(nombre);
        this.id = id;
        mascotas.IsVisible = false;
        mascotas2.IsVisible = false;
        //lista.IsVisible = false;
        //lista2.IsVisible = false;
        calendario.Culture = new System.Globalization.CultureInfo("es-ES");
        calendario2.Culture = new System.Globalization.CultureInfo("es-ES");
        /*phone.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});
        phone.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.2, GridUnitType.Star) });
        phone.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) });
        phone.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });*/

        CargarPag();

    }

    public async void CargarPag()
    {
        Thread thread1 = new Thread(new ThreadStart(Ejecutar1));
        thread1.IsBackground = true;
        thread1.Start();
    }

    public void Ejecutar1()
    {
        Thread thread3 = new Thread(new ThreadStart(Ejecutar3));
        thread3.Start();
    }

    public void Ejecutar2()
    {
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitas(id);
    }

    public void Ejecutar3()
    {
        bool flag = true;
        while (flag)
        {
            if (viewModelMascota.Mascotas.Count > 0 && viewModelCita.Citas.Count > 0)
            {
                if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                {
                    phone.IsVisible = true;
                    desktop.IsVisible = false;
                    mascotas.ItemsSource = viewModelMascota.Mascotas;
                    //lista.ItemsSource = viewModelCita.Citas;
                    mascotas.IsVisible = true;
                    //lista.IsVisible = true;
                    viewModelEvento.eventos = new EventCollection();
                    foreach (var cita in viewModelCita.Citas)
                    {
                        string nombre = viewModelBBDD.ObtenerNombre(cita.Id_veterinario);
                        string mascota = viewModelBBDD.ObtenerNombreMascota(cita.Id_mascota);
                        if (!viewModelEvento.eventos.ContainsKey(cita.Fecha))
                        {
                            viewModelEvento.eventos.Add(cita.Fecha, new List<EventModel>
                            {
                                new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" }
                            });
                        }
                        else if (viewModelEvento.eventos.ContainsKey(cita.Fecha))
                        {
                            ICollection<EventModel> eventos = (ICollection<EventModel>)viewModelEvento.eventos[cita.Fecha];
                            eventos.Add(new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" });
                        }
                    }
                    calendario2.Events = viewModelEvento.eventos;

                    flag = false;
                }
                if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
                {
                    phone.IsVisible = false;
                    desktop.IsVisible = true;
                    mascotas2.ItemsSource = viewModelMascota.Mascotas;
                    //lista2.ItemsSource = viewModelCita.Citas;
                    mascotas2.IsVisible = true;
                    //lista2.IsVisible = true;
                    //calendario.SelectedDates = citas;
                    viewModelEvento.eventos = new EventCollection();
                    foreach (var cita in viewModelCita.Citas)
                    {
                        string nombre = viewModelBBDD.ObtenerNombre(cita.Id_veterinario);
                        string mascota = viewModelBBDD.ObtenerNombreMascota(cita.Id_mascota);
                        if (!viewModelEvento.eventos.ContainsKey(cita.Fecha))
                        {
                            viewModelEvento.eventos.Add(cita.Fecha, new List<EventModel>
                            {
                                new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" }
                            });
                        }else if (viewModelEvento.eventos.ContainsKey(cita.Fecha))
                        {
                            ICollection<EventModel> eventos = (ICollection<EventModel>)viewModelEvento.eventos[cita.Fecha];
                            eventos.Add( new EventModel { Name = $"Veterinario: {nombre}", Message = $"Mascota: {mascota}" } );
                        }
                    }
                    calendario.Events = viewModelEvento.eventos;
                    flag = false;
                }
            }
        }
    }

    public void VisibilizarTabla()
    {
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
            //lista.ItemsSource = viewModelBBDD.ObtenerListaCitas(id);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
            //lista2.ItemsSource = viewModelBBDD.ObtenerListaCitas(id);
            phone.IsVisible = false;
            desktop.IsVisible = true;
        }
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        RefreshPage();
    }

    private void RefreshPage()
    {
        Ejecutar3();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        Navigation.PopAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CuestionarioCita(id));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        /*var pop = new PopupPass(id);
        this.ShowPopup(pop);*/
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool msg = await DisplayAlert("Guardar mascota", "¿Quieres guardar una mascota?", "Aceptar", "Cancelar");
        if (msg)
        {
            await Navigation.PushAsync(new RegistroMascota(id));
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

    private void MenuFlyoutItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Ajustes(id));
    }

    private void mascotas_Focused(object sender, FocusEventArgs e)
    {
        DisplayAlert("info", "funciona", "Ok");
        phone.RowDefinitions[0].Height = GridLength.Auto;
        phone.RowDefinitions[1].Height = new GridLength(0.8, GridUnitType.Star);
        phone.RowDefinitions[2].Height = new GridLength(0.2, GridUnitType.Star);
        phone.RowDefinitions[3].Height = GridLength.Auto;
    }

    private void calen_Focused(object sender, FocusEventArgs e)
    {
        phone.RowDefinitions[0].Height = GridLength.Auto;
        phone.RowDefinitions[1].Height = new GridLength(0.2, GridUnitType.Star);
        phone.RowDefinitions[2].Height = new GridLength(0.8, GridUnitType.Star);
        phone.RowDefinitions[3].Height = GridLength.Auto;
    }
}