using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class CuestionarioCita : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    ViewModelUsuario viewModelUsuario;
    ViewModelMascota viewModelMascota;
    ObjectId id, id_mas, id_vet;

	/// <summary>
    /// Constructor que inicializa las variables y consulta en la base de datos
    /// </summary>
    /// <param name="id">identificador del usuario</param>
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
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
            desktop.IsVisible = false;
            fechalbl2.FontSize = 16;
            fecha2.FontSize = 16;
            mascotaslbl2.FontSize = 16;
            mascotas2.FontSize = 16;
            veterinarioslbl2.FontSize = 16;
            veterinarios2.FontSize = 16;
            BtnAceptar2.FontSize = 16;
            BtnCancelar2.FontSize = 16;
            mascotas2.ItemsSource = viewModelMascota.Mascotas;
            veterinarios2.ItemsSource = viewModelUsuario.Usuarios;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = false;
            desktop.IsVisible = true;
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
        
    }

    /// <summary>
    /// Método asociado al botón aceptar ppara guardar la cita en la base de datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnAceptar2_Clicked(object sender, EventArgs e)
    {
        if ((mascotas2.SelectedIndex != -1) && (veterinarios2.SelectedIndex != -1))
        {
            foreach (var item in viewModelUsuario.Usuarios)
            {
                if (item.Equals(veterinarios2.SelectedItem))
                {
                    id_vet = new ObjectId(item.Id);
                }
            }
            foreach (var item in viewModelMascota.Mascotas)
            {
                if (item.Equals(mascotas2.SelectedItem))
                {
                    id_mas = new ObjectId(item.Id);
                }
            }
            bool msg = viewModelBBDD.GuardarCita(id, id_mas, fecha2.Date, id_vet);
            if (msg)
            {
                DisplayAlert("Información", "Cita guardada", "Ok");
            }
            else
            {
                DisplayAlert("Información", "Ha ocurrido un error al guardar la cita", "Ok");
            }
        }
        else
        {
            DisplayAlert("Información", "No se han seleccionado todas las opciones", "Ok");
        }
        veterinarios2.SelectedIndex = -1;
        mascotas2.SelectedIndex = -1;
        Navigation.PopAsync();
    }

    /// <summary>
    /// Método asociado al botón cancelar para volver a la página anterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnCancelar2_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    /// <summary>
    /// Método asociado al botón aceptar ppara guardar la cita en la base de datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                DisplayAlert("Información", "Cita guardada", "Ok");
            }else
            {
                DisplayAlert("Información", "Ha ocurrido un error al guardar la cita", "Ok");
            }
        } else
        {
            DisplayAlert("Información", "No se han seleccionado todas las opciones", "Ok");
        }
        veterinarios.SelectedIndex = -1;
        mascotas.SelectedIndex = -1;
        Navigation.PopAsync();
    }

    /// <summary>
    /// Método asociado al botón cancelar para volver a la página anterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}