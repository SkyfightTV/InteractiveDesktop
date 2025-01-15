using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace Interactive_Desktop.Converters
{
    public class BoolToImageSourceConverter : IValueConverter
    {
        public ImageSource? TrueImage { get; set; }
        public ImageSource? FalseImage { get; set; }

        public object? Convert(object? value, Type targetType, object? parameter, string language)
        {
            return value is true ? TrueImage : FalseImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
