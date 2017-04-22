using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;

[TestFixture]
public class AssemblyWithBadWritesTests
{
    List<string> errors = new List<string>();

    public AssemblyWithBadWritesTests()
    {
        var assemblyPath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\AssemblyWithBadWrites\bin\Debug\AssemblyWithBadWrites.dll"));
#if (!DEBUG)

        assemblyPath = assemblyPath.Replace("Debug", "Release");
#endif

        var newAssembly = assemblyPath.Replace(".dll", "2.dll");
        File.Copy(assemblyPath, newAssembly, true);

        var moduleDefinition = ModuleDefinition.ReadModule(newAssembly);
        var weavingTask = new ModuleWeaver
                              {
                                  ModuleDefinition = moduleDefinition,
                                  LogError = LogError
                              };

        weavingTask.Execute();

    }

    void LogError(string error)
    {
        errors.Add(error);
    }


    [Test]
    public void AssertErrors()
    {
        Assert.Contains("Method 'ClassWithField.Method' has a write to the readonly field 'ClassWithField.Member'.", errors);
        Assert.Contains("Method 'ClassWithFieldInherit..ctor' has a write to the readonly field 'ClassWithField.Member'.", errors);
        foreach (var error in errors)
        {
            Debug.WriteLine(error);
        }
    }

}