using System.Globalization;

namespace MauiUiChallenge2025_SkiaSharpSlider.Converters;

public class ColorToHexTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Color color)
            return value;
        
        return color.ToHex();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}