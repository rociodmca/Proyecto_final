using Proyecto_final.Resources.Idiomas;
using Proyecto_final.Resources.Temas;

namespace Proyecto_final.View;

public partial class Info : ContentPage
{
    int tema;

    /// <summary>
    /// Constructor que inicializa los controles
    /// </summary>
    public Info()
    {
        InitializeComponent();
        info1.FontSize = tam_letra.Value + 6;
        info2.FontSize = tam_letra.Value;
        size.FontSize = tam_letra.Value;
        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            size.IsVisible = false;
            op1.IsVisible = false;
            op2.IsVisible = false;
            op3.IsVisible = false;
        }
    }

    /// <summary>
    /// Método asociado al toggle de idiomas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void idiomas_Toggled(object sender, ToggledEventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        if (idiomas.IsToggled)
        {
            miListaDiccionarios.Add(new Ingles());
            if (tema == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            if (tema == 1)
            {
                miListaDiccionarios.Add(new TemaOscuro());
            }
            if (tema == 2)
            {
                miListaDiccionarios.Add(new TemaAltoContraste());
            }
        }
        if (!idiomas.IsToggled)
        {
            miListaDiccionarios.Add(new Espanol());
            if (tema == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            if (tema == 1)
            {
                miListaDiccionarios.Add(new TemaOscuro());
            }
            if (tema == 2)
            {
                miListaDiccionarios.Add(new TemaAltoContraste());
            }
        }
    }

    /// <summary>
    /// Método asociado al slider para cambiar el tamaño de la letra
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tam_letra_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        info1.FontSize = tam_letra.Value + 6;
        info2.FontSize = tam_letra.Value;
        size.FontSize= tam_letra.Value;
    }

    /// <summary>
    /// Método asociado al radiobutton para cambiar el tema
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                    miListaDiccionarios.Add(new TemaClaro());
                    tema = 0;
                }
                if (miRadioButton.Value.ToString() == "1")
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                    tema = 1;
                }
                if (miRadioButton.Value.ToString() == "2")
                {
                    miListaDiccionarios.Add(new TemaAltoContraste());
                    tema = 2;
                }
            }
            catch (Exception) { }
        }
    }

    /// <summary>
    /// Método sobreescrito de OnBackButtonPressed para inutilizar el botón de onback en Android
    /// </summary>
    /// <returns></returns>    
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}