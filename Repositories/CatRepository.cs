using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

class CatRepository : RepositoryBase, ICatRepository
{
  public CatRepository(IDbTransaction transaction) : base(transaction) { }

  public IEnumerable<Cat> All()
  {
    return Connection.Query<Cat>("select * from Cats ");
  }

  public Cat Find(int id)
  {
    return Connection.Query<Cat>(
      "select * from Cats where CatId = @CatId ",
      new { CatId = id },
      Transaction
    ).FirstOrDefault();

  }

  public void Add(Cat entity)
  {
    entity.CatId = Connection.ExecuteScalar<int>(
      "insert into Cats(BreedId, Name, Age) values (@BreedId, @name, @age); select scope_identity() ",
      new { entity.BreedId, entity.Name, entity.Age },
      Transaction
    );

  }

  public void Update(Cat entity)
  {
    Connection.Execute(
      "update Cats set BreedId = @breedId, Name = @name, Age = @age where CatId = @CatId ",
      new { entity.CatId, entity.BreedId, entity.Name, entity.Age },
      Transaction
    );

  }

  public void Remove(Cat entity)
  {
    Remove(entity.CatId);
  }

  public void Remove(int id)
  {
    Connection.Execute("delete from Cats where CatId = @CatId ", new { CatId = id }, Transaction);
  }

  public IEnumerable<Cat> FindByBreedId(int breedId)
  {
    return Connection.Query<Cat>("select * from Cats where BreedId = @breedId ", new { BreedId = breedId }, Transaction);
  }

}