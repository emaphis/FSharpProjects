// Lesson 7 - parsing

open System
open System.Text.RegularExpressions


type Customer = {
    Name : string
    Email : string
    IsEligible : bool
    IsRegistered : bool
    DateRegistered : DateTime
    Discount : Decimal
}

let createCustomer name email eligible registered dateRegistered discount = 
    { 
        Name = name
        Email = email
        IsEligible = eligible
        IsRegistered = registered
        DateRegistered = dateRegistered
        Discount = discount
    }


let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success && m.Groups.Count >= 1 then
        [ for x in m.Groups -> x.Value ]
        |> List.tail
        |> Some
    else None


let (|Email|_|) input =
    match input with
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None


let (|DateTime|_|) (input:string) =
    match DateTime.TryParse(input) with
    | true, value  -> Some value
    | false, _  -> None


let (|Decimal|_|) (input:string) =
    match Decimal.TryParse input with
    | true, value -> Some value
    | false, _ -> None


let (|Boolean|_|) (input:string) =
    match input with
    | "1" -> Some true
    | "0" -> Some false
    | _ -> None


let (|NotEmptyString|_|) (input:string) =
    if input.Length > 0 then Some input else None


let (|Split|) (on:char) (input:string) =
    input.Split(on)
    |> Array.toList


let parse (input:string)  =
    match input with
    | Split '|'
        [ NotEmptyString name;
          Email email;
          Boolean eligible;
          Boolean registered;
          DateTime dateRegistered;
          Decimal discount
        ] ->
        createCustomer name email eligible registered dateRegistered discount |> Ok
    | _ -> Error $"Invalid line {input}"


let john = "John|john@test.com|1|1|2015-01-23|0.1"

let fred = "Fred|fred'semail|1|1|2015-01-23|0.1"


let parseJohn = john |> parse

let parseFred = fred |> parse
