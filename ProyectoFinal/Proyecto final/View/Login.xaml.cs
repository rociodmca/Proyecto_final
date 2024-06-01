using Proyecto_final.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MongoDB.Bson;
using Proyecto_final.Resources.Temas;

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
        if (email.Text != null && password.Text != null)
        {
            string text = viewModelBBDD.ObtenerId(email.Text, password.Text);
            string tema = viewModelBBDD.ObtenerAjuste(new ObjectId(text)).Tema;
            ICollection<ResourceDictionary> miListaDiccionarios;
            miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
            miListaDiccionarios.Clear();
            try
            {
                if (tema == "claro")
                {
                    miListaDiccionarios.Add(new TemaClaro());
                }
                if (tema == "oscuro")
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                }
                if (tema == "contraste")
                {
                    miListaDiccionarios.Add(new TemaAltoContraste());
                }
            }
            catch (Exception) { }
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
        else
        {
            DisplayAlert("Error", "No se han introducido datos", "Ok");
        }
    }
}