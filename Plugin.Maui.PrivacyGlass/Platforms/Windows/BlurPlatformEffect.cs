using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Plugin.Maui.PrivacyGlass.Effects;

internal class BlurPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        var brush = GetBrush();

        if (Control is Control control)
            control.Background = brush;

        if (Control is Panel panel)
            panel.Background = brush;

        if (Control is Microsoft.UI.Xaml.Controls.Border border)
            border.Background = brush;
    }

    protected override void OnDetached()
    {
        if (Control is Control control)
            control.Background = null;

        if (Control is Panel panel)
            panel.Background = null;

        if (Control is Microsoft.UI.Xaml.Controls.Border border)
            border.Background = null;
    }

    protected static AcrylicBrush GetBrush()
    {
        return new AcrylicBrush
        {
            TintColor = Application.Current?.RequestedTheme == AppTheme.Dark
                ? Colors.Black.ToWindowsColor()
                : Colors.White.ToWindowsColor(),
            TintOpacity = 0.4,
            TintLuminosityOpacity = 0.4
        };
    }
}