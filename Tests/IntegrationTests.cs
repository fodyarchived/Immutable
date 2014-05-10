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
    string beforeAssemblyPath;
    string afterAssemblyPath;

    public IntegrationTests()
    {
        beforeAssemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\Debug\AssemblyToProcess.dll");
#if (!DEBUG)

        beforeAssemblyPath = beforeAssemblyPath.Replace("Debug", "Release");
#endif

        afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.dll");
        File.Copy(beforeAssemblyPath, afterAssemblyPath, true);

        var moduleDefinition = ModuleDefinition.ReadModule(afterAssemblyPath, new ReaderParameters
        {
        });
        var weavingTask = new ModuleWeaver
        {
            ModuleDefinition = moduleDefinition,
        };

        weavingTask.Execute();
        moduleDefinition.Write(afterAssemblyPath);

        assembly = Assembly.LoadFile(afterAssemblyPath);
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
        Verifier.Verify(beforeAssemblyPath, afterAssemblyPath);
    }
#endif

}