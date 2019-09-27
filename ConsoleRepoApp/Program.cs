using System;
using FormulatrixRepoLibrary;

namespace ConsoleRepoApp
{
  class Program
  {
    static void Main( string[] args )
    {
      FormulatrixRepo fmlxRepo = new FormulatrixRepo();
      int a = 1;
      string b = "bbb";

      fmlxRepo.Register( "a", a );
      fmlxRepo.Register( "b", b );
      A objA = new A() { nama = "myname", id = "1234" };
      fmlxRepo.Register( "objectA", objA );

      // foreach( var f in fmlxRepo.GetType( "objectA" ).GetFields().Where( f => f.IsPublic ) )
      foreach( var f in fmlxRepo.GetType( "objectA" ).GetFields() )
      {
        Console.WriteLine(
            String.Format( "Name: {0} Value: {1}", f.Name, f.GetValue( fmlxRepo.Retrieve( "objectA" ) ) )
        );
      }

      var retObj = Convert.ChangeType( fmlxRepo.Retrieve( "objectA" ), fmlxRepo.GetType( "objectA" ) );
      Console.ReadLine();
    }
  }

  class A
  {
    public string nama;
    public string id;
  }
}
