namespace Serializers.Data;

using System.Collections.Generic;
using System.Data;
using System.Linq;

/// <summary>
/// Converts (or deserializes) a <see cref="DataTable"/> instance into an instance of <see cref="IEnumerable{T}"/>.
/// </summary>
public static class DataTableConvert
{
    /// <summary>
    /// Deserializes <paramref name="dataTable"/> into an instance of <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The item output type</typeparam>
    /// <param name="dataTable">The data table</param>
    /// <returns>An instance of <see cref="IEnumerable{T}"/></returns>
    public static IEnumerable<T> Deserialize<T>(this DataTable dataTable)
    {
        foreach (var row in dataTable.Rows.Cast<DataRow>())
        {
            yield return row.Deserialize<T>(dataTable.Columns);
        }
    }
}