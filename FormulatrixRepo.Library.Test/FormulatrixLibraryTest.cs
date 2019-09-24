using NUnit.Framework;

namespace FormulatrixRepo.Library.Test
{
  [TestFixture]
  public class FormulatrixLibraryTest
  {
    // check the content of the Register input must be the same as output of the Retrieve content
    [Test]
    public void FormulatrixRepo_Register_Then_Retrieve_String()
    {
      string inputName = "test";
      int inputContentType = 1;
      string inputContent = "{\"test\" : \"test\"}";
      FormulatrixRepo<string>.Register( inputName, inputContent, inputContentType );
      string outputContent = FormulatrixRepo<string>.Retrieve<string>( inputName );

      // deregister so that this test case can repeated due to existance of an item
      FormulatrixRepo<string>.Deregister( inputName );
      Assert.AreEqual(inputContent, outputContent);
    }

    // check the content of the Register input must be the same as output of the Retrieve content
    [Test]
    public void FormulatrixRepo_Register_Then_Retrieve_Int()
    {
      string inputName = "test";
      int inputContentType = 1;
      int inputContent = 101;
      FormulatrixRepo<int>.Register( inputName, inputContent, inputContentType );
      int outputContent = FormulatrixRepo<int>.Retrieve<int>( inputName );

      // deregister so that this test case can repeated due to existance of an item
      FormulatrixRepo<int>.Deregister( inputName );
      Assert.AreEqual( inputContent, outputContent );
    }

    [Test]
    public void FormulatrixRepo_Register()
    {

    }
  }
}
