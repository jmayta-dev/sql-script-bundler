using SSB.Shared.Attributes;
using System.Reflection;

namespace SSB.Shared.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the string value for a given Enum field value which has assigned
        /// the StringValue attribute.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetStringValue(this Enum value)
        {
            string? @string = null;

            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo? fieldInfo = type.GetField(value.ToString());

            if (fieldInfo != null)
            {
                // Get the StringValue attributes
                object[]? customAttributes = fieldInfo.GetCustomAttributes(
                    attributeType: typeof(StringValueAttribute),
                    inherit: false);

                // Return the first if there was a match
                if (customAttributes is StringValueAttribute[] attributes)
                    @string = attributes.Length > 0 ? attributes[0].StringValue : null;
            }
            return @string;
        }
    }
}
