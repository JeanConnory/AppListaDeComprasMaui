using AppListaDeCompras.Models;
using CommunityToolkit.Mvvm.ComponentModel;
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
						new Product {},
                        new Product {},
                        new Product {}
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
						new Product {},
						new Product {},
						new Product {}
					}
				}
            };
        }
    }
}
