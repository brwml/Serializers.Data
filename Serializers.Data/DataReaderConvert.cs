using System;
using System.Collections.Generic;
using System.Data;

namespace Serializers.Data
{
    /// <summary>
    /// Converts (or deserializes) <see cref="IDataReader"/> instances into instances of a given type.
    /// </summary>
    public static class DataReaderConvert
    {
        /// <summary>
        /// Deserializes the current record of <paramref name="reader"/> into an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The output type</typeparam>
        /// <param name="reader">The input data reader</param>
        /// <returns>The instance of type <typeparamref name="T"/></returns>
        /// <exception cref="ArgumentException">
        /// The type of the target property does not match the type of the data reader field.
        /// </exception>
        /// <exception cref="MissingMethodException">
        /// The type <typeparamref name="T"/> does not have a default constructor.
        /// </exception>
        public static T Deserialize<T>(this IDataReader reader)
        {
            return reader.CopyProperties(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// Deserializes all records of <paramref name="reader"/> into an instance of <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The item output type</typeparam>
        /// <param name="reader">The input data reader</param>
        /// <returns>The instance of <see cref="IEnumerable{T}"/></returns>
        /// <exception cref="ArgumentException">
        /// The type of the target property does not match the type of the data reader field.
        /// </exception>
        /// <exception cref="MissingMethodException">
        /// The type <typeparamref name="T"/> does not have a default constructor.
        /// </exception>
        public static IEnumerable<T> DeserializeAll<T>(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader.Deserialize<T>();
            }
        }

        /// <summary>
        /// Copies the fields from the current record of <paramref name="reader"/> to the properties
        /// of <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="T">The type of the destination instance</typeparam>
        /// <param name="reader">The source data reader</param>
        /// <param name="instance">The destination instance</param>
        /// <returns>An instance of type <typeparamref name="T"/></returns>
        private static T CopyProperties<T>(this IDataReader reader, T instance)
        {
            var length = reader.FieldCount;
            var type = instance.GetType();

            for (var i = 0; i < length; i++)
            {
                var property = type.GetPropertyInfo(reader.GetName(i));

                if (property != null)
                {
                    var value = reader[i];

                    if (!(value is DBNull))
                    {
                        property.SetValue(instance, value);
                    }
                }
            }

            return instance;
        }
    }
}