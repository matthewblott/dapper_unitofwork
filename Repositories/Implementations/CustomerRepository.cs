namespace dapper_unitofwork.Repositories.Implementations
{
  using System.Data;
  using Entities;
  using Interfaces;

  public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
  {
    public CustomerRepository(IDbTransaction transaction) : base(transaction) { }

  }
  
}