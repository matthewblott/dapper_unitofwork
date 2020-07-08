namespace dapper_unitofwork.Repositories.Implementations
{
  using System.Data;
  using Entities;
  using Interfaces;

  public class OrderRepository : RepositoryBase<Order>, IOrderRepository
  {
    public OrderRepository(IDbTransaction transaction) : base(transaction) { }
  }
}