namespace dapper_unitofwork.Repositories.Implementations
{
  using System.Data;
  using Entities;
  using Interfaces;

  public class ProductRepository : RepositoryBase<Product>, IProductRepository
  {
    public ProductRepository(IDbTransaction transaction) : base(transaction) { }
  }

  public class RegionRepository : RepositoryBase<Region>, IRegionRepository
  {
    public RegionRepository(IDbTransaction transaction) : base(transaction) { }
  }
  
  public class ShipperRepository : RepositoryBase<Shipper>, IShipperRepository
  {
    public ShipperRepository(IDbTransaction transaction) : base(transaction) { }
  }
  
  public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
  {
    public SupplierRepository(IDbTransaction transaction) : base(transaction) { }
  }
  
  public class TerritoryRepository : RepositoryBase<Territory>, ITerritoryRepository
  {
    public TerritoryRepository(IDbTransaction transaction) : base(transaction) { }
  }
  
}