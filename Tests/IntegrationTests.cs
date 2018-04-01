using System;
using Fody;
using Xunit;
#pragma warning disable 618

public class IntegrationTests
{
    TestResult testResult;

    public IntegrationTests()
    {
        var weavingTask = new ModuleWeaver();
        testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll");
    }

    [Fact]
    public void ClassWithField()
    {
        var instance = testResult.GetInstance("ClassWithField");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.True(fieldInfo.IsInitOnly);
        Assert.Equal("InitialValue", instance.Member);
    }

    [Fact]
    public void ClassWithNoAttribute()
    {
        var instance = testResult.GetInstance("ClassWithNoAttribute");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.False(fieldInfo.IsInitOnly);
    }

    [Fact]
    public void ClassWithReadOnlyField()
    {
        var instance = testResult.GetInstance("ClassWithReadOnlyField");

        Type type = instance.GetType();
        var fieldInfo = type.GetField("Member");
        Assert.True(fieldInfo.IsInitOnly);
        Assert.Equal("InitialValue", instance.Member);
    }

    [Fact]
    public void StructWithFields()
    {
        var type = testResult.Assembly.GetType("StructWithFields");
        var fieldInfo = type.GetField("Member");
        Assert.True(fieldInfo.IsInitOnly);
    }
}