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
        private void NavigateToAccessCodePage()
        {
            Shell.Current.GoToAsync("//Profile/AccessCode");
        }
    }
}
