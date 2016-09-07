using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

class BreedRepository : RepositoryBase, IBreedRepository
{
  public BreedRepository(IDbTransaction transaction) : base(transaction) { }

  public IEnumerable<Breed> All()
  {
    return Connection.Query<Breed>("select * from Breeds", transaction: Transaction).ToList();
  }

  public Breed Find(int id)
  {
    return Connection.Query<Breed>(
      "select * from Breeds where BreedId = @breedId ",
      new { BreedId = id },
      Transaction
    ).FirstOrDefault();

  }

  public void Add(Breed entity)
  {
    entity.BreedId = Connection.ExecuteScalar<int>(
      "insert into Breeds(Name) values (@name); select scope_identity() ",
      new { entity.Name },
      Transaction
    );
  }

  public void Update(Breed entity)
  {
    Connection.Execute(
      "update Breeds set Name = @name where BreedId = @breedId ",
      new { entity.Name, entity.BreedId },
      Transaction
    );

  }

  public void Delete(int id)
  {
    Connection.Execute("delete from Breeds where BreedId = @breedId ", new { BreedId = id }, Transaction);
  }

  public void Delete(Breed entity)
  {
    Delete(entity.BreedId);
  }

  public Breed FindByName(string name)
  {
    return Connection.Query<Breed>(
      "select * from Breeds where Name = @name ",
      new { Name = name },
      Transaction
    ).FirstOrDefault();

  }

}