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


let (|IsEmail|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None


type CustomerId = CustomerId of Guid


module MyTypes =

    type Email = private Email of string


    module Email =

        let value input = input |>  fun (Email email) -> email  // deconstruct 

        let create input = 
            match input with
            | IsEmail _  -> input |> Email |> Ok
            | _  -> Error $"{input} is invalid Email"



open MyTypes

type Customer = {
    Id: CustomerId
    Name: string
    Email: Email
}


let cust1 = {
    Id = CustomerId (Guid.NewGuid())
    Name = "John"
    Email = Email.create "jown@home.com"
}

let email1 = cust1.Email

let map f result =
    match result with
    | Ok x  -> x |> f |> Ok
    | Error err -> Error err

let mapError f result =
    match result with
    | Ok x  -> Ok x
    | Error err -> err |> f |> Error
