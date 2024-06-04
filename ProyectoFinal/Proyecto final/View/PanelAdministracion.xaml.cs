using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PanelAdministracion : ContentPage
{
    AppShell apps;
    ViewModelBBDD viewModelBBDD;
    ViewModelUsuario viewModelUsuario;
    string id;

	public PanelAdministracion(AppShell apps)
	{
		this.apps = apps;
        viewModelBBDD = new ViewModelBBDD();
        viewModelUsuario = new ViewModelUsuario();

        InitializeComponent();
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            usuarios1.ItemsSource = viewModelUsuario.Usuarios;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
            usuarios2.ItemsSource = viewModelUsuario.Usuarios;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        bool respuesta = viewModelBBDD.BorrarUsuario(id);
        DisplayAlert("INFO", respuesta.ToString(), "OK");
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios1.ItemsSource = viewModelUsuario.Usuarios;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario(1));
    }

    private void usuarios_Refreshing(object sender, EventArgs e)
    {
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios1.ItemsSource = null;
        usuarios1.ItemsSource = viewModelUsuario.Usuarios;
        usuarios1.IsRefreshing = false;
    }

    private void usuarios2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        foreach (var item in viewModelUsuario.Usuarios)
        {
            if (item.Equals(usuarios2.SelectedItem))
            {
                id = item.Id;
            }
        }
    }

    private void usuarios1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        foreach (var item in viewModelUsuario.Usuarios)
        {
            if (item.Equals(usuarios1.SelectedItem))
            {
                id = item.Id;
            }
        }
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        bool respuesta = viewModelBBDD.BorrarUsuario(id);
        DisplayAlert("INFO", respuesta.ToString(), "OK");
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios2.ItemsSource = viewModelUsuario.Usuarios;
    }

    private void Button_Clicked_3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario(1));
    }
}