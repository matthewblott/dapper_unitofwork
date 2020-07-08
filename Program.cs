namespace dapper_unitofwork
{
  using System;
  using Entities;

  internal static class Program
  {
    private static void Main(string[] args)
    {
      static string GetRandomText() => Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
      
      var connectionString = Utilities.GetDatabaseConnectionString();

      using var work = new UnitOfWork(connectionString);

      var id = GetRandomText();
      
      // Add
      var customer = new Customer {
        CustomerId = id,
        CompanyName = GetRandomText(),
        ContactName = GetRandomText(),
      };
      work.CustomerRepository.Add(customer);
      work.Commit();
      Console.WriteLine($"Customer {id} added successfully");

      // Update
      customer.CompanyName = GetRandomText();
      customer.ContactName = GetRandomText();
      work.CustomerRepository.Update(customer);
      work.Commit();
      Console.WriteLine($"Customer {id} updated successfully");

      // Delete
      work.CustomerRepository.Remove(id);
      work.Commit();
      Console.WriteLine($"Customer {id} deleted successfully");
      
    }

  }
  
}