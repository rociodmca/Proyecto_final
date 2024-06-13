using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using MongoDB.Bson;
using Proyecto_final.ViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace Proyecto_final.View;

/// <summary>
/// Clase heredada de Popup
/// </summary>
public partial class PopupPass : Popup
{
	ObjectId id;
	ViewModelBBDD viewModelBBDD;

	/// <summary>
	/// Constructor que inicializa los controles
	/// </summary>
	/// <param name="id">identificador del usuario que cambiar� la contrase�a</param>
	public PopupPass(ObjectId id)
	{
		InitializeComponent();
		this.id = id;
		viewModelBBDD = new ViewModelBBDD();
	}

	/// <summary>
	/// M�todo asociado al bot�n para actualizar la contrase�a
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void Button_Clicked(object sender, EventArgs e)
    {
		bool actualizado = viewModelBBDD.ActualizarPass(id, passw.Text);
		this.Close();
		MainThread.BeginInvokeOnMainThread(() =>
		{
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;
            if (actualizado)
            {
                Toast.Make("Correctamente actualizada la contrase�a", duration, fontSize).Show();
            }
			else
			{
                Toast.Make("Error al actualizar la contrase�a", duration, fontSize).Show();
            }    
        });
    }
}