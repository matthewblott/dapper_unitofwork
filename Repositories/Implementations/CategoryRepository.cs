namespace dapper_unitofwork.Repositories.Implementations
{
  using System.Data;
  using Entities;
  using Interfaces;

  public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
  {
    public CategoryRepository(IDbTransaction transaction) : base(transaction) { }
     
  }
  
}