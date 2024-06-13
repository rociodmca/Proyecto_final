using MongoDB.Bson;
using Proyecto_final.Resources.Idiomas;
using Proyecto_final.Resources.Temas;
using Proyecto_final.ViewModel;

namespace Proyecto_final.View;

public partial class Ajustes : ContentPage
{
    ViewModelBBDD viewModelBBDD;
    ViewModelAjuste viewModelAjuste;
    List<string> idiomasList = new List<string>();
    ObjectId id;
    int idTema, idTema2;
    AppShell apps;

    /// <summary>
    /// Constructor de la clase Ajustes
    /// </summary>
    /// <param name="id">identificador del usuario</param>
    /// <param name="apps"></param>
    public Ajustes(ObjectId id, AppShell apps)
    {
        viewModelBBDD = new ViewModelBBDD();
        viewModelAjuste = new ViewModelAjuste();
        viewModelAjuste.ajuste = viewModelBBDD.ObtenerAjuste(id);
        this.id = id;
        this.apps = apps;
        idiomasList = ["Inglés", "Español"];
        InitializeComponent();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            Appearing += MainPage_Appearing;
        }
        if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            desktop.IsVisible = true;
            phone.IsVisible = false;
            idiomas.ItemsSource = idiomasList;
            if (viewModelAjuste.ajuste.Idioma == "Espanol")
            {
                idiomas.SelectedItem = "Español";
            }
            if (viewModelAjuste.ajuste.Idioma == "Ingles")
            {
                idiomas.SelectedItem = "Inglés";
            }
            tam_letra.Value = double.Parse(viewModelAjuste.ajuste.Tam_letra);
            tema.FontSize = tam_letra.Value;
            tam_letra_lbl.FontSize = tam_letra.Value;
            idiomals_lbl.FontSize = tam_letra.Value;
            guardar.FontSize = tam_letra.Value;
            op1.FontSize = tam_letra.Value;
            op2.FontSize = tam_letra.Value;
            op3.FontSize = tam_letra.Value;
            idiomas.FontSize = tam_letra.Value;
            if (viewModelAjuste.ajuste.Tema == "claro")
            {
                op1.IsChecked = true;
                idTema = 0;
            }
            if (viewModelAjuste.ajuste.Tema == "oscuro")
            {
                op2.IsChecked = true;
                idTema = 1;
            }
            if (viewModelAjuste.ajuste.Tema == "contraste")
            {
                op3.IsChecked = true;
                idTema = 2;
            }
        }

    }

    /// <summary>
    /// Método al que se llama cuando aparece se muestra esta ContentPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainPage_Appearing(object sender, EventArgs e)
    {
        CargarPag();
    }

    /// <summary>
    /// Método para ajustar la interfaz con los datos de la tabla Ajustes guardados en 
    /// la base de datos
    /// </summary>
    public void CargarPag()
    {
        viewModelAjuste.ajuste = viewModelBBDD.ObtenerAjuste(id);
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        phone.IsVisible = true;
        desktop.IsVisible = false;
        idiomas2.ItemsSource = idiomasList;
        if (viewModelAjuste.ajuste.Idioma == "Espanol")
        {
            miListaDiccionarios.Add(new Espanol());
            idiomas2.SelectedItem = "Español";
        }
        if (viewModelAjuste.ajuste.Idioma == "Ingles")
        {
            miListaDiccionarios.Add(new Ingles());
            idiomas2.SelectedItem = "Inglés";
        }
        tam_letra2.Value = double.Parse(viewModelAjuste.ajuste.Tam_letra);
        tema2.FontSize = tam_letra2.Value;
        tam_letra_lbl2.FontSize = tam_letra2.Value;
        idiomals_lbl2.FontSize = tam_letra2.Value;
        guardar2.FontSize = tam_letra2.Value;
        op12.FontSize = tam_letra2.Value;
        op22.FontSize = tam_letra2.Value;
        op32.FontSize = tam_letra2.Value;
        idiomas2.FontSize = tam_letra2.Value;
        if (viewModelAjuste.ajuste.Tema == "claro")
        {
            miListaDiccionarios.Add(new TemaClaro());
            op12.IsChecked = true;
            idTema2 = 0;
        }
        if (viewModelAjuste.ajuste.Tema == "oscuro")
        {
            miListaDiccionarios.Add(new TemaOscuro());
            op22.IsChecked = true;
            idTema2 = 1;
        }
        if (viewModelAjuste.ajuste.Tema == "contraste")
        {
            miListaDiccionarios.Add(new TemaAltoContraste());
            op32.IsChecked = true;
            idTema2 = 2;
        }
    }

    /// <summary>
    /// Método asociado al radiobutton de selección del tema
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
                    if (idiomas.SelectedIndex == 1)
                    {
                        miListaDiccionarios.Add(new Espanol());
                    }
                    if (idiomas.SelectedIndex == 0)
                    {
                        miListaDiccionarios.Add(new Ingles());
                    }
                    idTema = 0;
                }
                else
                {
                    if (miRadioButton.Value.ToString() == "1")
                    {
                        miListaDiccionarios.Add(new TemaOscuro());
                        if (idiomas.SelectedIndex == 1)
                        {
                            miListaDiccionarios.Add(new Espanol());
                        }
                        if (idiomas.SelectedIndex == 0)
                        {
                            miListaDiccionarios.Add(new Ingles());
                        }
                        idTema = 1;
                    }
                    else
                    {
                        if (miRadioButton.Value.ToString() == "2")
                        {
                            miListaDiccionarios.Add(new TemaAltoContraste());
                            if (idiomas.SelectedIndex == 1)
                            {
                                miListaDiccionarios.Add(new Espanol());
                            }
                            if (idiomas.SelectedIndex == 0)
                            {
                                miListaDiccionarios.Add(new Ingles());
                            }
                            idTema = 2;
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }

    /// <summary>
    /// Método asociado al botón para guardar los ajustes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void guardar_Clicked(object sender, EventArgs e)
    {
        string tema_ = "", idioma_ = "";
        if (idTema == 0)
        {
            tema_ = "claro";
        }
        if (idTema == 1)
        {
            tema_ = "oscuro";
        }
        if (idTema == 2)
        {
            tema_ = "contraste";
        }
        if (idiomas.SelectedItem == "Español")
        {
            idioma_ = "Espanol";
        }
        if (idiomas.SelectedItem == "Inglés")
        {
            idioma_ = "Ingles";
        }
        if (viewModelBBDD.ActualizarAjuste(id, tema_, tam_letra.Value.ToString(), idioma_))
        {
            var recurso1 = (string)Application.Current.Resources["ajustes1"];
            var recurso2 = (string)Application.Current.Resources["ajustes2"];
            var recurso3 = (string)Application.Current.Resources["ajustes3"];
            await DisplayAlert(recurso1, recurso2, recurso3);
            await Navigation.PopAsync();
        }
    }

    /// <summary>
    /// Método asociado al slider para modificar el tamaño de la letra
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tam_letra_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        tema.FontSize = tam_letra.Value;
        tam_letra_lbl.FontSize = tam_letra.Value;
        idiomals_lbl.FontSize = tam_letra.Value;
        guardar.FontSize = tam_letra.Value;
        op1.FontSize = tam_letra.Value;
        op2.FontSize = tam_letra.Value;
        op3.FontSize = tam_letra.Value;
        idiomas.FontSize = tam_letra.Value;
    }

    /// <summary>
    /// Método asociado al selector de idiomas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void idiomas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ICollection<ResourceDictionary> miListaDiccionarios;
        miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();
        if (idiomas.SelectedIndex == 1)
        {
            miListaDiccionarios.Add(new Espanol());
            if (idTema == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            else
            {
                if (idTema == 1)
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                }
                else
                {
                    if (idTema == 2)
                    {
                        miListaDiccionarios.Add(new TemaAltoContraste());
                    }
                }
            }
        }
        if (idiomas.SelectedIndex == 0)
        {
            miListaDiccionarios.Add(new Ingles());
            if (idTema == 0)
            {
                miListaDiccionarios.Add(new TemaClaro());
            }
            else
            {
                if (idTema == 1)
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                }
                else
                {
                    if (idTema == 2)
                    {
                        miListaDiccionarios.Add(new TemaAltoContraste());
                    }
                }
            }
        }
    }

    /// <summary>
    /// Método asociado al botón para guardar los ajustes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void guardar2_Clicked(object sender, EventArgs e)
    {
        string tema_ = "", idioma_ = "";
        if (idTema2 == 0)
        {
            tema_ = "claro";
        }
        if (idTema2 == 1)
        {
            tema_ = "oscuro";
        }
        if (idTema2 == 2)
        {
            tema_ = "contraste";
        }
        if (idiomas2.SelectedItem == "Español")
        {
            idioma_ = "Espanol";
        }
        if (idiomas2.SelectedItem == "Inglés")
        {
            idioma_ = "Ingles";
        }
        if (viewModelBBDD.ActualizarAjuste(id, tema_, tam_letra2.Value.ToString(), idioma_))
        {
            var recurso1 = (string)Application.Current.Resources["ajustes1"];
            var recurso2 = (string)Application.Current.Resources["ajustes2"];
            var recurso3 = (string)Application.Current.Resources["ajustes3"];
            await DisplayAlert(recurso1, recurso2, recurso3);
            await Navigation.PopAsync();
        }
    }

    /// <summary>
    /// Método asociado al slider para modificar el tamaño de la letra
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tam_letra2_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        tema2.FontSize = tam_letra2.Value;
        tam_letra_lbl2.FontSize = tam_letra2.Value;
        idiomals_lbl2.FontSize = tam_letra2.Value;
        guardar2.FontSize = tam_letra2.Value;
        op12.FontSize = tam_letra2.Value;
        op22.FontSize = tam_letra2.Value;
        op32.FontSize = tam_letra2.Value;
        idiomas2.FontSize = tam_letra2.Value;
    }

    /// <summary>
    /// Método asociado al radiobutton de temas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void op12_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton miRadioButton = sender as RadioButton;
        if (miRadioButton.IsChecked)
        {
            ICollection<ResourceDictionary> miListaDiccionarios2;
            miListaDiccionarios2 = Application.Current.Resources.MergedDictionaries;
            miListaDiccionarios2.Clear();
            try
            {
                if (miRadioButton.Value.ToString() == "3")
                {
                    miListaDiccionarios2.Add(new TemaClaro());
                    if (idiomas2.SelectedIndex == 1)
                    {
                        miListaDiccionarios2.Add(new Espanol());
                    }
                    if (idiomas2.SelectedIndex == 0)
                    {
                        miListaDiccionarios2.Add(new Ingles());
                    }
                    idTema2 = 0;
                }
                else
                {
                    if (miRadioButton.Value.ToString() == "4")
                    {
                        miListaDiccionarios2.Add(new TemaOscuro());
                        if (idiomas2.SelectedIndex == 1)
                        {
                            miListaDiccionarios2.Add(new Espanol());
                        }
                        if (idiomas2.SelectedIndex == 0)
                        {
                            miListaDiccionarios2.Add(new Ingles());
                        }
                        idTema2 = 1;
                    }
                    else
                    {
                        if (miRadioButton.Value.ToString() == "5")
                        {
                            miListaDiccionarios2.Add(new TemaAltoContraste());
                            if (idiomas2.SelectedIndex == 1)
                            {
                                miListaDiccionarios2.Add(new Espanol());
                            }
                            if (idiomas2.SelectedIndex == 0)
                            {
                                miListaDiccionarios2.Add(new Ingles());
                            }
                            idTema2 = 2;
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}