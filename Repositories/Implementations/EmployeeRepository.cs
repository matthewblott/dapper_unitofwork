namespace dapper_unitofwork.Repositories.Implementations
{
  using System.Data;
  using Entities;
  using Interfaces;

  public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
  {
    public EmployeeRepository(IDbTransaction transaction) : base(transaction) { }
  }

}