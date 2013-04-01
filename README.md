## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

Creates immutable types

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage)

## Nuget package http://nuget.org/packages/Immutable.Fody 

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



