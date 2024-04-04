using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace planirovshik;

public class TextDecorationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isCompleted = (bool)value;
        if (isCompleted)
            return TextDecorations.Strikethrough;
        else
            return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}