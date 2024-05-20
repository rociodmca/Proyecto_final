using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PanelAdministracion : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    ViewModelUsuario viewModelUsuario;
    string id;

	public PanelAdministracion()
	{
		viewModelBBDD = new ViewModelBBDD();
        viewModelUsuario = new ViewModelUsuario();

        InitializeComponent();
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios.ItemsSource = viewModelUsuario.Usuarios;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        bool respuesta = viewModelBBDD.BorrarUsuario(id);
        DisplayAlert("INFO", respuesta.ToString(), "OK");
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios.ItemsSource = viewModelUsuario.Usuarios;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario(1));
    }

    private void usuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        foreach (var item in viewModelUsuario.Usuarios)
        {
            if (item.Equals(usuarios.SelectedItem))
            {
                id = item.Id;
            }
        }
    }
}