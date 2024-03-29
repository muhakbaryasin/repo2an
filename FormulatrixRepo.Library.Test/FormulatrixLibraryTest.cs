﻿using System;
using NUnit.Framework;

namespace FormulatrixRepo.Library.Test
{
  [TestFixture]
  public class FormulatrixLibraryTest
  {
    // Register must create a file with a corresponding type and content
    [Test]
    public void FormulatrixRepo_Register_Then_Deregister([Values("test1", "test_1")] string inputName)
    {
      int inputContentType = 1;
      
      // will create test1.json file
      string inputContent = "{\"test1\" : \"test1\"}";
      FormulatrixRepo<string>.Register( inputName, inputContent, inputContentType );

      // check file existance
      Assert.IsTrue(System.IO.File.Exists(inputName + ".json"));
      
      // deregister so that this test case can be repeated due to existance of an item
      FormulatrixRepo<string>.Deregister( inputName );
    }

    // check the content of the Register input must be the same as output of the Retrieve content
    [Test]
    public void FormulatrixRepo_Register_Then_Retrieve_String_Then_Deregister()
    {
      string inputName = "test2";
      int inputContentType = 1;
      string inputContent = "{\"test2\" : \"test2\"}";
      FormulatrixRepo<string>.Register( inputName, inputContent, inputContentType );
      string outputContent = FormulatrixRepo<string>.Retrieve<string>( inputName );

      // deregister so that this test case can repeated due to existance of an item
      FormulatrixRepo<string>.Deregister( inputName );
      Assert.AreEqual(inputContent, outputContent);
    }

    // check the content of the Register input must be the same as output of the Retrieve content
    [Test]
    public void FormulatrixRepo_Register_Then_Retrieve_Int_Then_Deregister()
    {
      string inputName = "test3";
      int inputContentType = 1;
      int inputContent = 333;
      FormulatrixRepo<int>.Register( inputName, inputContent, inputContentType );
      int outputContent = FormulatrixRepo<int>.Retrieve<int>( inputName );

      // deregister so that this test case can repeated due to existance of an item
      FormulatrixRepo<int>.Deregister( inputName );
      Assert.AreEqual( inputContent, outputContent );
    }

    // check the content of the Register input must be the same as output of the Retrieve content
    [Test]
    public void FormulatrixRepo_Register_Then_Gettype_Then_Deregister()
    {
      string inputName = "test4";
      int inputContentType = 1;
      string inputContent = "{\"test4\" : \"test4\"}";
      FormulatrixRepo<string>.Register( inputName, inputContent, inputContentType );
      int outputContentType = FormulatrixRepo<string>.GetType( inputName );

      // deregister so that this test case can repeated due to existance of an item
      FormulatrixRepo<string>.Deregister( inputName );
      Assert.AreEqual( inputContentType, outputContentType );
    }

    // GetTtype of unregistered item must be 0
    [Test]
    public void FormulatrixRepo_Gettype_Arbitrary_Unregistered_Item()
    {
      string inputName = "test5";
      int contentType = FormulatrixRepo<string>.GetType( inputName );

      Assert.AreEqual(0, contentType );
    }

    // Retrieving content of unregistered item must throw an exception message about the content doesnt exist
    [Test]
    public void FormulatrixRepo_Retrieve_Arbitrary_Unregistered_Item()
    {
      string inputName = "test6";

      try
      {
        string content = FormulatrixRepo<string>.Retrieve<string>( inputName );
      }
      catch( System.Exception e)
      {
        StringAssert.Contains("doesn't exist", e.Message );
      }
    }

    // Deregister unregistered item must throw an exception message about the content doesnt exist
    [Test]
    public void FormulatrixRepo_Deregister_Arbitrary_Unregistered_Item()
    {
      string inputName = "test7";

      try
      {
        FormulatrixRepo<string>.Deregister( inputName );
      }
      catch( System.Exception e )
      {
        StringAssert.Contains( "doesn't exist", e.Message );
      }
    }

    // Try Register/Retrieve/Gettype-ing invalid item name must throw an exception message about the item name invalid
    [Test]
    public void FormulatrixRepo_RegisterRetrieveGettype_Arbitrary_Invalid_Item_Name( [Values( "test8,test", "test8/test", ".test8test", "test8.test" )] string inputName )
    {
      string inputContent = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend! - Test 8</body></note>";
      int contentType = 2;

      try
      {
        FormulatrixRepo<string>.Register( inputName, inputContent, contentType );
      }
      catch( System.Exception e )
      {
        StringAssert.Contains( "is invalid name", e.Message );
      }

      try
      {
        FormulatrixRepo<string>.Retrieve<string>( inputName );
      }
      catch( System.Exception e )
      {
        StringAssert.Contains( "is invalid name", e.Message );
      }

      try
      {
        FormulatrixRepo<string>.GetType( inputName );
      }
      catch( System.Exception e )
      {
        StringAssert.Contains( "is invalid name", e.Message );
      }
    }

    // Try rewrite registered item
    [Test]
    public void FormulatrixRepo_Register2x_Item()
    {
      string inputName = "test9";
      string inputContent = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><note><to>Tove</to><from>Jani</from><heading>Reminder</heading><body>Don't forget me this weekend! - Test 9</body></note>";
      int contentType = 2;

      try
      {
        FormulatrixRepo<string>.Register( inputName, inputContent, contentType );
        FormulatrixRepo<string>.Register( inputName, inputContent, contentType );
      }
      catch( System.Exception e )
      {
        StringAssert.Contains( "already exists", e.Message );
      }
      finally
      {
        FormulatrixRepo<string>.Deregister( inputName );
      }
    }
  }
}
