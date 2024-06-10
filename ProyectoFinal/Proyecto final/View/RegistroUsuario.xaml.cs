using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class RegistroUsuario : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    List<int> roles = new List<int>();

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
            //rol.IsEnabled = false;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            desktop.IsVisible = true;
            phone.IsVisible = false;
            rol.ItemsSource = roles;
            rol.SelectedIndex = 2;
            //rol.IsEnabled = false;
        }
    }

    public RegistroUsuario(int optionalRol)
	{
        roles = [1, 2, 3];
        InitializeComponent();
        rol.ItemsSource = roles;
        rol.SelectedIndex = optionalRol;
        rol.IsEnabled = false;
        viewModelBBDD = new ViewModelBBDD();
	}

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

    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        nombre.Text = "";
        apellidos.Text = "";
        email.Text = "";
        password.Text = "";
        rol.SelectedIndex = -1;
    }

    private void BtnCancelar2_Clicked(object sender, EventArgs e)
    {
        nombre2.Text = "";
        apellidos2.Text = "";
        email2.Text = "";
        password2.Text = "";
        rol2.SelectedIndex = -1;
    }

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
}