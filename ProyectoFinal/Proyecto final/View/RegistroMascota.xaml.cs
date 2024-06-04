using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class RegistroMascota : ContentPage
{
    ViewModelCatAPI viewModelCat;
    ViewModelDogAPI viewModelDog;
    ViewModelBBDD viewModelBBDD;
    ObjectId id;
    List<string> listaRazasGatos;
    List<string> listaRazasPerros;

	public RegistroMascota(ObjectId id)
	{
		viewModelCat = new ViewModelCatAPI();
        viewModelDog = new ViewModelDogAPI();
        viewModelBBDD = new ViewModelBBDD();
        this.id = id;

        InitializeComponent();
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            tipo1.ItemsSource = new string[] { "Gato", "Perro" };
            sexo1.ItemsSource = new string[] { "Macho", "Hembra" };
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
            tipo2.ItemsSource = new string[] { "Gato", "Perro" };
            sexo2.ItemsSource = new string[] { "Macho", "Hembra" };
        }
        
    }

    private void BtnRegistrarse1_Clicked(object sender, EventArgs e)
    {
        if ((nombre1.Text != "") && (tipo1.SelectedIndex != -1) && (raza1.SelectedIndex != -1) && (sexo1.SelectedIndex != -1) && (peso1.Text != ""))
        {
            Uri? imagen = null;
            if (tipo1.SelectedItem.ToString() == "Gato")
            {
                string reference = viewModelCat.DiccionarioRefImagenes()[raza1.SelectedItem.ToString()];
                imagen = new Uri("https://cdn2.thecatapi.com/images/" + reference + ".jpg");
            }
            if (tipo1.SelectedItem.ToString() == "Perro")
            {
                string reference = viewModelDog.DiccionarioRefImagenes()[raza1.SelectedItem.ToString()];
                imagen = new Uri("https://cdn2.thedogapi.com/images/" + reference + ".jpg");
            }
            if (viewModelBBDD.GuardarMascota(nombre1.Text, tipo1.SelectedItem.ToString(), raza1.SelectedItem.ToString(), sexo1.SelectedItem.ToString(), int.Parse(peso1.Text), "", imagen, id))
            {
                DisplayAlert("Info", "Mascota guardada", "ok");
                nombre1.Text = "";
                tipo1.SelectedIndex = -1;
                raza1.SelectedIndex = -1;
                sexo1.SelectedIndex = -1;
                peso1.Text = "";
            } else
            {
                DisplayAlert("Info", "Ha habido problemas al guardar la mascota", "ok");
            }
            Navigation.PopAsync();
        }
    }

    private void BtnCancelar1_Clicked(object sender, EventArgs e)
    {
        nombre1.Text = "";
        tipo1.SelectedIndex = -1;
        raza1.SelectedIndex = -1;
        sexo1.SelectedIndex = -1;
        peso1.Text = "";
        Navigation.PopAsync();
    }

    private async void Tipo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo1.SelectedIndex == 0)
        {
            Ejecutar();
            await RellenarListRazasGatos();
        }
        if (tipo1.SelectedIndex == 1)
        {
            Ejecutar();
            await RellenarListRazasPerros();
        }
    }

    public void Ejecutar()
    {
        listaRazasGatos = viewModelCat.RazasList();
        listaRazasPerros = viewModelDog.RazasList();
    }

    public async Task RellenarListRazasGatos()
    {
        await Task.Delay(10);
        raza1.ItemsSource = listaRazasGatos;
    }

    public async Task RellenarListRazasPerros()
    {
        await Task.Delay(10);
        raza1.ItemsSource = listaRazasPerros;
    }

    public async Task RellenarListRazasGatos2()
    {
        await Task.Delay(10);
        raza2.ItemsSource = listaRazasGatos;
    }

    public async Task RellenarListRazasPerros2()
    {
        await Task.Delay(10);
        raza2.ItemsSource = listaRazasPerros;
    }

    private async void Tipo2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo2.SelectedIndex == 0)
        {
            Ejecutar();
            await RellenarListRazasGatos2();
        }
        if (tipo2.SelectedIndex == 1)
        {
            Ejecutar();
            await RellenarListRazasPerros2();
        }
    }

    private void BtnRegistrarse2_Clicked(object sender, EventArgs e)
    {
        if ((nombre2.Text != "") && (tipo2.SelectedIndex != -1) && (raza2.SelectedIndex != -1) && (sexo2.SelectedIndex != -1) && (peso2.Text != ""))
        {
            Uri? imagen = null;
            if (tipo2.SelectedItem.ToString() == "Gato")
            {
                string reference = viewModelCat.DiccionarioRefImagenes()[raza2.SelectedItem.ToString()];
                imagen = new Uri("https://cdn2.thecatapi.com/images/" + reference + ".jpg");
            }
            if (tipo2.SelectedItem.ToString() == "Perro")
            {
                string reference = viewModelDog.DiccionarioRefImagenes()[raza2.SelectedItem.ToString()];
                imagen = new Uri("https://cdn2.thedogapi.com/images/" + reference + ".jpg");
            }
            if (viewModelBBDD.GuardarMascota(nombre2.Text, tipo2.SelectedItem.ToString(), raza2.SelectedItem.ToString(), sexo2.SelectedItem.ToString(), int.Parse(peso2.Text), "", imagen, id))
            {
                DisplayAlert("Info", "Mascota guardada", "ok");
                nombre2.Text = "";
                tipo2.SelectedIndex = -1;
                raza2.SelectedIndex = -1;
                sexo2.SelectedIndex = -1;
                peso2.Text = "";
            }
            else
            {
                DisplayAlert("Info", "Ha habido problemas al guardar la mascota", "ok");
            }
            Navigation.PopAsync();
        }
    }

    private void BtnCancelar2_Clicked(object sender, EventArgs e)
    {
        nombre2.Text = "";
        tipo2.SelectedIndex = -1;
        raza2.SelectedIndex = -1;
        sexo2.SelectedIndex = -1;
        peso2.Text = "";
        Navigation.PopAsync();
    }
}