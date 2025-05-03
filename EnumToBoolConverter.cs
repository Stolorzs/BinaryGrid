using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BinaryGrid
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 关键：即使用户点击已选中的项，也强制返回当前枚举值，阻止取消选中
            return (bool)value
                ? Enum.Parse(targetType, parameter.ToString())
                : Binding.DoNothing; // 保持原值
        }
    }
}
