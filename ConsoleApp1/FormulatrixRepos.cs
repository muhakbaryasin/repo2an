using System;
using System.Text;

namespace ConsoleApp1
{
  class FormulatrixRepos<T>
  {
    public static void Register( string itemName, T itemContent, int itemType )
    {
      if( itemType != 1 && itemType != 2 )
      {
        Console.WriteLine( "Item type 1 for JSON and 2 for XML! You cant pick other." );
        return;
      }

      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        Console.WriteLine( "Item name \"" + itemName + "\" is invalid name." );
        return;
      }


      if( GetType( itemName ) > 0 )
      {
        Console.WriteLine( "Item name already exists." );
        return;
      }

      if( itemType == 1 && !RepoCommon.JsonIsValid( itemContent.ToString() ) )
      {
        Console.WriteLine( "JSON content is not valid!" );
        return;
      }
      else if( itemType == 2 && !RepoCommon.XmlIsValid( itemContent.ToString() ) )
      {
        Console.WriteLine( "XML content is not valid!" );
        return;
      }


      string fileExt;

      if( itemType == 1 )
        fileExt = ".json";
      else if( itemType == 2 )
        fileExt = ".xml";
      else
      {
        Console.WriteLine( "ItemType is only 1 or 2" );
        return;
      }

      RepoCommon.SaveToFile( itemName + fileExt, itemContent.ToString() );
    }

    public static T Retrieve<U>( string itemName )
    {
      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        Console.WriteLine( "Invalid item name!" );
        return (T)Convert.ChangeType( null, typeof( T ) );
      }

      int itemType = GetType( itemName );

      if( itemType == 0 )
      {
        Console.WriteLine( "Content doesn't exist!" );
        return (T)Convert.ChangeType( null, typeof( T ) );
      }

      string fileExt = itemType == 1 ? ".json" : ".xml";
      string fileName = itemName + fileExt;

      try
      {
        // open file stream procedure
        string isi = System.IO.File.ReadAllText( @"" + fileName, Encoding.UTF8 );
        return (T)Convert.ChangeType( isi, typeof( T ) );
      }
      catch( Exception )
      {
        // load file failed handling procedure.
        throw;
      }
    }

    public static int GetType( string itemName )
    {
      // return -1 if item name is invalid
      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        Console.WriteLine( "Item name \"" + itemName + "\" is invalid name." );
        return -1;
      }

      string jsonItem = itemName + ".json", xmlItem = itemName + ".xml";

      if( RepoCommon.FileExists( jsonItem ) )
        return 1;

      if( RepoCommon.FileExists( xmlItem ) )
        return 2;

      // return 0 if item doesn't exist.
      return 0;
    }

    public static void Deregister( string itemName )
    {
      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        Console.WriteLine( "Item name \"" + itemName + "\" is invalid name." );
        return;
      }

      int itemType = GetType( itemName );

      if( itemType == 0 )
      {
        Console.WriteLine( "Content doesn't exist." );
        return;
      }

      string fileExt = itemType == 1 ? ".json" : ".xml";
      string fileName = itemName + fileExt;

      try
      {
        // deleting file procedure
        System.IO.File.Delete( @"" + fileName );
      }
      catch( Exception )
      {
        // deleting file failed handling procedure
        throw;
      }
    }
    private void Initialize() { }
  }
}
