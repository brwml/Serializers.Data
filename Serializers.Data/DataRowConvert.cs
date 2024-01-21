namespace Serializers.Data;

using System;
using System.Data;
using System.Linq;

/// <summary>
/// Converts (or deserializes) <see cref="DataRow"/> instances into instances of a given type.
/// </summary>
public static class DataRowConvert
{
    /// <summary>
    /// Deserializes the <paramref name="row"/> described by <paramref name="columns"/> into an
    /// instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The output type</typeparam>
    /// <param name="row">The input data row</param>
    /// <param name="columns">The columns of the input data row</param>
    /// <returns>An instance of type <typeparamref name="T"/></returns>
    /// <exception cref="ArgumentException">
    /// The type of the target property does not match the type of the data reader field.
    /// </exception>
    /// <exception cref="MissingMethodException">
    /// The type <typeparamref name="T"/> does not have a default constructor.
    /// </exception>
    public static T Deserialize<T>(this DataRow row, DataColumnCollection columns)
    {
        return row.CopyProperties(columns, Activator.CreateInstance<T>());
    }

    /// <summary>
    /// Copies the fields from <paramref name="row"/> to the properties of
    /// <paramref name="instance"/> using <paramref name="columns"/> for mappings.
    /// </summary>
    /// <typeparam name="T">The destination type</typeparam>
    /// <param name="row">The input data row</param>
    /// <param name="columns">The columns of the input data row</param>
    /// <param name="instance">The destination instance</param>
    /// <returns>An instance of type <typeparamref name="T"/></returns>
    private static T CopyProperties<T>(this DataRow row, DataColumnCollection columns, T instance)
    {
        var type = instance.GetType();

        foreach (var column in columns.Cast<DataColumn>())
        {
            var property = type.GetPropertyInfo(column.ColumnName);

            if (property is not null)
            {
                var value = row[column];

                if (value is not DBNull)
                {
                    property.SetValue(instance, value);
                }
            }
        }

        return instance;
    }
}