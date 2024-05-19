using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PagUsuarioLogeado : ContentPage
{
    string id;
    ViewModelBBDD viewModelBBDD;

	public PagUsuarioLogeado(string id)
	{
		viewModelBBDD = new ViewModelBBDD();
        InitializeComponent();
        this.id = id;

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            mascotas.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
            mascotas2.ItemsSource = viewModelBBDD.ObtenerListaMascotas(id);
        }
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

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool msg = await DisplayAlert("Guardar mascota", "¿Quieres guardar una mascota?", "Aceptar", "Cancelar");
        if (msg)
        {
            Navigation.PushAsync(new RegistroMascota(id));
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
}