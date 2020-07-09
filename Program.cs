namespace dapper_unitofwork
{
  using System;
  using System.Data.SQLite;
  using Entities;

  internal static class Program
  {
    private static void Main(string[] args)
    {
      RunCustomerCrudExample();
      RunCategoryCrudExample();
    }

    private static void RunCustomerCrudExample()
    {
      static string GetRandomText() => Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
      
      var connectionString = Utilities.GetDatabaseConnectionString();

      using var work = new UnitOfWork(new SQLiteConnection(connectionString));

      var id = GetRandomText();
      
      // Add
      var entity = new Customer {
        CustomerId = id,
        CompanyName = GetRandomText(),
        ContactName = GetRandomText(),
      };
      work.CustomerRepository.Add(entity);
      work.Commit();
      Console.WriteLine($"Customer {id} added successfully");

      // Update
      entity.CompanyName = GetRandomText();
      entity.ContactName = GetRandomText();
      work.CustomerRepository.Update(entity);
      work.Commit();
      Console.WriteLine($"Customer {id} updated successfully");

      // Delete
      work.CustomerRepository.Remove(id);
      work.Commit();
      Console.WriteLine($"Customer {id} deleted successfully");

    }

    private static void RunCategoryCrudExample()
    {
      static string GetRandomText() => Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
      
      var connectionString = Utilities.GetDatabaseConnectionString();
    
      using var work = new UnitOfWork(new SQLiteConnection(connectionString));
    
      var entity = new Category
      {
        CategoryName = GetRandomText(),
        Description = GetRandomText(),
      };

      // Add
      var id = work.CategoryRepository.Add(entity);
      work.Commit();
      Console.WriteLine($"Category {id} added successfully");
      
      // Update
      entity.CategoryName = GetRandomText();
      entity.Description = GetRandomText();
      work.CategoryRepository.Update(entity);
      work.Commit();
      Console.WriteLine($"Category {id} updated successfully");

      // Delete
      work.CategoryRepository.Remove(id);
      work.Commit();
      Console.WriteLine($"Category {id} deleted successfully");
      
    }
    
  }
  
}