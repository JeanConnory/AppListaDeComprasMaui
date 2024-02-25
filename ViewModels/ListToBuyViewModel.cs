using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace AppListaDeCompras.ViewModels
{
	public partial class ListToBuyViewModel : ObservableObject
	{
		[ObservableProperty]
		public ObservableCollection<ListToBuy> _listToBuy;

		public ListToBuyViewModel()
		{			
		}

		[RelayCommand]
		private async Task OnAppearing()
		{
			await MongoDBAtlasService.Init();
			await MongoDBAtlasService.LoginAsync();
		}

		[RelayCommand]
		private void OpenPopupSharePage(ListToBuy listSelected)
		{
			MopupService.Instance.PushAsync(new ListToBuySharedPage(listSelected));
		}

		[RelayCommand]
		private void OpenListOfItensPage(ListToBuy listSelected)
		{
			var pageParameter = new Dictionary<string, object>()
			{
				{ "ListToBuy", listSelected }
			};

			Shell.Current.GoToAsync("//ListToBuy/ListOfItens", pageParameter);
		}

		[RelayCommand]
		private void OpenAddListOfItensPage()
		{ 
			Shell.Current.GoToAsync("//ListToBuy/ListOfItens");
		}
	}
}
