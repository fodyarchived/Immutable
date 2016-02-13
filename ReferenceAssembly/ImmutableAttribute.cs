using System;

/// <summary>
/// Marks the class should be Immutable. 
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false)]
public sealed class ImmutableAttribute : Attribute
{
}