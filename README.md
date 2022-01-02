# JollyWrapper
An asynchronous wrapper around the C# MySql.Data library.

# Examples
## Database Connection
```csharp
//Standard Connection
Database.Init("host.server.com", "DatabaseName", "Pa33word123", "UserName");

//Connection with custom flags
Database.Init("host.server.com", "DatabaseName", "Pa33word123", "UserName", "SSLMode=None");

//Connection with connection string
Database.ConnectionString = "";

//Checking if database can be connected to.
Console.WriteLine(await Database.CheckConnection());
```

## Non-reply Query
```csharp
int userID = 1;
string firstName = "John";
await Database.ExecuteNonQuery("UPDATE `users` SET `firstName` = @val WHERE `users`.`userID` = @val", firstName, userID);
```

## Single Value Query
```csharp
string name = await Database.ExecuteScalerQuery("SELECT `firstName` FROM `users` WHERE `age` > @val LIMIT 1", 18);
Console.WriteLine(name);
```

## Standard Query
```csharp
QueryData users = await Database.ExecuteQuery("SELECT * FROM `users`");
foreach (var user in users)
{
    Console.WriteLine(user["firstName"] + " is " + user["age"] + " years old.");
}
```

## Stored Procedure
```csharp
foreach (var user in await Database.ExecuteProcedure("GetScores"))
{
    Console.WriteLine(user["name"] + " " + user["score"]);
}
```
