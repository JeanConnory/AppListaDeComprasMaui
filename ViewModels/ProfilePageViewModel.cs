using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AppListaDeCompras.ViewModels
{
	public partial class ProfilePageViewModel : ObservableObject
	{
		[ObservableProperty]
        public User user;

        public ProfilePageViewModel()
        {
            user = new User();            
        }

        [RelayCommand]
        private async Task NavigateToAccessCodePage()
        {
            var realm = MongoDBAtlasService.GetMainThreadRealm();
            var userDb = realm.All<User>().FirstOrDefault(a => a.Email == User.Email.Trim().ToLower());

			User.AccessCodeTemp = Text.GerarNumeroAleatorio().ToString();
			User.AccessCodeTempCreatedAt = DateTime.UtcNow;

			if (userDb == null)
            {
                await realm.WriteAsync(() =>
                {
                    realm.Add(User);
                });

                Libraries.Utilities.Email.SendEmailWithAccessCode(User);
            }
            else
            {
				await realm.WriteAsync(() =>
				{
					realm.Add(User, update: true);
				});

				Libraries.Utilities.Email.SendEmailWithAccessCode(User);
			}

            var parameters = new Dictionary<string, object>();
            parameters.Add("usuario", User);
            await Shell.Current.GoToAsync("//Profile/AccessCode", parameters);
        }
    }
}
