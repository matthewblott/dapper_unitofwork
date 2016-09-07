using System.Collections.Generic;

public interface IBreedRepository
{
  void Add(Breed entity);
  IEnumerable<Breed> All();
  void Delete(int id);
  void Delete(Breed entity);
  Breed Find(int id);
  Breed FindByName(string name);
  void Update(Breed entity);
}