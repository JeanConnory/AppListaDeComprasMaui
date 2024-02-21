using AppListaDeCompras.Models;
using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters
{
	public class TextQuantityItensNoCaughtConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			List<Product> products = (List<Product>)value;

			int notCaughtCount = products.Where(a => a.HasCaught == false).Count();

			return notCaughtCount > 0 ? $"Faltam {notCaughtCount} itens" : $"Falta {notCaughtCount} item";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
