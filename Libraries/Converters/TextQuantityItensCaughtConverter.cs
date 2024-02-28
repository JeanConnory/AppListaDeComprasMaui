using AppListaDeCompras.Models;
using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters
{
	public class TextQuantityItensCaughtConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			IList<Product> products = (IList<Product>)value;

			var caughtCount = products.Where(a => a.HasCaught == true).Count();

			return caughtCount > 0 ? $"{caughtCount} itens" : $"{caughtCount} item";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
