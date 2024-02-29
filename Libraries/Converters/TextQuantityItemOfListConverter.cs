using System.Globalization;

namespace AppListaDeCompras.Libraries.Converters
{
	public class TextQuantityItemOfListConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var productsCount = (int)value;

			return productsCount > 1 ? "Itens" : "Item";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
