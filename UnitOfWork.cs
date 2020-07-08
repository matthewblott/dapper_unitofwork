namespace dapper_unitofwork
{
  using System;
  using System.Data;
  using System.Data.SQLite;
  using Repositories.Implementations;
  using Repositories.Interfaces;

  public class UnitOfWork : IUnitOfWork
  {
    private IDbConnection _connection;
    private IDbTransaction _transaction;
    private ICategoryRepository _categoryRepository;
    private ICustomerRepository _customerRepository;
    private IEmployeeRepository _employeeRepository;
    private IOrderRepository _orderRepository;
    private IProductRepository _productRepository;
    private IRegionRepository _regionRepository;
    private IShipperRepository _shipperRepository;
    private ISupplierRepository _supplierRepository;
    private ITerritoryRepository _territoryRepository;
    
    private bool _disposed;

    public UnitOfWork(string connectionString)
    {
      _connection = new SQLiteConnection(connectionString);
      _connection.Open();
      _transaction = _connection.BeginTransaction();
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_transaction);
    public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_transaction);
    public IEmployeeRepository EmployeeRepository  => _employeeRepository ??= new EmployeeRepository(_transaction);
    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_transaction);
    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_transaction);
    public IRegionRepository RegionRepository => _regionRepository ??= new RegionRepository(_transaction);
    public IShipperRepository ShipperRepository => _shipperRepository ??= new ShipperRepository(_transaction);
    public ISupplierRepository SupplierRepository => _supplierRepository ??= new SupplierRepository(_transaction);
    public ITerritoryRepository TerritoryRepository => _territoryRepository ??= new TerritoryRepository(_transaction);
    
    public void Commit()
    {
      try
      {
        _transaction.Commit();
      }
      catch
      {
        _transaction.Rollback();
        throw;
      }
      finally
      {
        _transaction.Dispose();
        _transaction = _connection.BeginTransaction();
        ResetRepositories();
      }

    }

    private void ResetRepositories()
    {
      _categoryRepository = null;
      _customerRepository = null;
      _employeeRepository = null;
      _orderRepository = null;
      _productRepository = null;
      _regionRepository = null;
      _shipperRepository = null;
      _supplierRepository = null;
      _territoryRepository = null;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }
    
      if (disposing)
      {
        if (_transaction != null)
        {
          _transaction.Dispose();
          _transaction = null;
        }
        if (_connection != null)
        {
          _connection.Dispose();
          _connection = null;
        }
      }
      _disposed = true;

    }

    ~UnitOfWork()
    {
      Dispose(false);
    }

  }
  
}