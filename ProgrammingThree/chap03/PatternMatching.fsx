module PatternMatching

// Simple pattern matching

let isOdd x = x % 2 = 1

let describeNumber x =
    match isOdd x with
    | true -> printfn "x is odd"
    | false -> printfn "x is even"

do describeNumber 4
//x is even

// Example 3-5. Constructing a truth table using pattern matching
let testAnd x y =
    match x, y with
    | true, true    -> true
    | true, false   -> false
    | false, true   -> false
    | false, false  -> false

let bool1 = testAnd true true
//al bool1: bool = true

// '_' is the wild card matcher
let testAnd' x y =
    match x, y with
    | true, true -> true
    | _, _       -> false

let baal2 = testAnd' true false
//val baal2: bool = false


//module MatchFailure =

    (*
    let testAnd x y =
        match x, y with
        | true, true    -> true
        | true, false   -> false
       // | false, true   -> false
        | false, false  -> false
    // warning FS0025: Incomplete pattern matches on this expression. For example, the value '(_,true)' may indicate a case not covered by the pattern(s).
    *)

    (*
    // Incomplete pattern matching, Not every letter is matched
    let letterIndex l =
        match l with
        | 'a'  -> 1
        | 'b'  -> 2

    open MatchFailure

    let ret = letterIndex 'k'
    //The match cases were incomplete
    //at FSI_0015.letterIndex(Char l) in C:\src\fsharp\FSharpProjects\ProgrammingThree\chap03\PatternMatching.fsx:
    *)


module NamedPatterns =

    // Named patterns to extract and name data
    let greet name =
        match name with
        | "Robert"  -> printfn "Hello, Bob"
        | "William" -> printfn "Hello, Bill"
        | x         -> printfn $"Hello, {x}"

    do greet "Earl"


module MatchingLiterals =

    // Define a literal value
    [<Literal>]
    let Bill = "Bill Gates"

    // Match against literal values
    let greet name =
        match name with
        | Bill -> printfn "Hello, Bill G."
        | x    -> printfn $"Hello, {x}"

    do greet Bill
    //Hello, Bill G


module WhenGuards =

    // Custom logic to determine if a pattern matches

    // High / Low game
    open System

    let highLowGame () =
        let rng = new Random()
        let secretNumber = rng.Next() % 100

        let rec highLowGameStep () =

            printfn "Guess the secret number:"
            let guessStr = Console.ReadLine()
            let guess = Int32.Parse(guessStr)
 
            match guess with
            | _ when guess > secretNumber
                -> printfn "The secret number is lower."
                   highLowGameStep()

            | _  when guess = secretNumber
                -> printfn "You've guessed correctly!"
                   ()

            | _  when guess < secretNumber
                -> printfn "The secret number is higher."
                   highLowGameStep()

            | _ -> printfn "What!"

        // Begin the game
        highLowGameStep();;

//henGuards.highLowGame ()
 

module GroupingPatterns =

    let vowelTest c =
        match c with
        | 'a' | 'e' | 'i' | 'o' | 'u'-> true
        | _ -> false

    let describeNumbers x y =
        match x, y with
        | 1, _
        | _, 1
            -> "One of the numbers is one"
        | (2, _) & (_, 2)
            -> "Both numbers are 2"
        | _ -> "Other."



module MachingStructureData =

    // Tuples

    let testXor x y =
        match x, y with
        | tuple when fst tuple <> snd tuple
            -> true
        | true, true   -> false
        | false, false -> false
        | _ -> false


    // Lists
    // Example 3-6. Determining the length of a list
 
    let rec listLength l =
        match l with
        | []        -> 0
        | [_]       -> 1
        | [_; _]    -> 2
        | [_; _; _] -> 3
        | hd::tail -> 1 + listLength tail


    // Options

    let describeOptions o =
        match o with
        | Some(42) -> "The answer was 42, but what was thw question?"
        | Some(x) -> sprintf $"The answer was {x}"
        | None -> "No answer was found"


module OutsideMatchExpressions =

    // Pattern  matching can occure thoughout the F# language

    // let bindings

    let f() = 5

    let x1 = f()  // 'x1' matches to the int 5

    // Think of this as
    let x2 =
        match f() with
        | x ->  x


    // let bindings with tuples

    let t1 = (100, 200)

    let x3, y3 = t1
    //val y3: int = 200
    //val x3: int = 100

    let x4, y4 =
        match t1 with
        | x, y -> x, y

    //val y4: int = 200
    //val x4: int = 100

 
    // Function parameters
    // .. are pattern matches too.

    /// Gien a tuple of option values, return their sum
    let addOptionValues = fun (Some(x), Some(y)) -> x + y

    let sum = addOptionValues (Some(3), Some(4))
    //val sum: int = 7



module AlternateLambdaSyntaz =

    // It’s common to pass the parameter directly into a pattern-match expression,

    let rec listLength theList =
        match theList with
        | []        -> 0
        | [_]       -> 1
        | [_; _]    -> 2
        | [_; _; _] -> 3
        | _::tl  -> 1 + listLength tl

    // The 'function' keyword.
 
    let rec funListLength =
        function
        | []        -> 0
        | [_]       -> 1
        | [_; _]    -> 2
        | [_; _; _] -> 3
        | _::tl  -> 1 + listLength tl

    let len = funListLength [1..5]
    //val len: int = 5

    let howMany =
        (function
             | 1 -> "one"
             | 2 -> "two"
             | 3 -> "three"
             | _ -> "many") 3

    //val howMany: string = "three"
