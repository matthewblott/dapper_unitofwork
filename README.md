# dapper_unitofwork

Boilerplate for unit of work pattern. No configuration or database setup required - it uses a Sqlite database in the project file so the examples run smoothly.
 
Apart from the tiny snippet of SQL found in the ```CreateInsertSql``` method used for returning the created Id for identity fields ...
```
select last_insert_rowid();
```
... the code is 100 per cent database agnostic.

### Getting Started

It runs as a console app so you can just do the following.

```
git clone https://github.com/matthewblott/dapper_unitofwork
cd dapper_unitofwork
dotnet run
```
You should then see something like the following in your terminal.

```
Customer 2EF20 added successfully
Customer 2EF20 updated successfully
Customer 2EF20 deleted successfully
```

You can set a breakpoint in the ```Main``` method and step through the code which is pretty straightforward.

```c#

...

using var work = new UnitOfWork(new SQLiteConnection(connectionString));

var id = GetRandomText();

// Add
var entity = new Customer 
{
  CustomerId = id,
  CompanyName = GetRandomText(),
  ContactName = GetRandomText(),
};

work.CustomerRepository.Add(entity);
work.Commit();

Console.WriteLine($"Customer {id} added successfully");

...

```

### Change Database Vendor

As mentioned above the code is pretty database agnostic. You might need to change the SQL for returning the seeded identity depending on which vendor you use (I might get round to fixing that so no changes are necessary). Just make sure you pass the appropriate ```IDbConnection``` into the constructor when you instantiate the ```UnitOfWork``` object.

```csharp
public UnitOfWork(IDbConnection connection)
{
  _connection = connection;
  _connection.Open();
  _transaction = _connection.BeginTransaction();
}
```
