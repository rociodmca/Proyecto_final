using MongoDB.Bson;
using Proyecto_final.Model;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class Manual : ContentPage
{
    /// <summary>
    /// Constructor para inicializar los controles
    /// </summary>
	public Manual()
	{
		InitializeComponent();
    }

    /// <summary>
    /// M�todo sobreescrito de OnBackButtonPressed para inutilizar el bot�n de onback en Android
    /// </summary>
    /// <returns></returns>  
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}