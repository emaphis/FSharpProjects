// Lesson 7 - Single Case Total Active Patterns

open System

// Password should be:
// 1. At leaast 8 characters in length
// 2. Must contain at least on digit.


let (|CharacterCount|) (input: string) =
    input.Length

let (|ContainsANumber|) (input:string) =
    input
    |> Seq.filter Char.IsDigit
    |> Seq.length > 0

let (|InvalidPassword|_|) input =
    match input with
    | CharacterCount len when len < 8
        -> Some "Password must be at least 8 characters long"
    | ContainsANumber false
        -> Some "Password must contains at least one number"
    | _ -> None

let setPassword input =
    match input with
    | InvalidPassword failureMsg -> Error failureMsg
    | _ -> Ok ()

// examples
let badPawssword1 = setPassword "word5"
let badPassword2 = setPassword "password"
let goodPassword1 = setPassword "passw0rd"


// Multi-case Total Active Pattern

let (|Odd|Even|) input =
    if input % 2 = 0 then Even else Odd

let isOdd input =
    match input with
    | Odd -> true
    | Even -> false


let (|Spring|Summer|Autum|Winter|What|) input =
    match input with
    | 1 | 2 | 3 -> Winter
    | 4 | 5 | 6 -> Spring
    | 7 | 8 | 9 -> Summer
    | 10 | 11 | 12  -> Autum
    | _  -> What
