[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg?style=flat)](https://gitter.im/Fody/Fody)
[![NuGet Status](http://img.shields.io/nuget/v/Immutable.Fody.svg?style=flat)](https://www.nuget.org/packages/Immutable.Fody/)


## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

![Icon](https://raw.github.com/Fody/Immutable/master/Icons/package_icon.png)

Creates immutable types

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage)

[![NuGet Status](https://img.shields.io/gitter/room/fody/fody.svg?style=flat)](https://gitter.im/Fody/Fody)


## The nuget package

https://nuget.org/packages/Immutable.Fody/

    PM> Install-Package Immutable.Fody


## Your code

    [Immutable]
    public class Sample
    {
        public string MyField = "Foo";
    }


## What gets compiled

    public class Sample
    {
        public readonly string MyField = "Foo";
    }


## What fields are targeted 

 * For Types with the `[Immutable]` attribute.
 * All instance fields


## Icon

Icon courtesy of [The Noun Project](http://thenounproject.com)