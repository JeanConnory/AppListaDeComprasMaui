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
		private Product _product;

		public Product Product 
		{ 
			get 
			{ 
				return _product; 
			}
			set 
			{ 
				_product = value;
				ProductForm = new Product()
				{
					Id = value.Id,
					HasCaught = value.HasCaught,
					CreatedAt = value.CreatedAt,
					Name = value.Name,
					Price = value.Price,
					Quantity = value.Quantity,
					QuantityUnitMeasure = value.QuantityUnitMeasure
				};
			} 
		}

		[ObservableProperty]
		private Product productForm;

		[ObservableProperty]
		private string[] unitMeasure;

		[ObservableProperty]
		private ListToBuy _list;

        public ListOfItensAddItemPageViewModel()
        {
			unitMeasure = Enum.GetNames(typeof(UnitMeasure));
			Product = new Product();
			ProductForm = new Product();
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
					ProductForm.Id = ObjectId.GenerateNewId();
					ProductForm.CreatedAt = DateTime.UtcNow;
					List.Products.Add(ProductForm);
					realm.Add(List, update: true);
				}
				else
				{
					Product.Name = ProductForm.Name;
					Product.Price = ProductForm.Price;
					Product.Quantity = ProductForm.Quantity;
					Product.QuantityUnitMeasure = ProductForm.QuantityUnitMeasure;
				}
			});

			await MopupService.Instance.PopAsync();
		}
	}
}
