using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulatrixRepo.Library
{
    public class FormulatrixRepo<T>
    {
    public static void Register( string itemName, T itemContent, int itemType )
    {
      if( itemType != 1 && itemType != 2 )
        throw new Exception( "Item type 1 for JSON and 2 for XML! You cant pick other." );

      if( !RepoCommon.FileNameIsValid( itemName ) )
        throw new Exception( "Item name \"" + itemName + "\" is invalid name." );

      if( GetType( itemName ) > 0 )
        throw new Exception( "Item name already exists." );

      if( itemType == 1 && !RepoCommon.JsonIsValid( itemContent.ToString() ) )
        throw new Exception( "JSON content is not valid!" );
      else if( itemType == 2 && !RepoCommon.XmlIsValid( itemContent.ToString() ) )
        throw new Exception( "XML content is not valid!" );

      string fileExt;

      if( itemType == 1 )
        fileExt = ".json";
      else if( itemType == 2 )
        fileExt = ".xml";
      else
        throw new Exception( "ItemType is only 1 or 2" );
      
      RepoCommon.SaveToFile( itemName + fileExt, itemContent.ToString() );
    }

    public static T Retrieve<U>( string itemName )
    {
      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        throw new Exception( "Invalid item name!" );
        // return (T)Convert.ChangeType( null, typeof( T ) );
      }

      int itemType = GetType( itemName );

      if( itemType == 0 )
      {
        throw new Exception( "Content doesn't exist!" );
        // return (T)Convert.ChangeType( null, typeof( T ) );
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
        // rethrow only :)
        throw;
      }
    }

    public static int GetType( string itemName )
    {
      if( !RepoCommon.FileNameIsValid( itemName ) )
      {
        throw new Exception( "Item name \"" + itemName + "\" is invalid name." );
        // return -1 if item name is invalid
        // return -1;
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
        throw new Exception( "Item name \"" + itemName + "\" is invalid name." );

      int itemType = GetType( itemName );

      if( itemType == 0 )
        throw new Exception( "Content doesn't exist." );

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
        // rethrow only :)
        throw;
      }
    }
    private void Initialize() { }
  }
}
