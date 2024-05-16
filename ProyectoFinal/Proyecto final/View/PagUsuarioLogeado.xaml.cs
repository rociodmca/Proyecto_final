namespace Proyecto_final.View;

public partial class PagUsuarioLogeado : ContentPage
{
    string id;

	public PagUsuarioLogeado(string id)
	{
		InitializeComponent();
        this.id = id;
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            phone.IsVisible = true;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            phone.IsVisible = true;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CuestionarioCita(id));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {

    }
}