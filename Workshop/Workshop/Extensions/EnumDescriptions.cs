using System.ComponentModel;

namespace Workshop.Extensions
{
    public static class EnumDescriptions
    {
        public static string EnumDescription<T>(this T source)
        {
            var fieldInfo = source.GetType().GetField(source.ToString() ?? string.Empty);

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}