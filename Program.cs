using static System.Console;

class Program
{
  static void Main(string[] args)
  {
    Test1();
  }

  static void Test1()
  {
    var breed1 = new Breed { Name = "Egyptian Mau" };
    var breed2 = new Breed { Name = "Arabian Mau" };

    var cat1 = new Cat { Name = "Pharoh", Age = 4 };
    var cat2 = new Cat { Name = "Tut", Age = 2 };
    var cat3 = new Cat { Name = "Anas", Age = 8 };

    using (var work = new UnitOfWork("default"))
    {
      work.BreedRepository.Add(breed1);
      work.BreedRepository.Add(breed2);

      cat1.BreedId = breed1.BreedId;
      cat2.BreedId = breed1.BreedId;
      cat3.BreedId = breed2.BreedId;

      work.CatRepository.Add(cat1);
      work.CatRepository.Add(cat2);
      work.CatRepository.Add(cat3);

      work.Commit();
    }

  }

  static void Test2()
  {
    var breed1 = new Breed { Name = "Orange Mackerel" };
    var cat1 = new Cat { Name = "Cheddar", Age = 4 };

    using (var work = new UnitOfWork("default"))
    {
      work.BreedRepository.Add(breed1);
      cat1.BreedId = breed1.BreedId;
      work.CatRepository.Add(cat1);
      work.Commit();
    }

  }

  static void Test3()
  {
    using (var work = new UnitOfWork("default"))
    {
      var orangeMackerel = work.BreedRepository.FindByName("Orange Mackerel");
      var morris = new Cat { BreedId = orangeMackerel.BreedId, Name = "Morris", Age = 12 };

      work.CatRepository.Add(morris);
      work.Commit();

      var siamese = new Breed { Name = "Siamese" };

      work.BreedRepository.Add(siamese);

      var foo = new Cat { BreedId = siamese.BreedId, Name = "Foo", Age = 19 };
      var xing = new Cat { BreedId = siamese.BreedId, Name = "Xing", Age = 6 };
      var xang = new Cat { BreedId = siamese.BreedId, Name = "Xang", Age = 6 };

      work.CatRepository.Add(foo);
      work.CatRepository.Add(xing);
      work.CatRepository.Add(xang);
      work.Commit();

    }

  }

}