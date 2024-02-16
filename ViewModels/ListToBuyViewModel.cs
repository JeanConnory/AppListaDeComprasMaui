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
			ListToBuy = new ObservableCollection<ListToBuy>()
			{
				new()
				{
					Name = "Minha Lista",
					Users =
					[
						new User {Name = "Jean Michael", Email = "jean@gmail.com"},
						new User {Name = "Jessica Jamilly", Email = "jessica@gmail.com"}
					],
					Products =
					[
						new Product { Name = "Arroz 5kg", Quantity = 2, Price = 28.99m, HasCaught = true },
						new Product { Name = "Feijão 1kg", Quantity = 1, Price = 7.49m, HasCaught = true},
						new Product { Name = "Leite Condensado", Quantity = 1, Price = 6.29m }
					]
				},
				new()
				{
					Name = "Minha Lista 2",
					Users = new()
					{
						new User {Name = "Elias Ribeiro", Email = "elias@gmail.com"}
					},
					Products = new()
					{
						new Product { Name = "Arroz 5kg", Quantity = 2, Price = 36.99m, HasCaught = true },
						new Product { Name = "Feijão 1kg", Quantity = 1, Price = 8.49m, HasCaught = true},
						new Product { Name = "Leite Condensado", Quantity = 1, Price = 5.29m, HasCaught = true }
					}
				}
			};
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

			Shell.Current.GoToAsync("//ListToBuy/ListOfItens");
		}
	}
}
