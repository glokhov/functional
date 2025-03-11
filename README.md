# C# Functional Programming [![Nuget Version](https://img.shields.io/nuget/v/Functional.Monad)](https://www.nuget.org/packages/Functional.Monad) [![Nuget Download](https://img.shields.io/nuget/dt/Functional.Monad)](https://www.nuget.org/packages/Functional.Monad)
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

int someValue = some.Match(value => value, 0);
int noneValue = none.Match(value => value, 0);

Assert.Equal(42, someValue);
Assert.Equal(0, noneValue);
```
```Result``` monads support either an ok value, or an error value:
```csharp
Result<int, string> ok = Ok(42);
Result<int, string> err = Err("There is no answer");

int okValue = ok.Match(value => value, 0);
int errValue = err.Match(value => value, 0);
string error = err.Match(value => $"{value}", error => error);

Assert.Equal(42, okValue);
Assert.Equal(0, errValue);
Assert.Equal("There is no answer", error);
```
A nullable object can be converted to an ```Option```:
```csharp
string? someString = "Forty two";
string? nullString = null;

Option<string> some = someString.ToOption();
Option<string> none = nullString.ToOption();

string someResult = some.Match(value => value, "none");
string noneResult = none.Match(value => value, "none");

Assert.Equal("Forty two", someResult);
Assert.Equal("none", noneResult);
```
A nullable value can be converted to an ```Option```:
```csharp
int? someInt = 42;
int? nullInt = null;

Option<int> some = someInt.ToOption();
Option<int> none = nullInt.ToOption();

int someValue = some.Match(value => value, 0);
int noneValue = none.Match(value => value, 0);

Assert.Equal(42, someValue);
Assert.Equal(0, noneValue);
```
An ```Option``` can be converted to a nullable object:
```csharp
Option<string> some = Some("Forty two");
Option<string> none = None;

string? someValue = some.ToObject();
string? noneValue = none.ToObject();

Assert.Equal("Forty two", someValue);
Assert.Null(noneValue);
```
An ```Option``` can be converted to a nullable value:
```csharp
Option<int> some = Some(42);
Option<int> none = None;

int? someValue = some.ToNullable();
int? noneValue = none.ToNullable();

Assert.Equal(42, someValue);
Assert.False(noneValue.HasValue);
```
A ```Result``` can be converted to an ```Option```:
```csharp
Result<int, string> ok = Ok(42);
Result<int, string> err = Err("There is no answer");

Option<int> some = ok.ToOption();
Option<int> none = err.ToOption();

Assert.True(some.IsSome);
Assert.True(none.IsNone);
```
A ```Result``` can be converted to a nullable object:
```csharp
Result<string, string> ok = Ok("Forty two");
Result<string, string> err = Err("There is no answer");

string? okValue = ok.ToObject();
string? errValue = err.ToObject();

Assert.Equal("Forty two", okValue);
Assert.Null(errValue);
```
A ```Result``` can be converted to a nullable value:
```csharp
Result<int, string> ok = Ok(42);
Result<int, string> err = Err("There is no answer");

int? okValue = ok.ToNullable();
int? errValue = err.ToNullable();

Assert.Equal(42, okValue);
Assert.False(errValue.HasValue);
```