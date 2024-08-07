// Chapter 03 - F# Syntax Basics
//  Understanding core F# syntax
//  Working with type inference in F#

// 3.1 F# syntax basics

// 3.1.1 Characteristics of F# syntax

// Readability

//  Sensible Defaults

// Consistency

// A Worked Example

// Listing 3.1 F# syntax basics
let addTenThenDouble theNumber =
    let addedTen = theNumber + 10
    let answer = addedTen * 2
    printfn $"({theNumber} + 10) * 2 is {answer}"
    let website = System.Uri "https://fsharo.org"
    answer


// Listing 3.2 A verbose version of F# syntax basics
let addTenThenDoubleV (theNumber : int) : int =
    let addedTen : int = theNumber + 10;
    let answer : int = addedTen * 2;
    printfn $"({theNumber} + 10) * 2 is {answer}";
    let website : System.Uri = new System.Uri ("https://fsharp.org");
    answer;


// 3.1.2 The let keyword
//  Bind values to symbols

let doACalculation theNumber =
    let twenty = 20
    let answer = twenty + theNumber
    let foo = System.Uri "https://fsharo.org"
    answer

// Functions are just values

let isaac = 42
let olderIsaac = isaac + 1
let youngerIsaac = isaac - 1

// 3.1.3 Scoping


// Exercise 3.1
//  Create a function that takes in three numbers as input
//  arguments. Add the first two together and bind it to a symbol
//  inProgress. Then, multiply that by the third argument and
//  bind it to a symbol answer. Finally, return a string that says The
//  answer is {answer} using string interpolation.

let addTwoThenMultiply a b c =
    let inProgress = a + b
    let answer = inProgress * c
    $"The answer is {answer}"


// Nested Scopes

// flat scope
let fname = "Frank"
let sname = "Schmidt"
let fullName = $"{fname} {sname}"
let greetingText = $"Greetings, {fullName}"

// nested scope
let greetingText0 =
    let fullName =
        let fname = "Frank"
        let sname = "Schmeidt"
        $"{fname} {sname}"
    $"Greetings, {fullName}"


// Nested Functions

let greetingTextWithFunction person = 
    let makeFullName fname sname =
        $"{fname} {sname}"
    let fullName = makeFullName "Frank" "Schmidt"
    $"Greetings {fullName} from {person}"


// Accessing Outer Scoped Values

// Listing 3.3 Accessing outer scoped values

let greetingTextWithFunction0 =
    let city = "London"
    let makeFullName fname sname =
        $"{fname} {sname} from {city}"
    let fullName = makeFullName "Frank" "Schmidt"
    let surnameCity = $"{sname} from {city}"
    $"Greetings, {fullName}"

// Cyclical Dependencies
// let description = $"{employee} lives in New York."   // wont compile
// let employee = "Joe Bloggs"


// 3.2 Type inference

// 3.2.1 Benefits of type inference

// 3.2.2 Type inference basics

let ia = 10  // no type annotation
let ib : int = 10  // type annotation

// IDE Support for Type Inference  - CodeLense  // type

let myInt = 10
let myString = "Hello"
let myFloat = 123.456
let myDate = System.DateTime.Now

// Listing 3.4 Working with type annotations

let add0 (a: int) (b: int) : int =
    let answer: int  = a + b
    answer

// Exercise 3.2
// Exercise3_2.fsx

let add a b =
    let answer = a + b
    answer

// Exercise 3.3

// 1. Add the string hello to a + b on line 3. What
//    happens?
 
// 2. Add a type annotation of string to the return type of
//    the function. What happens?
 
// 3. Explicitly annotate b as an integer again and add
//    13.34 (which is a float, not an int) to a + b on line 3.
//    What happens?

// let add1 a (b : int) : string =
//    let answer : string = a + b + 13.34
//    answer


// 3.2.3 Inferring generics

// Working with Existing Generic Types
//  System.Collections.Generic.List<'T>

let explicit = ResizeArray<int>()
// val explicit: ResizeArray<int>

let typeHole = ResizeArray<_>()
typeHole.Add 99
// val typeHole: ResizeArray<int>

let omitted = ResizeArray()
omitted.Add 10
// val omitted: ResizeArray<int>


// Exercise 3.4
let omitted1 = ResizeArray()
omitted1.Add "A string"
// val omitted1: ResizeArray<string>


// Automatic Generalization

let combineElements<'T> (a: 'T) (b: 'T) (c: 'T) =
    let output = ResizeArray<'T>()
    output.Add a
    output.Add b
    output.Add c
    output

// val combineElements: a: 'T -> b: 'T -> c: 'T -> ResizeArray<'T>

combineElements 1 2 3
// val it: ResizeArray<int> = seq [1; 2; 3]

//combineElements 1 2 "test"  // does not compile


let combineElements1 a b c =
    let output = ResizeArray()
    output.Add a
    output.Add b
    output.Add c
    output

// val combineElements1: a: 'a -> b: 'a -> c: 'a -> ResizeArray<'a>

combineElements1 1 2 3
// val combineElements1: a: 'a -> b: 'a -> c: 'a -> ResizeArray<'a>


// 3.2.4 Diagnosing unexpected type
//       inference behavior

let calculateGroup age =
    if age < 18 then "Child"
    elif age < 65 then "Adult"
    else "Pensioner"

let sayHello someValue =
    let group =
        if someValue < 10.0 then calculateGroup 15
        else calculateGroup 35
    "Hello " + group

let result = sayHello 10.5
// val result: string = "Hello Adult"

// Exercise 3.5
// Exercise3_5.fsx


// 3.2.5 Limitations of type inference
// Doesn't infer types from the CLR object system

// requires the type annotation
let addThreeDays (theDate: System.DateTime) =
    theDate.AddDays 3

// val addThreeDays: theDate: System.DateTime -> System.DateTime

// type annotation is unneeded
let addYearAndThreeDays theDate =
    let threeDaysForward = addThreeDays theDate
    theDate.AddYears 1

// val addYearAndThreeDays: thaeDate: System.DateTime -> System.DateTime

// 3.2.6 Criticisms of type inference
