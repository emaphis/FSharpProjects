// 10 - Object Programming

// Class Types

/// Class to represent fizzbuzz
type FizzBuzz() =
    member _.Calculate(value) =
        [(3, "Fizz"); (5, "Buzz")]
        |> List.map (fun(v, s) ->
                if value % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string value else s

/// An instantiation
let fizzBuzz = FizzBuzz()

let fifteen = fizzBuzz.Calculate(15)  // FizzBuzz
[1..15] |> List.map fizzBuzz.Calculate


/// a function that will use the new FizzBuzz object type
let doFizzBuzz0 range =
    let fizzBuzz = FizzBuzz()
    range
    |> List.map (fun n -> fizzBuzz.Calculate(n))

let output0 = doFizzBuzz0 [1..15]


/// A code simplification
let doFizzBuzz1 range =
    let fizzBuzz = FizzBuzz()
    range
    |> List.map fizzBuzz.Calculate

let output1 = doFizzBuzz1 [1..15]


/// FizzBuzz with the mapping passed in the default constructor
type FizzBuzz2(mapping) =
    member _.Calculate(value) =
        mapping
        |> List.map (fun (v, s) -> if value % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string value else s


/// a function that will use the new FizzBuzz object type that takes a
/// a FizzBuzz mapping as a parameter
let doFizzBuzz2 mapping range =
    let fizzBuzz = FizzBuzz2(mapping)
    range
    |> List.map fizzBuzz.Calculate

let output2a = doFizzBuzz2 [(3, "Fizz"); (5, "Buzz")] [1..15]
let output2b = doFizzBuzz2 [(3, "Fizz"); (5, "Buzz"); (7, "Bazz")] [70..105]


/// Move the function code in the object from the member into the body of the
/// class type as a new inner function:
type FizzBuzz3(mapping) =
    let calculate n =  // an inner function
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    member _.Calculate(value) = calculate value


let doFizzBuzz3 mapping range =
    let fizzBuzz = FizzBuzz3(mapping)
    range
    |> List.map fizzBuzz.Calculate

let output3a = doFizzBuzz3 [(3, "Fizz"); (5, "Buzz")] [1..15]
let output3b = doFizzBuzz3 [(3, "Fizz"); (5, "Buzz"); (7, "Bazz")] [70..105]


// Interfaces

// The 'I' prefix to the name is not required but is used by convention in .NET
type IFizzBuzz =
    abstract member Calculate : int -> string

// Abstract Classes

// To convert IFizzBuzz into an abstract class, decorate it with
// [<Abstract>] attribute

/// Class implements IFizzBuzz
type FizzBuzz4(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s

    interface IFizzBuzz with
        member _.Calculate(value) = calculate value

// F# requires casting

let fizzBuzz2 = FizzBuzz4([(3, "Fizz");(5, "Buzz")]) :> IFizzBuzz //Upcast 
let fifeteen = fizzBuzz.Calculate(15)

let doFizzBuzz4a =
    let fizzBuzz = FizzBuzz4([(3, "Fizz");(5, "Buzz")]) :> IFizzBuzz // Upcast
    [1..15]
    |> List.map (fun n -> fizzBuzz.Calculate(n))

let doFizzBuzz4b =
    let fizzBuzz = FizzBuzz4([(3, "Fizz");(5, "Buzz")])
    [1..15]
    |> List.map (fun n -> (fizzBuzz :> IFizzBuzz).Calculate(n))


// Object Expressions

open System

type ILogger =
    abstract member Info : string -> unit
    abstract member Error : string -> unit

type Logger() =
    interface ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg

// create an object expression which simplifies the usage:
// An anonymous type
let logger = {
    new ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
}

// create a class type and pass the ILogger in as a constructor argument

type MyClass(logger: ILogger) =
    let mutable count = 0

    member _.DoSomething input =
        logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
        count <- count + 1
        ()

    member _.Count = count

// Use the class type with the object expression logger:

let myClass = MyClass(logger)
[1..10] |> List.iter myClass.DoSomething
printfn "%i" myClass.Count


// So it's very easy to use ILogger

/// A new Logger
let logger2 = {
    new ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
}

/// function that takes in an ILogger as a parameter
let doSomethingElse (logger: ILogger) input =
    logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
    ()

doSomethingElse logger2 "MyData"
