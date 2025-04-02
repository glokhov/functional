##th C# Functional Programming
Functions for converting between C# ```Option``` / ```Result``` types and 
F# ```FSharpOption``` / ```FSharpValueOption``` / ```FSharpResult``` types.
### Motivation
If you are using F# components in your C# code, some functions return values of the
```FSharpOption```, ```FSharpValueOption``` or ```FSharpResult``` types.
The support of those types in C# is very limited. You can use this component to convert the F# types
to the [```Option``` and ```Result``` implementations for C#](https://github.com/glokhov/functional).
```csharp
var fsharpResult      = FSharpResult<int, string>.NewOk(42);
var fsharpOption      = FSharpOption<int>.Some(42);
var fsharpValueOption = FSharpValueOption<int>.Some(42)
```
### Getting started
Import ```Functional.FSharp``` namespace:
```csharp
using Functional.FSharp;
```
Use extenstion methods ```ToResult``` and ```ToOption```:
```csharp
Result<int, string> result = fsharpResult.ToResult();
Option<int> option         = fsharpOption.ToOption();
Option<int> valueOption    = fsharpValueOption.ToOption();
```