using Plugin.Maui.PrivacyGlass.Effects;

namespace Plugin.Maui.PrivacyGlass.Controls;

public partial class PrivacyGlassView : ContentView, IPrivacyGlass
{
	private readonly BlurEffect _blurEffect = new();

#if IOS || MACCATALYST || WINDOWS
	private readonly PrivacyGlassSpecificGrid _container;

	private readonly ContentView _iOSProtectionView;

	private readonly ContentPresenter _content;
#endif

	private static readonly List<PrivacyGlassView> _instances = [];

	public static readonly BindableProperty IsProtectedProperty = BindableProperty.Create(
		nameof(IsProtected),
		typeof(bool),
		typeof(PrivacyGlassView),
		false,
		propertyChanged: OnIsProtectedChanged
	);
	public bool IsProtected
	{
		get => (bool)GetValue(IsProtectedProperty);
		set => SetValue(IsProtectedProperty, value);
	}

	public static readonly BindableProperty KeepScreenOnProperty = BindableProperty.Create(
		nameof(KeepScreenOn),
		typeof(bool),
		typeof(PrivacyGlassView),
		false,
		propertyChanged: OnIsProtectedChanged
	);
	public bool KeepScreenOn
	{
		get => (bool)GetValue(KeepScreenOnProperty);
		set => SetValue(KeepScreenOnProperty, value);
	}

	public PrivacyGlassView()
	{
		_instances.Add(this);

#if IOS || MACCATALYST || WINDOWS
		_container = new()
		{
			RowDefinitions =
			{
				new RowDefinition(),
			}
		};

		_content = new()
		{
			ZIndex = 1
		};

		_iOSProtectionView = new()
		{
			ZIndex = 2
		};
#endif
	}

	protected override void OnChildAdded(Element child)
	{
		if (child is PrivacyGlassSpecificGrid)
			return;

#if IOS || MACCATALYST || WINDOWS
        _container.Add(_content, 0, 0);

		_container.Add(_iOSProtectionView, 0, 0);

		_content.Content = (View)child;

		Content = _container;
#else
        base.OnChildAdded(child);
#endif
	}

	public void TogglePrivacyGlass()
	{
		foreach(var instance in _instances)
		{
			if (!instance.KeepScreenOn)
				instance.IsProtected = !instance.IsProtected;
        }
    }

    private static void OnIsProtectedChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var privacyView = (PrivacyGlassView)bindable;

		privacyView.SetProtection();
	}

	private void SetProtection()
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			if (IsProtected)
			{
#if IOS || MACCATALYST || WINDOWS
                _iOSProtectionView.Effects.Add(_blurEffect);
#else
				this.Effects.Add(_blurEffect);
#endif
			}
			else
			{
#if IOS || MACCATALYST || WINDOWS
                _iOSProtectionView.Effects.Remove(_blurEffect);
#else
				this.Effects.Remove(_blurEffect);
#endif
			}
		});
	}
}