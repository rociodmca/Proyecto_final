using MongoDB.Bson;
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
                    idTema = 0;
                }
                if (miRadioButton.Value.ToString() == "1")
                {
                    miListaDiccionarios.Add(new TemaOscuro());
                    idTema = 1;
                }
                if (miRadioButton.Value.ToString() == "2")
                {
                    miListaDiccionarios.Add(new TemaAltoContraste());
                    idTema = 2;
                }
            }
            catch (Exception) { }
        }
    }

    private void guardar_Clicked(object sender, EventArgs e)
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
            DisplayAlert("Info", "Ajuste guardado", "Ok");
            Navigation.PopAsync();
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
}