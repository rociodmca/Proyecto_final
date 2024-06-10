
using Proyecto_final.Resources.Idiomas;
using Proyecto_final.Resources.Temas;

namespace Proyecto_final
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            loginDesktop.Content = new View.Login(this);
            loginPhone.Content = new View.Login(this);
                 
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                PhoneTabs.IsVisible = true;
                FlyoutBehavior = FlyoutBehavior.Disabled;
                SetNavBarIsVisible(this, false);
                //SetTabBarIsVisible(this, true);
                GoToAsync("///info");
            }
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                PhoneTabs.IsVisible = false;
                PhoneTabs2.IsVisible = false;
                FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }

        public Shell GetShell()
        {
            return shellEstructuraNavegacion;
        }
    }
}
