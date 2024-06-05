using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Manager.Domain.Entities;

namespace Manager.UI.Converters
{
    public class TaskStatusToBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Status status)
            {
                switch (status.TaskStatus)
                {
                    case "Выполнено":
                        return Colors.Green;
                    case "Ожидание":
                        return Colors.Gray;
                    case "Выполняется":
                        return Colors.Yellow;
                }
            }
            return Colors.LightGray;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
