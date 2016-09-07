using System;

public interface IUnitOfWork : IDisposable
{
  IBreedRepository BreedRepository { get; }
  ICatRepository CatRepository { get; }

  void Commit();
}