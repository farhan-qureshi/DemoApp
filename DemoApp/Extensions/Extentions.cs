using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace DemoApp.Extensions
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (!(e is System.Enum)) return string.Empty;

            if (e.GetAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
            {
                return descriptionAttribute.Description;
            }

            return string.Empty;
        }


        public static string GetDisplayText<T>(this T e) where T : IConvertible
        {
            if (!(e is System.Enum)) return string.Empty;

            if (e.GetAttribute(typeof(DisplayAttribute)) is DisplayAttribute attribute)
            {
                return attribute.Name;
            }

            return string.Empty;
        }


        private static Attribute GetAttribute<T>(this T e, Type attribute) where T : IConvertible
        {
            if (!(e is System.Enum)) return null;

            var type = e.GetType();

            var values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture) && !string.IsNullOrEmpty(type.GetEnumName(val)))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));

                    return (Attribute)memInfo[0]
                        .GetCustomAttributes(attribute, false)
                        .FirstOrDefault();
                }
            }

            return null;
        }

        public static object ParseToEnum<T>(this string value)
        {
            var hasValue = System.Enum.TryParse(typeof(T), value, true, out var enumValue);

            return hasValue ? enumValue : null;
        }
    }
}
