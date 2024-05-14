using Proyecto_final.Controlador;

namespace Proyecto_final.Vista;

public partial class RegistroUsuario : ContentPage
{
    ControladorBBDD contBBDD;
    List<int> roles = new List<int>();


    public RegistroUsuario()
	{
        roles = [1, 2, 3];
        InitializeComponent();
        rol.ItemsSource = roles;
        contBBDD = new ControladorBBDD();
	}

    private void BtnRegistrarse_Clicked(object sender, EventArgs e)
    {
        if ((nombre.Text.Length != 0) && (apellidos.Text.Length != 0) && (email.Text.Length != 0) && (password.Text.Length != 0))
        {
            contBBDD.InsertarUsuario(nombre.Text, apellidos.Text, email.Text, password.Text, (rol.SelectedIndex + 1));
            nombre.Text = "";
            apellidos.Text = "";
            email.Text = "";
            password.Text = "";
        }
    }

    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        
    }
}