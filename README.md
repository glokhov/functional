# C# Functional Programming [![Nuget](https://img.shields.io/nuget/v/Functional.Monad)](https://www.nuget.org/packages/Functional.Monad)

Simple ```Option``` and ```Result``` monad implementation for C#.

## Getting started

Use the ```global using``` directive for the whole project:

```csharp
global using Functional;
global using static Functional.Prelude;
```

Or the ```using``` directive in a single file: 

```csharp
using Functional;
using static Functional.Prelude;
```

```Option``` monads support either some value, or no value: 

```csharp
Option<int> some = Some(42);
Option<int> none = None;

int value = some.Pure().Value;
Unit unit = none.Fail().Error;
```

```Result``` monads support either an ok value, or an error value:

```csharp
Result<int, string> ok = Ok(42);
Result<int, string> err = Err("Error description");

int value = ok.Pure().Value;
string error = err.Fail().Error;
```