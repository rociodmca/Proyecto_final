using Proyecto_final.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Proyecto_final.View;

public partial class Login : ContentPage
{
    ViewModelBBDD controladorBBDD;


    public Login()
	{
		InitializeComponent();
		controladorBBDD = new ViewModelBBDD();
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