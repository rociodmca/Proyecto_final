namespace Proyecto_final.View;

public partial class Ajustes : ContentPage
{
    List<string> idiomasList = new List<string>();

	public Ajustes()
	{
        idiomasList = ["Inglés", "Español"];
        InitializeComponent();
        idiomas.ItemsSource = idiomasList;
        idiomas.SelectedItem = "Español";
        
	}

    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton miRadioButton = sender as RadioButton;
        if (miRadioButton.IsChecked)
        {
            ICollection<ResourceDictionary> miListaDiccionarios;
            miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
            miListaDiccionarios.Clear();
            try
            {
                if (miRadioButton.Value.ToString() == "0")
                {
                    /*miListaDiccionarios.Add(new TemaOriginal());
                    idTema = 0;*/
                    DisplayAlert("Info", miRadioButton.Value.ToString(), "Ok");
                }
                if (miRadioButton.Value.ToString() == "1")
                {
                    /*miListaDiccionarios.Add(new TemaOscuro());
                    idTema = 1;*/
                    DisplayAlert("Info", miRadioButton.Value.ToString(), "Ok");
                }
                if (miRadioButton.Value.ToString() == "2")
                {
                    /*miListaDiccionarios.Add(new TemaAltoContraste());
                    idTema = 2;*/
                    DisplayAlert("Info", miRadioButton.Value.ToString(), "Ok");
                }
            }
            catch (Exception) { }
        }
    }

    private void guardar_Clicked(object sender, EventArgs e)
    {

    }

    private void tam_letra_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        //DisplayAlert("Info", tam_letra.Value.ToString(), "Ok");
        tema.FontSize = tam_letra.Value;
        tam_letra_lbl.FontSize = tam_letra.Value;
        idiomals_lbl.FontSize= tam_letra.Value;
        guardar.FontSize = tam_letra.Value;
        op1.FontSize = tam_letra.Value;
        op2.FontSize = tam_letra.Value;
        op3.FontSize = tam_letra.Value;
        idiomas.FontSize = tam_letra.Value;
    }
}