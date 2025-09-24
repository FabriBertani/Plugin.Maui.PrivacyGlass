using Plugin.Maui.PrivacyGlass.Controls;

namespace Plugin.Maui.PrivacyGlass;

public class PrivacyGlass
{
    public static IPrivacyGlass? _defaultImplementation;

    public static IPrivacyGlass Default => _defaultImplementation ??= new PrivacyGlassView();

    internal static void SetDefault(IPrivacyGlass? implementation) => _defaultImplementation = implementation;
}