// Lession 07 - Partial active patterns

open System

let parse0 input =
    match DateTime.TryParse(input) with
    | true, value -> Some value
    | false, _  -> None


let bad0 = parse0 "2024-23-18"

let good0 = parse0 "2024-03-18"


// An+ active pattern

let (|DateTime|_|) (input:string) =
    match DateTime.TryParse(input) with
    | true, value -> Some value
    | false, _ -> None

let parse (input: string) =
    match input with
    | DateTime value -> Some value
    | _ -> None

let bad = parse "2024-23-18"

let good = parse "2024-03-18"


// regex example
open System.Text.RegularExpressions


let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success && m.Groups.Count >= 1 then
        [ for x in m.Groups -> x.Value ]
        |> List.tail
        |> Some
    else None


let (|Lmail|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None

// Paramatized Partial Active Patternss+
// FizzBuzz

// Canonical

let fizzBuzzC input =
    if input % 3 = 0 && input % 5 = 0 then "FizzBuzz"
    elif input % 3 = 0 then "Fizz"
    elif input % 5 = 0 then "Buzz"
    else input |> string

[1..12] |> List.map fizzBuzzC

// Using pattern matching

let fizzBuzz0 input =
    match input % 3 = 0, input % 5 = 0 with
    | (true, true) -> "FizzBuzz"
    | (true, _) -> "Fizz"
    | (_, true) -> "Buzz"
    | _ -> input |> string


// Using active patterns

let (|IsDivisibleBy1|_|) divisor input =
    if input % divisor = 0 then Some () else None

let fizzBuzz1 input =
    match input  with
    | IsDivisibleBy1 3 & IsDivisibleBy1 5 -> "FizzBuzz"
    | IsDivisibleBy1 3 -> "Fizz"
    | IsDivisibleBy1 5 -> "Buzz"
    | _ -> string input


let (|IsDivisibleBy|_|) (divisors: int list) (input: int) =
    if divisors|> List.forall (fun div -> input % div = 0)
    then Some ()
    else None

let fizzBuzz input =
    match input  with
    | IsDivisibleBy [3; 5] -> "FizzBuzz"
    | IsDivisibleBy [3] -> "Fizz"
    | IsDivisibleBy [5] -> "Buzz"
    | IsDivisibleBy [] -> string input
    | _ -> "Unknown"


[1..20] |> List.map (fun num -> fizzBuzz num)


// This is extensible

let fizzBuzsBazz input =
    match input with
    | IsDivisibleBy [3;5;7] -> "FizzBuzzBazz"
    | IsDivisibleBy [3;5] -> "FizzBuzz"
    | IsDivisibleBy [3;7] -> "FizzBazz"
    | IsDivisibleBy [5;7] -> "BuzzBazz"
    | IsDivisibleBy [3] -> "Fizz"
    | IsDivisibleBy [5] -> "Buzz"
    | IsDivisibleBy [7] -> "Bazz"
    | _ -> input |> string

[1..100] |> List.map fizzBuzsBazz
