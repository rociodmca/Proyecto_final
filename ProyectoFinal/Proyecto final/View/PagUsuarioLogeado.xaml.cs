using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Proyecto_final.ViewModel;
using MongoDB.Bson;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;

namespace Proyecto_final.View;

public partial class PagUsuarioLogeado : ContentPage
{
    ObjectId id;
    ViewModelBBDD viewModelBBDD;
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;

	public PagUsuarioLogeado(ObjectId id)
	{
		viewModelBBDD = new ViewModelBBDD();
        viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas(id);
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitas(id);

        

        InitializeComponent();
        this.Appearing += MainPage_Appearing;
        this.id = id;
        mascotas.IsVisible = false;
        mascotas2.IsVisible = false;
        lista.IsVisible = false;
        lista2.IsVisible = false;

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
                    lista.ItemsSource = viewModelCita.Citas;
                    mascotas.IsVisible = true;
                    lista.IsVisible = true;
                    
                    flag = false;
                }
                if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
                {
                    phone.IsVisible = false;
                    desktop.IsVisible = true;
                    mascotas2.ItemsSource = viewModelMascota.Mascotas;
                    lista2.ItemsSource = viewModelCita.Citas;
                    mascotas2.IsVisible = true;
                    lista2.IsVisible = true;
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
            lista.ItemsSource = viewModelBBDD.ObtenerListaCitas(id);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
            lista2.ItemsSource = viewModelBBDD.ObtenerListaCitas(id);
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
        VisibilizarTabla();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
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
        
        DisplayAlert("Info", lista2.SelectedItem.ToString(), "Ok");

    }
}