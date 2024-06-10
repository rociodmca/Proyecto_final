using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class CuestionarioCita : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    ViewModelUsuario viewModelUsuario;
    ViewModelMascota viewModelMascota;
    ObjectId id, id_mas, id_vet;

	public CuestionarioCita(ObjectId id)
	{
		viewModelBBDD = new ViewModelBBDD();
        viewModelUsuario = new ViewModelUsuario();
        viewModelUsuario.Usuarios = viewModelBBDD.ObtenerListaVeterinarios();
        viewModelMascota = new ViewModelMascota();
        viewModelMascota.Mascotas = viewModelBBDD.ObtenerListaMascotas(id);
        this.id = id;
        double size = double.Parse(viewModelBBDD.ObtenerAjuste(id).Tam_letra);

        InitializeComponent();
        fechalbl.FontSize = size;
        fecha.FontSize = size;
        mascotaslbl.FontSize = size;
        mascotas.FontSize = size;
        veterinarioslbl.FontSize = size;
        veterinarios.FontSize = size;
        BtnAceptar.FontSize = size;
        BtnCancelar.FontSize = size;
        mascotas.ItemsSource = viewModelMascota.Mascotas;
        veterinarios.ItemsSource = viewModelUsuario.Usuarios;
    }

    private void BtnAceptar_Clicked(object sender, EventArgs e)
    {
        if ((mascotas.SelectedIndex != -1) && (veterinarios.SelectedIndex != -1))
        {
            foreach (var item in viewModelUsuario.Usuarios)
            {
                if (item.Equals(veterinarios.SelectedItem))
                {
                    id_vet = new ObjectId(item.Id);
                }
            }
            foreach (var item in viewModelMascota.Mascotas)
            {
                if (item.Equals(mascotas.SelectedItem))
                {
                    id_mas = new ObjectId(item.Id);
                }
            }
            bool msg = viewModelBBDD.GuardarCita(id, id_mas, fecha.Date, id_vet);
            if (msg)
            {
                //DisplayAlert("Información", "Cita guardada", "Ok");
            }else
            {
                //DisplayAlert("Información", "Ha ocurrido un error al guardar la cita", "Ok");
            }
        } else
        {
            //DisplayAlert("Información", "No se han seleccionado todas las opciones", "Ok");
        }
        veterinarios.SelectedIndex = -1;
        mascotas.SelectedIndex = -1;
        Navigation.PopAsync();
    }

    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}