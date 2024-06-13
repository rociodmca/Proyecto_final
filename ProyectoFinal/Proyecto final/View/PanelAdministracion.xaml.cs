using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PanelAdministracion : ContentPage
{
    AppShell apps;
    ViewModelBBDD viewModelBBDD;
    ViewModelUsuario viewModelUsuario;
    string id = "";

    /// <summary>
    /// Constructor para inicializar los controles y hacer llamadas a la base de datos
    /// </summary>
    /// <param name="apps">instancia de la AppShell</param>
	public PanelAdministracion(AppShell apps)
	{
		this.apps = apps;
        viewModelBBDD = new ViewModelBBDD();
        viewModelUsuario = new ViewModelUsuario();

        InitializeComponent();
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            desktop.IsVisible = true;
            usuarios2.ItemsSource = viewModelUsuario.Usuarios;
        }
    }

    /// <summary>
    /// Método asociado a registrar un veterinario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario(1));
    }

    /// <summary>
    /// Método asociado al listview usuarios2
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Método asociado al botón para borrar un usuario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_2(object sender, EventArgs e)
    {
        bool respuesta = viewModelBBDD.BorrarUsuario(id);
        DisplayAlert("INFO", respuesta.ToString(), "OK");
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaUsuarios();
        usuarios2.ItemsSource = viewModelUsuario.Usuarios;
    }
    
    /// <summary>
    /// Método asociado al botón para registrar un veterinario
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario(1));
    }

    /// <summary>
    /// Método para asignarle la función al botón onback
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
                //await Navigation.PushAsync(new MainPage());
                await Navigation.PopAsync();
                apps.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        });

        return true;
    }
}