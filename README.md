# C# Functional Programming

A simple implementation of an ```Option``` and a ```Result``` monads.

## Getting started

Use the ```global using``` directive for the whole project:

```C#
global using Functional;
global using static Functional.Prelude;
```

Or the ```using``` directive in a single file: 

```C#
using Functional;
using static Functional.Prelude;
```

```Option``` monads support either some value, or no value: 

```C#
Option<int> some = Some(42);
Option<int> none = None;
```

```Result``` monads support either an ok value, or an error value:

```C#
Result<int, string> ok = Ok(42);
Result<int, string> err = Err("Error description");
```
