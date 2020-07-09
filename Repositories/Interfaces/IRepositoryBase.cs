namespace dapper_unitofwork.Repositories.Interfaces
{
  using System.Collections.Generic;

  public interface IRepositoryBase<T> where T : class
  {
    IEnumerable<T> All();
    T Find(object id);
    object Add(T entity);
    void Update(T entity);
    void Remove(object id);
  }
}