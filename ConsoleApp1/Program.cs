using System;
using FormulatrixRepo.Library;

namespace ConsoleApp1
{
  class Program
  {
    static void Main(string[] args)
    {
      int argsLength = args.Length;

      if (argsLength != 2 && argsLength != 4)
      {
        Console.WriteLine("Need 2 or 5 args");
        Console.WriteLine("ConsoleApp1.exe [method] [item_name] [input] [item_type]");
        Console.WriteLine("method: register/gettype/retrieve/deregister");
        Console.WriteLine("item_name: string");
        Console.WriteLine("input content/text");
        Console.WriteLine("item_type 1 or 2 (1 for JSON, 2 for XML.");
      }

      if (argsLength == 0)
        return;

      if ( (args[0].Equals("retrieve") || args[0].Equals("gettype") || args[0].Equals("deregister")) && argsLength != 2)
      {
        Console.WriteLine("Need 2 args to {0}", args[0]);
        return;
      } else if (args[0].Equals("register") && argsLength != 4)
      {
          Console.WriteLine("Need 4 args to register.");
          return;
      }

      if (args[0].Equals("retrieve") )
      {
        Console.WriteLine( "Retrieve: {0} -> {1}", args[1], FormulatrixRepo<string>.Retrieve<string>(args[1]) );
      } else if (args[0].Equals("gettype"))
      {
        Console.WriteLine("Gettype: {0} -> {1}", args[1], FormulatrixRepo<string>.GetType(args[1]) );
      } else if (args[0].Equals("deregister"))
      {
        FormulatrixRepo<string>.Deregister(args[1]);
        Console.WriteLine("Deregister: {0} -> Done", args[1]);
      } else if (args[0].Equals("register"))
      {
        int contentType = 0;
        Int32.TryParse(args[3], out contentType);
        FormulatrixRepo<string>.Register(args[1], args[2], contentType);
        Console.WriteLine("Register: {0} -> Done", args[1]);
      } else
      {
        Console.WriteLine("Unknown method of {0}", args[1]);
      }
    }
  }
}
