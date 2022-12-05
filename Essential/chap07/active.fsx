// 7 - Active Patterns

// Partial Active Patterns

open System

let parse' (input: string) =
    match DateTime.TryParse(input) with
    | true, value   -> Some value
    | false, _      -> None

let isDate = parse' "2019-12-20"
let notDate = parse' "Bullwinkle"

// (| ... |)   - active pattern
// (| ... |_|) - partial active pattern

let (|ValidDate|_|) (input: string) =
    match DateTime.TryParse(input) with
    | true, value   -> Some value
    | false, _      -> None

// using if
let (|ValidDate'|_|) (input:string) =
    let success, value = DateTime.TryParse(input)
    if success then Some value else None


let parse input =
    match input with
    | ValidDate dt -> printfn "%A" dt
    | _            -> printfn $"'%s{input}' is not a valid date"

let isDate1 = parse "2019-12-20"
let notDate1 = parse "Bullwinkle"


// if you don't care about the value being returned

let (|IsValidDate|_|) (input: string) =
    let success, _  = DateTime.TryParse input
    if success then Some () else None

let isValidDate input =
    match input with
    | IsValidDate -> true
    | _     -> false

let isDate2 = isValidDate "2019-12-20"
let notDate2 = isValidDate "Bullwinkle"


// Partial active patterns are used a lot in activities like validation, as we will discover in Chapter 8.

// Parameterized Partial Active Patterns

/// FizzBuzz engine
let calculate1 i =
    if i % 3 = 0 && i % 5 = 0 then "FizzBuzz"
    elif i % 3 = 0 then "Fizz"
    elif i % 5 = 0 then "Buzz"
    else i |> string

[1..15] |> List.map calculate1


// pattern matching
let calculate2 i =
    match (i % 3, i % 5) with
    | (0, 0) -> "FizzBuss"
    | (0, _) -> "Fizz"
    | (_, 0) -> "Buzz"
    | _      -> string i

[1..15] |> List.map calculate2


let calculate3 i =
    match (i % 3 = 0, i % 5 = 0) with
    | (true, true) -> "FizzBuzz"
    | (true, _) -> "Fizz"
    | (_, true) -> "Buzz"
    | _ -> i |> string

[1..15] |> List.map calculate3

// active pattern

let (|IsDivisibleBy|_|) divisor n =
    if n % divisor = 0 then Some () else None

let calculate4 i =
    match i with
    | IsDivisibleBy 3 & IsDivisibleBy 5 -> "FizzBuzz"
    | IsDivisibleBy 3 -> "Fizz"
    | IsDivisibleBy 5 -> "Buzz"
    | _ -> i |> string

[1..15] |> List.map calculate4


// using a list as the input parameter

let (|IsDivisibleBy1|_|) divisors n =
    if divisors |> List.forall (fun div -> n % div = 0)
    then Some ()
    else None

let calculate5 i =
    match i with
    | IsDivisibleBy1 [3;5;7] -> "FizzBuzzBazz"
    | IsDivisibleBy1 [3;5] -> "FizzBuzz"
    | IsDivisibleBy1 [3;7] -> "FizzBazz"
    | IsDivisibleBy1 [5;7] -> "BuzzBazz"
    | IsDivisibleBy1 [3] -> "Fizz"
    | IsDivisibleBy1 [5] -> "Buzz"
    | IsDivisibleBy1 [7] -> "Bazz"
    | _ -> i |> string

[1..15] |> List.map calculate5


let calculate6 n =
    [(3, "Fizz"); (5, "Buzz"); (7, "Bazz")]
    |> List.map (fun (divisor, result) -> if n % divisor = 0 then result else "")
    |> List.reduce (+) // (+) is a shortcut for (fun acc v -> acc + v)
    |> fun input -> if input = "" then string n else input

[1..15] |> List.map calculate6


let calculate7 mapping n =
    mapping
    |> List.map (fun (divisor, result) -> if n % divisor = 0 then result else "")
    |> List.reduce (+)
    |> fun input -> if input = "" then string n else input

[1..15] |> List.map (calculate7 [(3, "Fizz"); (5, "Buzz")])


// Leap years

let isLeapYear1 year =
    year % 400 = 0 || (year % 4 = 0 && year % 100 <> 0)

[2000; 2001; 2020] |> List.map isLeapYear1 = [true; false; true]


let (|IsDivisibleBy2|_|) divisor n =
    if n % divisor = 0 then Some () else None

let (|NotDivisibleBy2|_|) divisor n =
    if n % divisor <> 0 then Some () else None

let isLeapYear2 year =
    match year with
    | IsDivisibleBy2 400 -> true
    | IsDivisibleBy2 4 & NotDivisibleBy2 100 -> true
    | _  -> false

[2000; 2001; 2020] |> List.map isLeapYear2 = [true; false; true]


// Multi-Case Active Patterns

type Rank = Ace|Two|Three|Four|Five|Six|Seven|Eight|Nine|Ten|Jack|Queen|King
type Suit = Hearts|Clubs|Diamonds|Spades
type Card = Rank * Suit

let (|Red|Black|) (card: Card) =
    match card with
    | (_, Diamonds) | (_, Hearts) -> Red
    | (_, Clubs) | (_, Spades)  -> Black

let describeColour card =
    match card with
    | Red  -> "red"
    | Black -> "black"
    |> printfn "The card is %s"

describeColour(Two, Hearts)


// Single-Case Active Patterns

// Password verification

open System

let (|CharacterCount|) (input: string) =
    input.Length

let (|ContainsANumber|) (input:string) =
    input
    |> Seq.filter Char.IsDigit
    |> Seq.length > 0


let (|IsValidPoassword|) input =
    match input with
    | CharacterCount len when len < 8 -> (false, "Password must be at least 8 characters.")
    | ContainsANumber false -> (false, "Password must contain at least 1 digit.")
    | _  -> (true, "")

let setPassword input =
    match input with
    | IsValidPoassword (true, _) as pwd -> Ok pwd
    | IsValidPoassword (false, failureReason) -> Error $"Password not set: %s{failureReason}"

let badPassword = setPassword "password"
let shorPassword = setPassword "sh0rt"
let goodPassword = setPassword "passw0rd"


// Using Active Patterns in a Practical Example

/// Type to represent the score
type Score = int * int

/// Determine if we have a correct score.
let (|CorrectScore|_|) (expected: Score, actual: Score) =
    if expected = actual then Some () else None


/// determine what the result of a Score is
let (|Draw|HomeWin|AwayWin|) (score: Score) =
    match score with
    | (h, a) when h = a  -> Draw
    | (h, a) when h > a  -> HomeWin
    | _   -> AwayWin


/// active pattern for determining if we have predicted the correct result
let (|CorrectResult|_|) (expected: Score, actual: Score) =
    match (expected, actual) with
    | (Draw, Draw)  -> Some ()
    | (HomeWin, HomeWin)  -> Some ()
    | (AwayWin, AwayWin)  -> Some ()
    | _  -> None


/// work out the points for the goals scored:
let goalsScore (expected:Score) (actual:Score) =
    let home = [ fst expected; fst actual ] |> List.min
    let away = [ snd expected; snd actual ] |> List.min
    (home * 15) + (away * 20)


/// calculate the total points for each game:
let calculatePoints (expected:Score) (actual:Score) =
    let pointsForCorrectScore =
        match (expected, actual) with
        | CorrectScore -> 300
        | _ -> 0
    let pointsForCorrectResult =
        match (expected, actual) with
        | CorrectResult -> 100
        | _ -> 0
    let pointsForGoals = goalsScore expected actual
    pointsForCorrectScore + pointsForCorrectResult + pointsForGoals


let assertnoScoreDrawCorrect = 
    calculatePoints (0, 0) (0, 0) = 400
let assertHomeWinExactMatch = 
    calculatePoints (3, 2) (3, 2) = 485
let assertHomeWin = 
    calculatePoints (5, 1) (4, 3) = 180
let assertIncorrect = 
    calculatePoints (2, 1) (0, 7) = 20
let assertDraw = 
    calculatePoints (2, 2) (3, 3) = 170
