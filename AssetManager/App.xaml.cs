namespace AssetManager;
using AssetManager.Classes;
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		logout();
		MainPage = new MainPage();
	}
	public async void logout()
	{
		CustomAuthenticationProvider AuthStateProvider = new CustomAuthenticationProvider();
		await AuthStateProvider.Logout();
	}
}

