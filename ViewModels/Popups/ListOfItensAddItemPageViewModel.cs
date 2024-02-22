using AppListaDeCompras.Models;
using AppListaDeCompras.Models.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;

namespace AppListaDeCompras.ViewModels.Popups
{
	public partial class ListOfItensAddItemPageViewModel : ObservableObject
	{
		[ObservableProperty]
		private Product product;

		[ObservableProperty]
		private string[] unitMeasure;

        public ListOfItensAddItemPageViewModel()
        {
			unitMeasure = Enum.GetNames(typeof(UnitMeasure));
        }

        [RelayCommand]
		private void Close()
		{
			MopupService.Instance.PopAsync();
		}

		[RelayCommand]
		private void Save()
		{

		}
	}
}
