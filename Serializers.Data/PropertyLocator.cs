using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Serializers.Data
{
    /// <summary>
    /// Provides a method to get a property that matches a given description.
    /// </summary>
    public static class PropertyLocator
    {
        /// <summary>
        /// Gets a <see cref="PropertyInfo"/> from <paramref name="type"/> with a given <paramref name="name"/>.
        /// </summary>
        /// <param name="type">The type to search for the property</param>
        /// <param name="name">The name description of the property to search for</param>
        /// <returns>The <see cref="PropertyInfo"/> instance if it exists, otherwise null</returns>
        public static PropertyInfo GetPropertyInfo(this Type type, string name)
        {
            return type.GetProperties().FirstOrDefault(x => IsMatch(x, name));
        }

        /// <summary>
        /// Determines if the candidate <paramref name="property"/> has a
        /// <see cref="DataMemberAttribute"/> with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="property">The property to evaluate</param>
        /// <param name="name">The name to evaluate</param>
        /// <returns><c>true</c> if the property has the specified attribute, otherwise <c>false</c></returns>
        private static bool HasDataMemberAttribute(PropertyInfo property, string name)
        {
            return property.GetCustomAttribute<DataMemberAttribute>(true)?.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        /// <summary>
        /// Determines if the candidate <paramref name="property"/> has an <see cref="IgnoreDataMemberAttribute"/>.
        /// </summary>
        /// <param name="property">The property to evaluate</param>
        /// <returns><c>true</c> if the property has the specified attribute, otherwise <c>false</c></returns>
        private static bool HasIgnoreDataMemberAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<IgnoreDataMemberAttribute>(true) != null;
        }

        /// <summary>
        /// Determines if the candidate <paramref name="property"/> is a match to the <paramref name="name"/>.
        /// </summary>
        /// <param name="property">The property to examine</param>
        /// <param name="name">The name description to evaluate</param>
        /// <returns><c>true</c> if the property matches the description, otherwise <c>false</c></returns>
        private static bool IsMatch(PropertyInfo property, string name)
        {
            return (property.Name.Equals(name, StringComparison.OrdinalIgnoreCase) || HasDataMemberAttribute(property, name))
                && !HasIgnoreDataMemberAttribute(property);
        }
    }
}