using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;

namespace AppListaDeCompras.ViewModels
{
	public partial class ListToBuyViewModel : ObservableObject
	{
		[ObservableProperty]
		public IQueryable<ListToBuy> _listsOflistToBuy;

		public ListToBuyViewModel()
		{			
		}

		[RelayCommand]
		private async Task OnAppearing()
		{
			await MongoDBAtlasService.Init();
			await MongoDBAtlasService.LoginAsync();

			var realm = MongoDBAtlasService.GetMainThreadRealm();
			ListsOflistToBuy = realm.All<ListToBuy>();
		}

		[RelayCommand]
		private void OpenPopupSharePage(ListToBuy listSelected)
		{
			MopupService.Instance.PushAsync(new ListToBuySharedPage(listSelected));
		}

		[RelayCommand]
		private void OpenListOfItensToEdit(ListToBuy listSelected)
		{
			var pageParameter = new Dictionary<string, object>()
			{
				{ "ListToBuy", listSelected }
			};

			Shell.Current.GoToAsync("//ListToBuy/ListOfItens", pageParameter);
		}

		[RelayCommand]
		private void OpenListOfItensToAdd()
		{ 
			Shell.Current.GoToAsync("//ListToBuy/ListOfItens");
		}
	}
}
