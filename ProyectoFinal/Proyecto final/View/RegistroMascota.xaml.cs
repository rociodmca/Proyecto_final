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
        tipo.ItemsSource = new string[] { "Gato", "Perro" };
        sexo.ItemsSource = new string[] { "Macho", "Hembra" };
    }

    private void BtnRegistrarse_Clicked(object sender, EventArgs e)
    {
        if ((nombre.Text != "") && (tipo.SelectedIndex != -1) && (raza.SelectedIndex != -1) && (sexo.SelectedIndex != -1) && (peso.Text != ""))
        {
            if(viewModelBBDD.GuardarMascota(nombre.Text, tipo.SelectedItem.ToString(), raza.SelectedItem.ToString(), sexo.SelectedItem.ToString(), int.Parse(peso.Text), "", id))
            {
                DisplayAlert("Info", "Mascota guardada", "ok");
                nombre.Text = "";
                tipo.SelectedIndex = -1;
                raza.SelectedIndex = -1;
                sexo.SelectedIndex = -1;
                peso.Text = "";
            } else
            {
                DisplayAlert("Info", "Ha habido problemas al guardar la mascota", "ok");
            }
            Navigation.PopAsync();
        }
    }

    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        nombre.Text = "";
        tipo.SelectedIndex = -1;
        raza.SelectedIndex = -1;
        sexo.SelectedIndex = -1;
        peso.Text = "";
        Navigation.PopAsync();
    }

    private async void tipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tipo.SelectedIndex == 0)
        {
            Ejecutar();
            await RellenarListRazasGatos();
        }
        if (tipo.SelectedIndex == 1)
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
        raza.ItemsSource = listaRazasGatos;
    }

    public async Task RellenarListRazasPerros()
    {
        await Task.Delay(10);
        raza.ItemsSource = listaRazasPerros;
    }
}