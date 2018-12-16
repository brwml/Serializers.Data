# ADO.NET to .NET Object Serializers

This project takes the common task of copying ADO.NET output fields to C# object properties in a manner similar to serialization libraries such as [Json.NET](https://github.com/JamesNK/Newtonsoft.Json).

## Example

```csharp
using (var connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();

    var command = connection.CreateCommand();
    command.CommandText = "select * from users";

    var users = (await command.ExecuteReaderAsync()).DeserializeAll<User>();

    // Do something with users.
}
```
