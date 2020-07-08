using System;

namespace dapper_unitofwork
{
  using Repositories.Interfaces;

  public interface IUnitOfWork : IDisposable
  {
    ICategoryRepository CategoryRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    IRegionRepository RegionRepository { get; }
    IShipperRepository ShipperRepository { get; }
    ISupplierRepository SupplierRepository { get; }
    ITerritoryRepository TerritoryRepository { get; }
    
    void Commit();
  }
}