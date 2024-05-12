using Proyecto_final.Modelo;

namespace Proyecto_final
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            if (DeviceInfo.Current.Platform == DevicePlatform.Android)
            {
                PhoneTabs.IsVisible = true;
                FlyoutBehavior = FlyoutBehavior.Disabled;
            }
            if (DeviceInfo.Current.Platform == DevicePlatform.WinUI)
            {
                PhoneTabs.IsVisible = false;
                FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }
    }
}
