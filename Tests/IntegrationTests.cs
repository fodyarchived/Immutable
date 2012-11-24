using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;

[TestFixture]
public class IntegrationTests
{
    Assembly assembly;
    List<string> errors = new List<string>();

    public IntegrationTests()
    {
        var assemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
#if (!DEBUG)

        assemblyPath = assemblyPath.Replace("Debug", "Release");
#endif

        var newAssembly = assemblyPath.Replace(".dll", "2.dll");
        File.Copy(assemblyPath, newAssembly, true);

        var moduleDefinition = ModuleDefinition.ReadModule(newAssembly);
        var weavingTask = new ModuleWeaver
                              {
                                  ModuleDefinition = moduleDefinition,
                                  LogErrorPoint = LogError,
                              };

        weavingTask.Execute();
        moduleDefinition.Write(newAssembly);

        assembly = Assembly.LoadFile(newAssembly);
    }

    void LogError(string error, SequencePoint arg2)
    {
        errors.Add(error);

    }


    [Test]
    public void AssertNoErrors()
    {
        Assert.IsEmpty(errors);
    }
    [Test]
    public void ClassWithField()
    {
        var instance = assembly.GetInstance("ClassWithField");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.IsTrue(fieldInfo.IsInitOnly);
		Assert.AreEqual("InitialValue", instance.Member);
    }

    [Test]
    public void ClassWithNoAttribute()
    {
        var instance = assembly.GetInstance("ClassWithNoAttribute");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.IsFalse(fieldInfo.IsInitOnly);
    }

    [Test]
	public void ClassWithReadOnlyField()
    {
		var instance = assembly.GetInstance("ClassWithReadOnlyField");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.IsTrue(fieldInfo.IsInitOnly);
		Assert.AreEqual("InitialValue", instance.Member);

    }
    
    
    [Test]
    public void StructWithFields()
    {
        var type = assembly.GetType("StructWithFields");
        var fieldInfo = type.GetField("Member");
        Assert.IsTrue(fieldInfo.IsInitOnly);
    }


#if(DEBUG)
    [Test]
    public void PeVerify()
    {
        Verifier.Verify(assembly.CodeBase.Remove(0, 8));
    }
#endif

}