using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class RegistroUsuario : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    List<int> roles = new List<int>();

    /// <summary>
    /// Constructor sin parámetros que inicializa los controles y las variables
    /// </summary>
    public RegistroUsuario()
    {
        roles = [1, 2, 3];
        viewModelBBDD = new ViewModelBBDD();
        InitializeComponent();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            desktop.IsVisible = false;
            phone.IsVisible = true;
            rol2.ItemsSource = roles;
            rol2.SelectedIndex = 2;
            rol.IsEnabled = false;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            desktop.IsVisible = true;
            phone.IsVisible = false;
            rol.ItemsSource = roles;
            rol.SelectedIndex = 2;
            rol.IsEnabled = false;
        }
    }

    /// <summary>
    /// Constructor con parámetros que inicializa los controles y las variables
    /// </summary>
    /// <param name="optionalRol">rol de usuario</param>
    public RegistroUsuario(int optionalRol)
	{
        roles = [1, 2, 3];
        InitializeComponent();
        rol.ItemsSource = roles;
        rol.SelectedIndex = optionalRol;
        rol.IsEnabled = false;
        viewModelBBDD = new ViewModelBBDD();
	}

    /// <summary>
    /// Método asociado al botón registrar para guardar el usuario en la BBDD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnRegistrarse_Clicked(object sender, EventArgs e)
    {
        if ((nombre.Text.Length != 0) && (apellidos.Text.Length != 0) && (email.Text.Length != 0) && (password.Text.Length != 0) && (rol.SelectedIndex != -1))
        {
            viewModelBBDD.InsertarUsuario(nombre.Text, apellidos.Text, email.Text, password.Text, (rol.SelectedIndex + 1));
            nombre.Text = "";
            apellidos.Text = "";
            email.Text = "";
            password.Text = "";
            rol.SelectedIndex = -1;
        }
    }

    /// <summary>
    /// Método asociado al botón cancelar que vacía los campos y vuelve a la 
    /// contentpage anterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        nombre.Text = "";
        apellidos.Text = "";
        email.Text = "";
        password.Text = "";
        rol.SelectedIndex = -1;
    }

    /// <summary>
    /// Método asociado al botón cancelar que vacía los campos y vuelve a la 
    /// contentpage anterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnCancelar2_Clicked(object sender, EventArgs e)
    {
        nombre2.Text = "";
        apellidos2.Text = "";
        email2.Text = "";
        password2.Text = "";
        rol2.SelectedIndex = -1;
    }

    /// <summary>
    /// Método asociado al botón registrarse para guardar el usuario en la BBDD
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnRegistrarse2_Clicked(object sender, EventArgs e)
    {
        if ((nombre2.Text.Length != 0) && (apellidos2.Text.Length != 0) && (email2.Text.Length != 0) && (password2.Text.Length != 0) && (rol2.SelectedIndex != -1))
        {
            viewModelBBDD.InsertarUsuario(nombre2.Text, apellidos2.Text, email2.Text, password2.Text, (rol2.SelectedIndex + 1));
            nombre2.Text = "";
            apellidos2.Text = "";
            email2.Text = "";
            password2.Text = "";
            rol2.SelectedIndex = -1;
        }
    }

    /// <summary>
    /// Método sobreescrito de OnBackButtonPressed para inutilizar el botón de onback en Android
    /// </summary>
    /// <returns></returns> 
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}