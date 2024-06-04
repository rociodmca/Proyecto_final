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
    int idTema;
    

    public Ajustes(ObjectId id)
    {
        viewModelBBDD = new ViewModelBBDD();
        viewModelAjuste = new ViewModelAjuste();
        viewModelAjuste.ajuste = viewModelBBDD.ObtenerAjuste(id);
        /*miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
        miListaDiccionarios.Clear();*/
        /*if (viewModelAjuste.ajuste.Tema == "claro")
        {
            miListaDiccionarios.Add(new TemaClaro());
        }
        if (viewModelAjuste.ajuste.Tema == "oscuro")
        {
            miListaDiccionarios.Add(new TemaOscuro());
        }
        if (viewModelAjuste.ajuste.Tema == "contraste")
        {
            miListaDiccionarios.Add(new TemaAltoContraste());
        }*/
        /*if (viewModelAjuste.ajuste.Idioma == "Espanol")
        {
            miListaDiccionarios.Add(new Espanol());
        }
        if (viewModelAjuste.ajuste.Idioma == "Ingles")
        {
            miListaDiccionarios.Add(new Ingles());
        }*/
        idiomasList = ["Inglés", "Español"];
        this.id = id;
        InitializeComponent();
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

    private void tam_letra_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        //DisplayAlert("Info", tam_letra.Value.ToString(), "Ok");
        tema.FontSize = tam_letra.Value;
        tam_letra_lbl.FontSize = tam_letra.Value;
        idiomals_lbl.FontSize = tam_letra.Value;
        guardar.FontSize = tam_letra.Value;
        op1.FontSize = tam_letra.Value;
        op2.FontSize = tam_letra.Value;
        op3.FontSize = tam_letra.Value;
        idiomas.FontSize = tam_letra.Value;
    }

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
}