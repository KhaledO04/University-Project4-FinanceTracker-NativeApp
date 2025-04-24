using System.Globalization;

namespace MinApp.Converters
{
    public class IncomeToProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double income && parameter is double allowedIncome && allowedIncome > 0)
            {
                return Math.Min(income / allowedIncome, 1.0);
            }

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}