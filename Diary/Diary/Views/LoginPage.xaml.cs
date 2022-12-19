using System;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials;

namespace Diary.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();

		}

        private async void Login_Clicked(object sender, EventArgs e)
		{
			// проверка, доступен ли вход по отпечатку пальца
			bool isFingerPrintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
			if (!isFingerPrintAvailable)
			{
				await DisplayAlert("Внимание","Вход по отпечатку недоступен на этом устройстве.", "Ок");
				return;
			}

			AuthenticationRequestConfiguration configuration = new AuthenticationRequestConfiguration("Authentification", "Authentificate access for login");
			var result = await CrossFingerprint.Current.AuthenticateAsync(configuration);
			if (result.Authenticated)
			{
				Preferences.Set("is_authorized", "true"); // флаг, который не дает зайти в записи без авторизации
                await Shell.Current.GoToAsync("NotesPage");

            }
			else
			{
				await DisplayAlert("Ошибка", "Вход не выполнен. Попробуйте снова", "Ок");
			}

		}
	}
}