using Proyecto_final.Controlador;
using System.Threading.Tasks;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Proyecto_final.Vista;

public partial class Login : ContentPage
{
    ControladorBBDD controladorBBDD;


    public Login()
	{
		InitializeComponent();
		controladorBBDD = new ControladorBBDD();
	}

    private void BtnLog_Clicked(object sender, EventArgs e)
    {
        string text = controladorBBDD.ObtenerId(email.Text, password.Text);
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;
        Toast.Make(text, duration, fontSize).Show();

        Navigation.PushAsync(new PagUsuarioLogeado(text)); 
    }
}