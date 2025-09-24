using Plugin.Maui.PrivacyGlass.Effects;

namespace Plugin.Maui.PrivacyGlass;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UsePrivacyGlass(this MauiAppBuilder builder)
    {
        builder.ConfigureEffects(effects =>
        {
            effects.Add<BlurEffect, BlurPlatformEffect>();
        });

        builder.Services.AddSingleton<IPrivacyGlass>(PrivacyGlass.Default);

        return builder;
    }
}