using Android.Graphics;
using Microsoft.Maui.Controls.Platform;

namespace Plugin.Maui.PrivacyGlass.Effects;

internal class BlurPlatformEffect : PlatformEffect
{
    protected override void OnAttached()
    {
        if (Control is null && Container is null)
            return;

        var element = (BlurEffect?)Element.Effects.FirstOrDefault(e => e is BlurEffect);

        if (element is null)
            return;

        float radius = 40f;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(31))
            {
                if (Control is Android.Views.View view)
                {
                    view.SetRenderEffect(RenderEffect.CreateBlurEffect(radius, radius, Shader.TileMode.Decal!));
                }
                else if (Container is Android.Views.ViewGroup container)
                {
                    container.SetRenderEffect(RenderEffect.CreateBlurEffect(radius, radius, Shader.TileMode.Decal!));
                }
            }
        });
    }

    protected override void OnDetached()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(31))
        {
            if (Control is Android.Views.View view)
                    view.SetRenderEffect(null);
            else if (Container is Android.Views.ViewGroup container)
                container.SetRenderEffect(null);
        }
    }
}