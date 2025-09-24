using Plugin.Maui.PrivacyGlass;

namespace PrivacyGlassSample;

public partial class MainPage : ContentPage
{
	private readonly IPrivacyGlass _privacyGlass;

	private bool _isBankAccountDetailsProtected = false;

	public MainPage(IPrivacyGlass privacyGlass)
	{
		InitializeComponent();

		_privacyGlass = privacyGlass;
	}

	private void ShowHideSensitiveInformation_Clicked(object? sender, EventArgs e)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			privacyGlassView.IsProtected = !privacyGlassView.IsProtected;
		});
	}

	private void ShowHideAll_Clicked(object? sender, EventArgs e)
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			_privacyGlass.TogglePrivacyGlass();
		});
	}

	private void ToogleBankAccountDetails_Tapped(object? sender, EventArgs e)
	{
		_isBankAccountDetailsProtected = !_isBankAccountDetailsProtected;

		ToogleBankAccountDetailsLabel.Text =  _isBankAccountDetailsProtected
			? "Show"
			: "Hide";

		ToogleBankAccountDetailsImage.Source = _isBankAccountDetailsProtected
			? ImageSource.FromFile("ic_eye")
			: ImageSource.FromFile("ic_eye_off");

		BalancePrivacyGlassView.IsProtected = _isBankAccountDetailsProtected;
		SavingsPrivacyGlassView.IsProtected = _isBankAccountDetailsProtected;
	}
}