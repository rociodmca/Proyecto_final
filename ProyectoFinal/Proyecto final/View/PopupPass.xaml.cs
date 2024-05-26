using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using MongoDB.Bson;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class PopupPass : Popup
{
	ObjectId id;
	ViewModelBBDD viewModelBBDD;

	public PopupPass(ObjectId id)
	{
		InitializeComponent();
		this.id = id;
		viewModelBBDD = new ViewModelBBDD();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		viewModelBBDD.ActualizarPass(id, passw.Text);
		this.Close();
    }
}