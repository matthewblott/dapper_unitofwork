using System.Collections.Generic;

public interface ICatRepository
{
  void Add(Cat entity);
  IEnumerable<Cat> All();
  Cat Find(int id);
  IEnumerable<Cat> FindByBreedId(int breedId);
  void Remove(int id);
  void Remove(Cat entity);
  void Update(Cat entity);
}