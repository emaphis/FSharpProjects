// Lesson 10 - Recap of Domain Modeling

open System
open System.Text.RegularExpressions

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success && m.Groups.Count >= 1 then
        [ for x in m.Groups -> x.Value ]
        |> List.tail
        |> Some
    else None

type CustomerId = CustomerId of Guid


let (|Email|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None

type Customer = {
    Id: Guid
    Name: string
    Email: string
}

