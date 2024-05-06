// Lesson 8 - Active Patterns Recap

/// Partial Active Patterns

// DateTime

open System

let (|DateTime|_|) (input:string) =
    let (success, value) =
        DateTime.TryParse(input)
    if success then Some value
    else None


let parseDate (input:string) =
    match input with
    | DateTime date ->
        $"{date} is a valid date"
    | _  ->
        $"{input} is not a valid date"


/// Parameterized Partial Active Patterns

// IsDivisibleBy

let (|IsDivisibleBy|_|) (divisor:int) (input:int) =
    if input % divisor = 0 then Some () else None 
    

let fizzBuzz input =
    match input with
    | IsDivisibleBy 3 & IsDivisibleBy 5 -> "FizzBuzz"
    | IsDivisibleBy 3 -> "Fizz"
    | IsDivisibleBy 5 -> "Buzz"
    | _  -> string input


let fb = [1..20] |> List.map fizzBuzz


/// Multi-Case Active Patterns

// Odd vs Even

let (|Odd|Even|) (input:int) =
   if input % 2 = 0 then Even else Odd


let isEven input =
    match input with
    | Even -> true
    | Odd -> false


/// Single-Case Active Patterns

// Split

let (|Split0|) (input:string) =
    input.Split('|') |> Array.toList


let (|Split|) (on:char) (input:string) =
    input.Split(on) |> Array.toList


let parse input =
    match input with
    | Split '|' items -> items 
    

let output = parse "This|is|some|output"
