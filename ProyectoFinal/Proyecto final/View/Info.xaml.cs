using Proyecto_final.Resources.Idiomas;
using Proyecto_final.Resources.Temas;

namespace Proyecto_final.View;

public partial class Info : ContentPage
{
	public Info()
	{
		InitializeComponent();
    }

    private void idiomas_Toggled(object sender, ToggledEventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        if (idiomas.IsToggled)
        {
            miListaDiccionarios.Add(new Ingles());
            if (tema.SelectedIndex == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            if (tema.SelectedIndex == 1)
            {
                miListaDiccionarios.Add(new TemaOscuro());
            }
            if (tema.SelectedIndex == 2)
            {
                miListaDiccionarios.Add(new TemaAltoContraste());
            }
        }
        if (!idiomas.IsToggled)
        {
            miListaDiccionarios.Add(new Espanol());
            if (tema.SelectedIndex == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            if (tema.SelectedIndex == 1)
            {
                miListaDiccionarios.Add(new TemaOscuro());
            }
            if (tema.SelectedIndex == 2)
            {
                miListaDiccionarios.Add(new TemaAltoContraste());
            }
        }
    }

    private void tam_letra_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        info1.FontSize = tam_letra.Value + 6;
        info2.FontSize = tam_letra.Value;
    }

    private void tema_SelectedIndexChanged(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        if (tema.SelectedIndex == 0)
        {
            miListaDiccionarios.Add(new TemaClaro());
        }
        if (tema.SelectedIndex == 1)
        {
            miListaDiccionarios.Add(new TemaOscuro());
        }
        if (tema.SelectedIndex == 2)
        {
            miListaDiccionarios.Add(new TemaAltoContraste());
        }
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {

    }
}