[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg?style=flat)](https://gitter.im/Fody/Fody)
[![NuGet Status](http://img.shields.io/nuget/v/Immutable.Fody.svg?style=flat)](https://www.nuget.org/packages/Immutable.Fody/)


## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

![Icon](https://raw.github.com/Fody/Immutable/master/Icons/package_icon.png)

Creates immutable types

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage)

[![NuGet Status](https://img.shields.io/gitter/room/fody/fody.svg?style=flat)](https://gitter.im/Fody/Fody)


## NuGet installation

Install the [Immutable.Fody NuGet package](https://nuget.org/packages/Immutable.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

```powershell
PM> Install-Package Fody
PM> Install-Package Immutable.Fody
```

The `Install-Package Fody` is required since NuGet always defaults to the oldest, and most buggy, version of any dependency.


## Your code

```csharp
[Immutable]
public class Sample
{
    public string MyField = "Foo";
}
```


## What gets compiled

```csharp
public class Sample
{
    public readonly string MyField = "Foo";
}
```


## What fields are targeted 

 * For Types with the `[Immutable]` attribute.
 * All instance fields


## Icon

Icon courtesy of [The Noun Project](http://thenounproject.com)