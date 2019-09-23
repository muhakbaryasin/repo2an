using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if ( (args[0].Equals("retrieve") || args[0].Equals("gettype") || args[0].Equals("deregister")) && argsLength != 2)
            {
                Console.WriteLine("Need 2 args to " + args[0]);
                return;
            } else if (args[0].Equals("register") && argsLength != 4)
            {
                Console.WriteLine("Need 4 args to register.");
                return;
            }

            if (args[0].Equals("retrieve") )
            {
                Console.WriteLine( "Retrieve: " + args[1] + " -> " + FormulatrixRepos<string>.Retrieve(args[1]) );
            } else if (args[0].Equals("gettype"))
            {
                Console.WriteLine("Gettype: " + args[1] + " -> " + FormulatrixRepos<string>.GetType(args[1]));
            } else if (args[0].Equals("deregister"))
            {
                FormulatrixRepos<string>.Deregister(args[1]);
                Console.WriteLine("Deregister: " + args[1] + " -> Done" );
            } else if (args[0].Equals("register"))
            {
                int contentType = 0;
                Int32.TryParse(args[3], out contentType);
                FormulatrixRepos<string>.Register(args[1], args[2], contentType);
                Console.WriteLine("Register: " + args[1] + " -> Done");
            }
        }
    }

    class RepoCommon
    {
        public static bool FileNameIsValid(string itemName)
        {
            string fileNamePattern = @"^[\w\-. ]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(itemName, fileNamePattern);
        }

        public static bool XmlIsValid(string itemContent)
        {
            return true;
        }

        public static bool JsonIsValid(string itemContent)
        {
            return true;
        }

        public static void SaveToFile(string fileName, string itemContent)
        {
            try
            {
                // writing file stream procedure
                System.IO.File.WriteAllText(@"" + fileName, itemContent);
            }
            catch (Exception)
            {
                // writing file stream failed handling procedure
                throw;
            }
        }

        public static bool FileExists(string fileName)
        {
            return System.IO.File.Exists(fileName);
        }

    }

    class FormulatrixRepos<T>
    {
        public static void Register(string itemName, T itemContent, int itemType)
        {
            if (itemType != 1 && itemType != 2)
            {
                Console.WriteLine("Item type 1 for JSON and 2 for XML! You cant pick other.");
                return;
            }

            if (!RepoCommon.FileNameIsValid(itemName))
            {
                Console.WriteLine("Item name \"" + itemName + "\" is invalid name.");
                return;
            }


            if (GetType(itemName) > 0)
            {
                Console.WriteLine("Item name already exists.");
                return;
            }

            if (itemType == 1 && !RepoCommon.JsonIsValid(itemContent.ToString()))
            {
                Console.WriteLine("JSON content is not valid!");
                return;
            }
            else if (itemType == 2 && !RepoCommon.XmlIsValid(itemContent.ToString()))
            {
                Console.WriteLine("XML content is not valid!");
                return;
            }


            string fileExt;

            if (itemType == 1)
                fileExt = ".json";
            else if (itemType == 2)
                fileExt = ".xml";
            else
            {
                Console.WriteLine("ItemType is only 1 or 2");
                return;
            }

            RepoCommon.SaveToFile(itemName + fileExt, itemContent.ToString());
        } 

        public static string Retrieve(string itemName)
        {
            if (!RepoCommon.FileNameIsValid( itemName ))
            {
                Console.WriteLine("Invalid item name!");
                return null;
            }

            int itemType = GetType(itemName);

            if (itemType == 0)
            {
                Console.WriteLine("Content doesn't exist!");
                return null;
            }

            string fileExt = itemType == 1 ? ".json" : ".xml";
            string fileName = itemName + fileExt;

            try
            {
                // open file stream procedure
                return System.IO.File.ReadAllText(@"" + fileName, Encoding.UTF8);
            }
            catch (Exception)
            {
                // load file failed handling procedure.
                throw;
            }
        }

        public static int GetType(string itemName)
        {
            // return -1 if item name is invalid
            if ( !RepoCommon.FileNameIsValid( itemName ) )
            {
                Console.WriteLine("Item name \"" + itemName + "\" is invalid name.");
                return -1;
            }

            string jsonItem = itemName + ".json", xmlItem = itemName + ".xml";

            if (RepoCommon.FileExists(jsonItem))
                return 1;

            if (RepoCommon.FileExists(xmlItem))
                return 2;

            // return 0 if item doesn't exist.
            return 0;
        }

        public static void Deregister(string itemName)
        {
            if ( !RepoCommon.FileNameIsValid( itemName ) )
            {
                Console.WriteLine("Item name \"" + itemName + "\" is invalid name.");
                return;
            }

            int itemType = GetType(itemName);

            if (itemType == 0)
            {
                Console.WriteLine("Content doesn't exist.");
                return;
            }

            string fileExt = itemType == 1 ? ".json" : ".xml";
            string fileName = itemName + fileExt;

            try
            {
                // deleting file procedure
                System.IO.File.Delete(@"" + fileName);
            }
            catch (Exception)
            {
                // deleting file failed handling procedure
                throw;
            }
        }

        private void Initialize() { }
    }
}
