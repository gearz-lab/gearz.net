using System;
using Gearz.Core.Metadata;

namespace Gearz.Core.Helpers
{
    /// <summary>
    /// Exposes methods to deal with metadata
    /// </summary>
    public static class MetadataHelper
    {
        /// <summary>
        /// Returns a property type based on a .NET type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyType GetPropertyTypeFromType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            if (type == typeof(String))
                return PropertyType.String;
            else if (type == typeof(int))
                return PropertyType.Int;
            else if (type == typeof(float))
                return PropertyType.Float;
            else if (type == typeof(bool))
                return PropertyType.Boolean;
            else if (type == typeof(DateTime))
                return PropertyType.DateTime;
            else throw new NotSupportedException();
        }
    }
}