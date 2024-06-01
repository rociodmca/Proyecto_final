
using Proyecto_final.Resources.Temas;

namespace Proyecto_final
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            ICollection<ResourceDictionary> miListaDiccionarios;
            miListaDiccionarios = Application.Current.Resources.MergedDictionaries;
            miListaDiccionarios.Clear();
            miListaDiccionarios.Add(new TemaClaro());
            App.Current.UserAppTheme = AppTheme.Light;

            InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                PhoneTabs.IsVisible = true;
                FlyoutBehavior = FlyoutBehavior.Disabled;
                SetNavBarIsVisible(this, false);
            }
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                PhoneTabs.IsVisible = false;
                FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }

        public Shell GetShell()
        {
            return shellEstructuraNavegacion;
        }
    }
}
