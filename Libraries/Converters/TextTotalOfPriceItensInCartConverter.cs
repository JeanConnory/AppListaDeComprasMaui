using AppListaDeCompras.Models;
using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters
{
	public class TextTotalOfPriceItensInCartConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ListToBuy listToBuy = value as ListToBuy;

			if (listToBuy is null)
				return "R$ 0,00";

			if (listToBuy.Products.Count == 0)
				return "R$ 0,00";

			decimal totalPrice = 0;

			foreach (var product in listToBuy.Products)
			{
				if (product.HasCaught)
					totalPrice += product.Price * product.Quantity;
			}

			return totalPrice.ToString("C");
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
