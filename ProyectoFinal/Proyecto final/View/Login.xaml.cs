using Proyecto_final.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MongoDB.Bson;

namespace Proyecto_final.View;

public partial class Login : ContentPage
{
    ViewModelBBDD viewModelBBDD;


    public Login()
	{
		InitializeComponent();
		viewModelBBDD = new ViewModelBBDD();
	}

    private void BtnLog_Clicked(object sender, EventArgs e)
    {
        string text = viewModelBBDD.ObtenerId(email.Text, password.Text);
        /*ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;*/
        //Toast.Make(text, duration, fontSize).Show();

        if (viewModelBBDD.ObtenerRol(email.Text, password.Text) == 1)
        {
            Navigation.PushAsync(new PanelAdministracion());
        } 
        if (viewModelBBDD.ObtenerRol(email.Text, password.Text) == 2)
        {
            Navigation.PushAsync(new PagVeterinario(new ObjectId(text)));
        }
        if (viewModelBBDD.ObtenerRol(email.Text, password.Text) == 3)
        {
            Navigation.PushAsync(new PagUsuarioLogeado(new ObjectId(text)));
        }

    }
}