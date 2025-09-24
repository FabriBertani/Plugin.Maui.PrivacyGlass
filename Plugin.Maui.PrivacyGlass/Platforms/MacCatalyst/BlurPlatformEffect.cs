using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace Plugin.Maui.PrivacyGlass.Effects;

internal class BlurPlatformEffect : PlatformEffect
{
    protected UIVisualEffectView? _blurView;

    protected override void OnAttached()
    {
        var platformView = Control;

        platformView.BackgroundColor = UIColor.Clear;

        _blurView = new()
        {
            TranslatesAutoresizingMaskIntoConstraints = false,
            UserInteractionEnabled = false,
            Effect = Application.Current?.RequestedTheme == AppTheme.Dark
                ? UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark)
                : UIBlurEffect.FromStyle(UIBlurEffectStyle.Light)
        };

        platformView.InsertSubview(_blurView, 0);

        NSLayoutConstraint.ActivateConstraints(
        [
            _blurView.TopAnchor.ConstraintEqualTo(platformView.TopAnchor),
            _blurView.LeadingAnchor.ConstraintEqualTo(platformView.LeadingAnchor),
            _blurView.HeightAnchor.ConstraintEqualTo(platformView.HeightAnchor),
            _blurView.WidthAnchor.ConstraintEqualTo(platformView.WidthAnchor)
        ]);
    }

    protected override void OnDetached()
    {
        Control.Subviews.FirstOrDefault(sv => sv is UIVisualEffectView)?.RemoveFromSuperview();
    }
}