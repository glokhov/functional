# Functional.Monad

[![CI](https://github.com/glokhov/functional/actions/workflows/build.yml/badge.svg)](https://github.com/glokhov/functional/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/Functional.Monad)](https://www.nuget.org/packages/Functional.Monad)

`Option` and `Result` types for C#, built on unions.

## Install

```shell
dotnet add package Functional.Monad
```

## Getting started

Import the types, globally:

```csharp
global using Functional;
```

or per file:

```csharp
using Functional;
```

## Option

`Option<TValue>` holds either a value, `Some`, or no value, `None`.

Create an option from a `Some<T>` or `None` value; both convert implicitly to `Option<T>`:

```csharp
Option<int> ParseInt(string text) => int.TryParse(text, out var value) ? new Some<int>(value) : default(None);

Option<int> some = new Some<int>(42);
Option<int> none = default(None);

Assert.True(some is Some<int>);
Assert.True(none is None);
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
Option<int> Half(int number) => number % 2 == 0 ? new Some<int>(number / 2) : default(None);

var mapped = ParseInt("42").Map(value => value * 2);    // Some(84)
var bound = ParseInt("42").Bind(Half);                  // Some(21)
var empty = ParseInt("forty two").Bind(Half);           // None
```

`Match` handles both cases with functions or actions. The `None` case takes a function or a value:

```csharp
var text = ParseInt("42").Match(value => $"Some({value})", () => "nothing");    // Some(42)
var number = ParseInt("forty two").Match(value => value * 2, -1);               // -1

ParseInt("42").Match(value => Console.WriteLine(value), () => Console.WriteLine("nothing"));
```

`Default` unwraps the value, taking a fallback value or a function that provides it:

```csharp
var value = ParseInt("42").Default(0);                    // 42
var fallback = ParseInt("forty two").Default(() => 0);    // 0
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

Create a result from an `Ok<T>` or `Error<T>` value; both convert implicitly to `Result<T, TError>`:

```csharp
Result<int, string> Divide(int a, int b) => b == 0 ? new Error<string>("division by zero") : new Ok<int>(a / b);

Result<int, string> ok = new Ok<int>(42);
Result<int, string> error = new Error<string>("division by zero");

Assert.True(ok is Ok<int>);
Assert.True(error is Error<string>);
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

`Match` handles both cases with functions or actions. The `Error` case takes a function or a value:

```csharp
var text = Divide(84, 2).Match(value => $"Ok({value})", err => $"Error({err})");    // Ok(42)
var number = Divide(42, 0).Match(value => value * 2, -1);                           // -1

Divide(84, 2).Match(value => Console.WriteLine(value), err => Console.WriteLine(err));
```

`Default` unwraps the success value, taking a fallback value or a function that provides it:

```csharp
var value = Divide(84, 2).Default(0);                       // 42
var fallback = Divide(42, 0).Default(err => err.Length);    // 16
```

## Conversions

An option converts to a result, and a result converts to an option:

```csharp
var result = ParseInt("42").ToResult();                        // Ok(42)
var option = Divide(42, 0).ToOption();                         // None
```

A nullable value, reference or struct, converts to an option or a result:

```csharp
int? nullable = 42;
var some = nullable.ToOption();    // Some(42)
var ok = nullable.ToResult();      // Ok(42)
```

`Transpose` swaps an option of a result with a result of an option:

```csharp
Option<Result<int, string>> some = new Some<Result<int, string>>(new Ok<int>(42));
Result<Option<int>, string> none = new Ok<Option<int>>(default(None));

var transposed = some.Transpose();    // Ok(Some(42))
var back = none.Transpose();          // None
```

## Utilities

`Choose` keeps the values of the `Some` results of a function:

```csharp
Option<int> Identity(Option<int> option) => option;

var numbers = new[] { "1", "x", "3" }.Choose(ParseInt);                                 // 1, 3
var values = new Option<int>[] { new Some<int>(1), default(None) }.Choose(Identity);    // 1
```

`Ignore` discards a value and returns `Unit`:

```csharp
var unit = 42.Ignore();    // ()
```

## License

[MIT](LICENSE)
