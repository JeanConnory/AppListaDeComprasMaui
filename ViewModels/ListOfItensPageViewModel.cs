using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Libraries.Utilities;
using AppListaDeCompras.Models;
using AppListaDeCompras.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MongoDB.Bson;
using Mopups.Services;

namespace AppListaDeCompras.ViewModels
{
	[QueryProperty(nameof(ListToBuy), "ListToBuy")]
	public partial class ListOfItensPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private bool _enableAddProduct;

		private ListToBuy _listToBuy;

		public ListToBuy ListToBuy
		{
			get => _listToBuy;
			set
			{
				ListToBuyName = value.Name;
				SetProperty(ref _listToBuy, value);
				EnableAddProduct = true;
			}
		}

		[ObservableProperty]
		private string _listToBuyName;

		public ListOfItensPageViewModel()
		{
			ListToBuy = new ListToBuy();
			EnableAddProduct = false;

			if (!WeakReferenceMessenger.Default.IsRegistered<string>("NewItem"))
			{
				WeakReferenceMessenger.Default.Register<string>("NewItem", (obj, str) =>
				{
					UpdateListToBuy();
				});
			}
		}

		[RelayCommand]
		private async Task SaveListToBuy()
		{
			if (string.IsNullOrWhiteSpace(ListToBuyName))
			{
				await App.Current.MainPage.DisplayAlert("Nome da lista", "O nome da lista deve ser preenchido", "OK");
				return;
			}

			var realm = MongoDBAtlasService.GetMainThreadRealm();

			await realm.WriteAsync(() =>
			{
				if (ListToBuy.Id == default(ObjectId))
				{
					ListToBuy.Id = ObjectId.GenerateNewId();
					ListToBuy.CreatedAt = DateTime.UtcNow;
					ListToBuy.Name = ListToBuyName;

					if (UserLoggedManager.ExistsUser())
					{
						var user = UserLoggedManager.GetUser();
						var userDb = realm.All<User>().Where(a => a.Id == user.Id).FirstOrDefault();

						if (userDb != null)
						{
							ListToBuy.Users.Add(userDb);
						}
					}
					else
					{
						ListToBuy.AnonymousUserId = new ObjectId(MongoDBAtlasService.CurrentUser.Id);
					}

					realm.Add(ListToBuy);
				}
				else
				{
					ListToBuy.Name = ListToBuyName;
					realm.Add(ListToBuy, update: true);
				}

				EnableAddProduct = true;
			});
		}

		[RelayCommand]
		private void BackPage()
		{
			Shell.Current.GoToAsync("..");
		}

		[RelayCommand]
		private void OpenPopupAddItemPage()
		{
			MopupService.Instance.PushAsync(new ListOfItensAddItemPage(ListToBuy));
		}

		[RelayCommand]
		private void OpenPopupEditItemPage(Product product)
		{
			MopupService.Instance.PushAsync(new ListOfItensAddItemPage(ListToBuy, product));
		}

		[RelayCommand]
		private async Task DeleteItem(Product product)
		{
			var realm = MongoDBAtlasService.GetMainThreadRealm();

			await realm.WriteAsync(() =>
			{
				realm.Remove(product);
			});
		}

		[RelayCommand]
		private void UpdateListToBuy()
		{
			OnPropertyChanged(nameof(ListToBuy));
		}
	}
}
