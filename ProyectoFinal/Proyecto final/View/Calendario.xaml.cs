using MongoDB.Bson;
using Proyecto_final.ViewModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using Proyecto_final.Resources.Temas;
using Proyecto_final.Resources.Idiomas;

namespace Proyecto_final.View;

public partial class Calendario : ContentPage
{
    ObjectId id;
    ViewModelBBDD viewModelBBDD;
    ViewModelMascota viewModelMascota;
    ViewModelCita viewModelCita;
    string nombre;
    List<DateTime> citas;
    ViewModelEvento viewModelEvento;
    bool flag = true;
    AppShell apps;

    public Calendario(ObjectId id, AppShell apps)
	{
        this.id = id;
        this.apps = apps;
        viewModelBBDD = new ViewModelBBDD();
        viewModelMascota = new ViewModelMascota();
        viewModelCita = new ViewModelCita();
        citas = new List<DateTime>();
        viewModelEvento = new ViewModelEvento();

        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas(id);
        viewModelCita.Citas = viewModelBBDD.ObtenerListaCitas(id);
        nombre = viewModelBBDD.ObtenerNombre(id);

        InitializeComponent();

        Appearing += MainPage_Appearing;

        //CargarPag();
        

    }

    public void CargarPag()
    {
        Thread.Sleep(10);
        Thread thread1 = new Thread(new ThreadStart(Ejecutar3));
        thread1.Start();
    }

    public async Task VisualizarTabla()
    {
        await Task.Delay(20);
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                string idioma = viewModelBBDD.ObtenerAjuste(id).Idioma;
                if (idioma == "Espanol")
                {
                    calendario2.Culture = new System.Globalization.CultureInfo("es-ES");
                }
                if (idioma == "Ingles")
                {
                    calendario2.Culture = new System.Globalization.CultureInfo("en-GB");
                }
                calendario2.Events = viewModelEvento.eventos;
                calendario2.IsVisible = true;
            });
        }
    }

    public async void Ejecutar3()
    {
        Thread.Sleep(10);
        await VisualizarTabla();
    }

    private void MainPage_Appearing(object sender, EventArgs e)
    {
        CargarPag();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Microsoft.Maui.Controls.Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        miListaDiccionarios.Add(new TemaClaro());
        miListaDiccionarios.Add(new Espanol());
        Navigation.PopAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CuestionarioCita(id));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        var pop = new PopupPass(id);
        this.ShowPopup(pop);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        bool msg = await DisplayAlert("Guardar mascota", "¿Quieres guardar una mascota?", "Aceptar", "Cancelar");
        if (msg)
        {
            await Navigation.PushAsync(new RegistroMascota(id));
            //await Navigation.PushAsync(new Ajustes(id));
        }
    }

    private void lista2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        //DisplayAlert("Info", lista2.SelectedItem.ToString(), "Ok");

    }

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
                /*var tabBar = (TabBar)apps.CurrentItem;
                var info = tabBar.Items[0];
                var registro = tabBar.Items[1];
                var login = tabBar.Items[2];
                var manual = tabBar.Items[3];
                var calendario = tabBar.Items[4];
                var home = tabBar.Items[5];
                var ajustes = tabBar.Items[6];

                if (info != null)
                {
                    info.IsVisible = true;
                }
                if (registro != null)
                {
                    registro.IsVisible = true;
                }
                if (login != null)
                {
                    login.IsVisible = true;
                    // Cambia las propiedades según sea necesario
                    //login.Title = "Ajustes";
                    //login.Icon = "ajustes.png";
                    //login.CurrentItem.Content = new Ajustes(new ObjectId(text));
                }
                if (manual != null)
                {
                    manual.IsVisible = true;
                }
                if (calendario != null)
                {
                    calendario.IsVisible = false;
                }
                if (home != null)
                {
                    home.IsVisible = false;
                }
                if (ajustes != null)
                {
                    ajustes.IsVisible = false;
                }
                await DisplayAlert("Info", Shell.Current.Items.Count.ToString(), "ok");*/
                await Shell.Current.GoToAsync("//info");
                //await Navigation.PopToRootAsync();
            }
        });

        return true;
    }
}