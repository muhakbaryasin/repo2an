using System;
using System.Collections.Generic;

namespace FormulatrixRepoLibrary
{
  public class FormulatrixRepo
  {
    // IDictionary<string, RepoWrapper> RepoCollection = new Dictionary<string, RepoWrapper>();
    IDictionary<string, RepoWrapper> RepoCollection = new Dictionary<string, RepoWrapper>();
    // public void Register( string itemName, T data )
    public void Register( string itemName, object Object )
    {
      if( !IsItemNameValid( itemName ) )
        throw new Exception( $"{itemName} is an invalid item name." );

      // var objA = new sfdsf( asdfdsf );

      RepoCollection.Add( itemName, new RepoWrapper( Object ) );
    }

    public object Retrieve( string itemName )
    {
      if( !IsItemNameValid( itemName ) )
        throw new Exception( $"{itemName} is an invalid item name." );

      return RepoCollection[itemName].Object();
    }

    public Type GetType( string itemName )
    {
      if( !IsItemNameValid( itemName ) )
        throw new Exception( $"{itemName} is an invalid item name." );

      return RepoCollection[itemName].Type();
    }

    public void Deregister( string itemName )
    {
      if( !IsItemNameValid( itemName ) )
        throw new Exception( $"{itemName} is an invalid item name." );

      RepoCollection.Remove( itemName );
    }

    private bool IsItemNameValid( string itemName )
    {
      string namePattern = @"^[\w\- ]+$";
      return System.Text.RegularExpressions.Regex.IsMatch( itemName, namePattern );
    }
    private void Initialize() { }
  }

  interface IData
  {
    void Set( object Object );
    object Object();
    Type Type();
  }

  public class RepoWrapper : IData
  {
    object _Object;

    public RepoWrapper( object Object )
    {
      Set( Object );
    }

    public void Set( object Object )
    {
      _Object = Object;
    }

    public object Object()
    {
      return _Object;
    }

    public Type Type()
    {
      return _Object.GetType();
    }
  }
}
