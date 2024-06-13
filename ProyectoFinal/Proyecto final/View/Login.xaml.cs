using Proyecto_final.ViewModel;
using System.Threading.Tasks;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MongoDB.Bson;
using Proyecto_final.Resources.Temas;
using Proyecto_final.Resources.Idiomas;

namespace Proyecto_final.View;

public partial class Login : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    private AppShell apps;

    /// <summary>
    /// Constructor para inicializar los controles
    /// </summary>
    /// <param name="apps"></param>
    public Login(AppShell apps)
    {
        this.apps = apps;
        
        InitializeComponent();
        size.Value = 20;
        lbl_email2.FontSize = size.Value;
        email2.FontSize = size.Value;
        lbl_pass2.FontSize = size.Value;
        password2.FontSize = size.Value;
        BtnLog2.FontSize = size.Value;
        viewModelBBDD = new ViewModelBBDD();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
        }

        this.apps = apps;
    }

    /// <summary>
    /// Método asociado al botón de login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void BtnLog_Clicked(object sender, EventArgs e)
    {
        if (email1.Text != null && password1.Text != null)
        {
            Thread hilo = new Thread(new ThreadStart(AjustarInterfaz));
            hilo.IsBackground = true;
            hilo.Start();
            string text = viewModelBBDD.ObtenerId(email1.Text, password1.Text);
            int rol = viewModelBBDD.ObtenerRol(email1.Text, password1.Text);
            if (rol == 1)
            {
                email1.Text = null;
                password1.Text = null;
                await DisplayAlert("Info", "Usuario no compatible con el dispositivo", "Ok");
            }
            if (rol == 2)
            {
                email1.Text = null;
                password1.Text = null;
                var tabBar = (TabBar)apps.Items.FirstOrDefault(item => item.Title == "log");
                var calendario = tabBar.Items.FirstOrDefault(item => item.Route == "calendario");
                var home = tabBar.Items.FirstOrDefault(item => item.Route == "home");

                if (calendario != null)
                {
                    calendario.IsVisible = false;
                }
                if (home != null)
                {
                    home.Items[0].Content = new PagVeterinario(new ObjectId(text), apps);
                }
                await Shell.Current.GoToAsync("//home");
            }
            if (rol == 3)
            {
                email1.Text = null;
                password1.Text = null;
                var tabBar = (TabBar)apps.Items.FirstOrDefault(item => item.Title == "log");
                var calendario = tabBar.Items.FirstOrDefault(item => item.Route == "calendario");
                var home = tabBar.Items.FirstOrDefault(item => item.Route == "home");

                if (calendario != null)
                {
                    calendario.Items[0].Content = new Calendario(new ObjectId(text), apps);
                }
                if (home != null)
                {
                    home.Items[0].Content = new PagUsuarioLogeado(new ObjectId(text), apps);
                }
                await Shell.Current.GoToAsync("//home");
            }
            
        }
        else
        {
            await DisplayAlert("Error", "No se han introducido datos", "Ok");
        }

    }

    /// <summary>
    /// Método para cargar los ajustes en los controles
    /// </summary>
    private void AjustarInterfaz()
    {
        string text = viewModelBBDD.ObtenerId(email1.Text, password1.Text);
        string tema = viewModelBBDD.ObtenerAjuste(new ObjectId(text)).Tema;
        string idioma = viewModelBBDD.ObtenerAjuste(new ObjectId(text)).Idioma;
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
            if (idioma == "Espanol")
            {
                miListaDiccionarios.Add(new Espanol());
            }
            if (idioma == "Ingles")
            {
                miListaDiccionarios.Add(new Ingles());
            }
        }
        catch (Exception) { }
    }

    /// <summary>
    /// Método para cargar los ajustes en los controles
    /// </summary>
    private void AjustarInterfaz2()
    {
        string text = viewModelBBDD.ObtenerId(email2.Text, password2.Text);
        string tema = viewModelBBDD.ObtenerAjuste(new ObjectId(text)).Tema;
        string idioma = viewModelBBDD.ObtenerAjuste(new ObjectId(text)).Idioma;
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        try
        {
            if (tema == "claro")
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            else
            {
                if (tema == "oscuro")
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                }
                else
                {
                    if (tema == "contraste")
                    {
                        miListaDiccionarios.Add(new TemaAltoContraste());
                    }
                }
            }
            if (idioma == "Espanol")
            {
                miListaDiccionarios.Add(new Espanol());
            }
            else
            {
                if (idioma == "Ingles")
                {
                    miListaDiccionarios.Add(new Ingles());
                }
            }
        }
        catch (Exception) { }
    }

    /// <summary>
    /// Método asociado al botón de login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void BtnLog2_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (email2.Text != null && password2.Text != null)
            {
                Thread hilo = new Thread(new ThreadStart(AjustarInterfaz2));
                hilo.IsBackground = true;
                hilo.Start();
                AjustarInterfaz2();
                string text = viewModelBBDD.ObtenerId(email2.Text, password2.Text);
                int rol = viewModelBBDD.ObtenerRol(email2.Text, password2.Text);
                if (rol == 1)
                {
                    await Navigation.PushAsync(new PanelAdministracion(apps));
                }
                else
                {
                    if (rol == 2)
                    {
                        await Navigation.PushAsync(new PagVeterinario(new ObjectId(text), apps));
                    }
                    else
                    {
                        if (rol == 3)
                        {
                            await Navigation.PushAsync(new PagUsuarioLogeado(new ObjectId(text), apps));
                        }
                    }
                }
                apps.FlyoutBehavior = FlyoutBehavior.Disabled;
                
                
                email2.Text = null;
                password2.Text = null;
            }
            else
            {
                await DisplayAlert("Error", "No se han introducido datos", "Ok");
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
            email2.Text = null;
            password2.Text = null;
        }
    }

    /// <summary>
    /// Método asociado al slider para cambiar el tamaño de la letra
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void size_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        lbl_email2.FontSize = size.Value;
        email2.FontSize = size.Value;
        lbl_pass2.FontSize = size.Value;
        password2.FontSize = size.Value;
        BtnLog2.FontSize = size.Value;
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