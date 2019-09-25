using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulatrixRepo.Library
{
  class RepoCommon
  {
    public static bool FileNameIsValid( string itemName )
    {
      string fileNamePattern = @"^[\w\- ]+$";
      return System.Text.RegularExpressions.Regex.IsMatch( itemName, fileNamePattern );
    }

    public static bool XmlIsValid( string itemContent )
    {
      return true;
    }

    public static bool JsonIsValid( string itemContent )
    {
      return true;
    }

    public static void SaveToFile( string fileName, string itemContent )
    {
      try
      {
        // writing file stream procedure
        System.IO.File.WriteAllText( @"" + fileName, itemContent );
      }
      catch( Exception )
      {
        // writing file stream failed handling procedure
        throw;
      }
    }

    public static bool FileExists( string fileName )
    {
      return System.IO.File.Exists( fileName );
    }

  }
}
