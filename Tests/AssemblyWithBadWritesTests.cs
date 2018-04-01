using System.Linq;
using Fody;
using Xunit;
#pragma warning disable 618

public class AssemblyWithBadWritesTests
{
    TestResult testResult;

    public AssemblyWithBadWritesTests()
    {
        var weavingTask = new ModuleWeaver();
        testResult = weavingTask.ExecuteTestRun("AssemblyWithBadWrites.dll",runPeVerify: false);
    }

    [Fact]
    public void AssertErrors()
    {
        var errors = testResult.Errors.Select(x=>x.Text).ToList();
        Assert.Contains("Method 'ClassWithField.Method' has a write to the readonly field 'ClassWithField.Member'.", errors);
        Assert.Contains("Method 'ClassWithFieldInherit..ctor' has a write to the readonly field 'ClassWithField.Member'.", errors);
    }
}