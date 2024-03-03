using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MongoDB.Bson;

namespace AppListaDeCompras.ViewModels
{
	public partial class ProfilePageViewModel : ObservableObject
	{
		[ObservableProperty]
		public User user;

		[ObservableProperty]
		private bool isLogged;

		[ObservableProperty]
		private string textUserLogged;

		public ProfilePageViewModel()
		{
			user = new User();
			GetLoggedUserMessage();

			if (!WeakReferenceMessenger.Default.IsRegistered<string>("Logado"))
			{
				WeakReferenceMessenger.Default.Register("Logado", (object obj, string str) =>
				{
					GetLoggedUserMessage();
				});
			}
		}

		private void GetLoggedUserMessage()
		{
			IsLogged = UserLoggedManager.ExistsUser();
			if (IsLogged)
			{
				var user = UserLoggedManager.GetUser();
				TextUserLogged = $"Usuário logado! {user.Name} {user.Email}";
			}
		}

		[RelayCommand]
		private async Task NavigateToAccessCodePage()
		{
			var realm = MongoDBAtlasService.GetMainThreadRealm();
			var userDb = realm.All<User>().FirstOrDefault(a => a.Email == User.Email.Trim().ToLower());

			if (userDb == null)
			{
				await realm.WriteAsync(() =>
				{
					User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
					User.AccessCodeTempCreatedAt = DateTime.UtcNow;
					User.CreatedAt = DateTime.UtcNow;
					User.Id = ObjectId.GenerateNewId();
					realm.Add(User);
				});

				Libraries.Utilities.Email.SendEmailWithAccessCode(User);
			}
			else
			{
				await realm.WriteAsync(() =>
				{
					User.Id = userDb.Id;
					User.CreatedAt = DateTime.UtcNow;
					User.Name = userDb.Name;
					User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
					User.AccessCodeTempCreatedAt = DateTime.UtcNow;
					realm.Add(User, update: true);
				});

				Libraries.Utilities.Email.SendEmailWithAccessCode(User);
			}

			var parameters = new Dictionary<string, object>();
			parameters.Add("usuario", User);
			await Shell.Current.GoToAsync("//Profile/AccessCode", parameters);
		}

		[RelayCommand]
		private async Task Logout()
		{
			UserLoggedManager.RemoveUser();
			User = new User();
			IsLogged = false;
		}
	}
}
