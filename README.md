# Functional.Monad

[![CI](https://github.com/glokhov/functional/actions/workflows/ci.yml/badge.svg)](https://github.com/glokhov/functional/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Functional.Monad)](https://www.nuget.org/packages/Functional.Monad)

`Option` and `Result` types for C#, built on unions.

## Install

```shell
dotnet add package Functional.Monad
```

## Getting started

Import the types and the `Prelude` factory functions, globally:

```csharp
global using Functional;
global using static Functional.Prelude;
```

or per file:

```csharp
using Functional;
using static Functional.Prelude;
```

## Option

`Option<TValue>` holds either a value, `Some`, or no value, `None`.

In methods returning an option, use `Some` and `Nothing` from the `Prelude`:

```csharp
Option<int> ParseInt(string text) => int.TryParse(text, out var value) ? Some(value) : Nothing;
```

In local variables declared with `var`, use the create methods of `Option`:

```csharp
var some = Option<int>.Some(42);
var none = Option<int>.None;

Assert.True(some.IsSome);
Assert.True(none.IsNone);
```

Match an option with `Some<T>` and `None`:

```csharp
string Describe(Option<int> option) => option switch
{
    Some<int> some => $"Some({some.Value})",
    None => "nothing"
};
```

Property patterns add conditions on the value:

```csharp
string Size(Option<int> option) => option switch
{
    Some<int> { Value: > 100 } => "large",
    Some<int> => "small",
    None => "nothing"
};
```

`Map` transforms the value, `Bind` chains option-returning methods:

```csharp
var mapped = Option<int>.Some(42).Map(value => value * 2);      // Some(84)
var bound = Option<string>.Some("42").Bind(ParseInt);           // Some(42)
var empty = Option<string>.None.Bind(ParseInt);                 // None
```

`DefaultValue` and `DefaultWith` unwrap the value:

```csharp
var value = ParseInt("42").DefaultValue(0);                     // 42
var fallback = ParseInt("forty two").DefaultWith(() => 0);      // 0
```

A nullable reference converts to an option and back. Assign `null` to a nullable-typed
variable first; the untyped `null` literal cannot be converted directly to `Option<T>`:

```csharp
string? maybePath = null;
Option<string> option = maybePath;
string? path = option;
```

## Result

`Result<TValue, TError>` holds either a success value, `Ok`, or a failure value, `Error`.

In methods returning a result, use `Ok` and `Error` from the `Prelude`:

```csharp
Result<int, string> Divide(int a, int b) => b == 0 ? Error("division by zero") : Ok(a / b);
```

In local variables declared with `var`, use the create methods of `Result`:

```csharp
var ok = Result<int, string>.Ok(42);
var error = Result<int, string>.Error("division by zero");

Assert.True(ok.IsOk);
Assert.True(error.IsError);
```

Match a result with `Ok<T>` and `Error<T>`:

```csharp
string Describe(Result<int, string> result) => result switch
{
    Ok<int> ok => $"Ok({ok.Value})",
    Error<string> error => $"Error({error.Value})"
};
```

`Map`, `MapError` and `Bind` work on the success and failure sides:

```csharp
var mapped = Divide(84, 2).Map(value => value * 2);             // Ok(84)
var reported = Divide(42, 0).MapError(err => err.ToUpper());    // Error(DIVISION BY ZERO)
var bound = Divide(84, 2).Bind(value => Divide(value, 0));      // Error(division by zero)
```

`DefaultValue` and `DefaultWith` unwrap the success value:

```csharp
var value = Divide(84, 2).DefaultValue(0);                      // 42
var fallback = Divide(42, 0).DefaultWith(() => 0);              // 0
```

## Conversions

An option converts to a result, and a result converts to an option:

```csharp
var result = Option<int>.Some(42).ToResult();                   // Ok(42)
var option = Result<int, string>.Error("error").ToOption();     // None
```

## License

[MIT](LICENSE)
