﻿using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Mopups.Services;
using Realms;

namespace AppListaDeCompras.ViewModels
{
	public partial class ListToBuyViewModel : ObservableObject
	{
		[ObservableProperty]
		public IQueryable<ListToBuy> _listsOflistToBuy;

		public IQueryable<ListToBuy> _backupListsOflistToBuy;

		public ListToBuyViewModel()
		{			
		}

		[RelayCommand]
		private async Task OnAppearing()
		{
			await MongoDBAtlasService.Init();
			await MongoDBAtlasService.LoginAsync();

			var realm = MongoDBAtlasService.GetMainThreadRealm();

			if(UserLoggedManager.ExistsUser())
			{
				ListsOflistToBuy = realm.All<ListToBuy>()
					.Filter("ANY Users.Id == $0", UserLoggedManager.GetUser().Id);
			}
			else
			{
				var anonymousId = new ObjectId(MongoDBAtlasService.CurrentUser.Id);
				ListsOflistToBuy = realm.All<ListToBuy>()
					.Where(a => a.AnonymousUserId == anonymousId);
			}
			_backupListsOflistToBuy = ListsOflistToBuy;
		}

		[RelayCommand]
		private void Search(Entry entry)
		{
			string word = string.IsNullOrWhiteSpace(entry.Text) ? "" : entry.Text;

			ListsOflistToBuy = _backupListsOflistToBuy.Where(a => a.Name.Contains(word, StringComparison.OrdinalIgnoreCase));
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

		[RelayCommand]
		private async Task DeleteList(ListToBuy listSelected)
		{
			var resposta = await App.Current.MainPage.DisplayAlert("Excluir Lista", $"Tem certeza que deseja excluir a lista '{listSelected.Name}'?", "Sim", "Não");

			if(resposta)
			{
				var realm = MongoDBAtlasService.GetMainThreadRealm();
				await realm.WriteAsync(() =>
				{
					realm.Remove(listSelected);
				});
			}
		}
	}
}
