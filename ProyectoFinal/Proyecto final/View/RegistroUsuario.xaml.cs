using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class RegistroUsuario : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    List<int> roles = new List<int>();

    public RegistroUsuario()
    {
        roles = [1, 2, 3];
        InitializeComponent();
        rol.ItemsSource = roles;
        rol.SelectedIndex = 2;
        //rol.IsEnabled = false;
        viewModelBBDD = new ViewModelBBDD();
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
}