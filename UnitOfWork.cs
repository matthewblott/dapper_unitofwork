using System;
using System.Data;
using System.Data.SqlClient;
using static System.Configuration.ConfigurationManager;

public class UnitOfWork : IUnitOfWork
{
  IDbConnection connection;
  IDbTransaction transaction;
  IBreedRepository breedRepository;
  ICatRepository catRepository;
  bool disposed;

  public UnitOfWork(string connectionStringName)
  {
    var connStr = ConnectionStrings[connectionStringName].ConnectionString;

    connection = new SqlConnection(connStr);
    connection.Open();
    transaction = connection.BeginTransaction();
  }

  public IBreedRepository BreedRepository
  {
    get { return breedRepository ?? (breedRepository = new BreedRepository(transaction)); }
  }

  public ICatRepository CatRepository
  {
    get { return catRepository ?? (catRepository = new CatRepository(transaction)); }
  }

  public void Commit()
  {
    try
    {
      transaction.Commit();
    }
    catch
    {
      transaction.Rollback();
      throw;
    }
    finally
    {
      transaction.Dispose();
      transaction = connection.BeginTransaction();
      resetRepositories();
    }

  }

  private void resetRepositories()
  {
    breedRepository = null;
    catRepository = null;
  }

  public void Dispose()
  {
    dispose(true);
    GC.SuppressFinalize(this);
  }

  void dispose(bool disposing)
  {
    if (!disposed)
    {
      if (disposing)
      {
        if (transaction != null)
        {
          transaction.Dispose();
          transaction = null;
        }
        if (connection != null)
        {
          connection.Dispose();
          connection = null;
        }
      }
      disposed = true;
    }

  }

  ~UnitOfWork()
  {
    dispose(false);
  }

}