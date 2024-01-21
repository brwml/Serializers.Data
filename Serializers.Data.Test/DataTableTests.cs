namespace Serializers.Data.Test;

using System.Collections.Generic;
using System.Data;
using System.Linq;

using Bogus;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class When_RowStringFieldExistsInObject
{
    private const string ColumnName = nameof(TestClass.Name);
    private readonly IList<TestClass> instance;
    private readonly string value1;
    private readonly string value2;

    public When_RowStringFieldExistsInObject()
    {
        var faker = new Faker();
        this.value1 = faker.Random.String(50);
        this.value2 = faker.Random.String(50);

        var table = new DataTable();
        table.Columns.Add(ColumnName, typeof(string));

        AddRow(table, this.value1);
        AddRow(table, this.value2);

        this.instance = table.Deserialize<TestClass>().ToList();
    }

    [TestMethod]
    public void Then_AllRowsAreDeserialized()
    {
        Assert.AreEqual(2, this.instance.Count);
    }

    [TestMethod]
    public void Then_FirstPropertyIsSet()
    {
        Assert.AreEqual(this.value1, this.instance[0].Name);
    }

    [TestMethod]
    public void Then_SecondPropertyIsSet()
    {
        Assert.AreEqual(this.value2, this.instance[1].Name);
    }

    private static void AddRow(DataTable table, string value)
    {
        var row = table.NewRow();
        row[ColumnName.ToLowerInvariant()] = value;
        table.Rows.Add(row);
    }

    private class TestClass
    {
        public string Name
        {
            get; set;
        }
    }
}