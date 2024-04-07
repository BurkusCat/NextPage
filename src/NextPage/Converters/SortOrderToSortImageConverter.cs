using NextPage.Models.Enums;
using System.Globalization;

namespace NextPage.Converters;

public class SortOrderToSortImageConverter : IValueConverter
{
	public object? Convert(object value, Type? targetType, object? parameter, CultureInfo culture)
	{
		if (value is null || value is not SortOrderEnum item)
		{
			// no sort is selected
			return "sort.png";
		}

		if (item == SortOrderEnum.Ascending)
		{
			return "ascending.png";
		}
		else
        {
            return "descending.png";
        }
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}