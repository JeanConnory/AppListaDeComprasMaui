﻿using AppListaDeCompras.Libraries.Services;
using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups
{
	public partial class ListOfItensAddItemPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private Product product;

		[ObservableProperty]
		private string[] unitMeasure;

		[ObservableProperty]
		private ListToBuy _list;

        public ListOfItensAddItemPageViewModel()
        {
			unitMeasure = Enum.GetNames(typeof(UnitMeasure));
			Product = new Product();
        }

        [RelayCommand]
		private void Close()
		{
			MopupService.Instance.PopAsync();
		}

		[RelayCommand]
		private async Task Save()
		{
			var realm = MongoDBAtlasService.GetMainThreadRealm();

			await realm.WriteAsync(() =>
			{
				if(Product.Id == default(ObjectId))
				{
					Product.Id = ObjectId.GenerateNewId();
					Product.CreatedAt = DateTime.UtcNow;
					List.Products.Add(Product);
					realm.Add(List, update: true);
				}
				else
				{
					//TODO - Fazer o Update
					realm.Add(List, update: true);
				}
			});

			await MopupService.Instance.PopAsync();
		}
	}
}
