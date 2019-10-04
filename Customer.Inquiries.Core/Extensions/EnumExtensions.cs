using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Customer.Inquiries.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T value) where T : struct, IConvertible
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
