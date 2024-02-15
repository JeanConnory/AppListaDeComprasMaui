using AppListaDeCompras.Models;
using AppListaDeCompras.ViewModels.Popups;
using Mopups.Pages;

namespace AppListaDeCompras.Views.Popups;

public partial class ListToBuySharedPage : PopupPage
{
	public ListToBuySharedPage(ListToBuy list)
	{
		InitializeComponent();

		var vm = (ListToBuySharedPageViewModel)BindingContext;
		vm.List = list;
	}
}