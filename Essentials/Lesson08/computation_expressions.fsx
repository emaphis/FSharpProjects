// Lesson 8 - Computation Expressions

#r "nuget: FsToolkit.ErrorHandling"

open System
open System.Text.RegularExpressions
open FsToolkit.ErrorHandling


type UnvalidatedUser = {
    Name : string
    Email : string
    DateOfBirth : string
    Discount : string
}

type ValidatedUser = {
    Name : string
    Email : string
    DateOfBirth : DateTime
    Discount : Decimal
}

type ValidateUser = UnvalidatedUser -> Result<ValidatedUser, string list>


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

let (|Split|) (on:char) (input:string) =
    input.Split(on) |> Array.toList

let (|NotEmptyString|_|) (input:string) =
    if input <> "" then Some () else None

let (|DateTime|_|) (input:string) =
    let (success, value) = DateTime.TryParse(input)
    if success then Some value else None


let (|Decimal|_|) (input:string) =
    let (success, value) = Decimal.TryParse input
    if success then Some value else None


let validateName (input:string) =
    match input with
    | NotEmptyString  -> Ok input
    | _ -> Error [ $"{input} invalid name" ]

let validateEmail (input:string) =
    match input with
    | Email _ -> Ok input
    | _ -> Error [$"{input} invalid email"]

let validateDateOfBirth (input:string) =
    match input with
    | DateTime value  -> Ok value
    | _ -> Error [$"{input} invalid date"]

let validateDiscount (input:string) =
    match input with
    | Decimal value  -> Ok value
    | _ -> Error [$"{input} invalid decimal"]


let createValidatedUser name email dateOfBirth discount : ValidatedUser =
    { Name = name
      Email = email
      DateOfBirth = dateOfBirth
      Discount = discount
    }


let validateUser (input:UnvalidatedUser) : Result<ValidatedUser, string list> =
    validation {
        let! name = validateName input.Name
        and! email = validateEmail input.Email
        and! dateOfBirth = validateDateOfBirth input.DateOfBirth
        and! discount = validateDiscount input.Discount
        return createValidatedUser name email dateOfBirth discount
    }
 
let createUnvalidatedUser (name, email,  dateOfBirth, discount) : UnvalidatedUser = 
    { Name = name
      Email = email
      DateOfBirth = dateOfBirth
      Discount = discount
    }


let import (input:string) =
    match input with
    | Split '|' [name; email; dateOfBirth; discount ] -> 
        createUnvalidatedUser(name, email, dateOfBirth, discount)
    | _ -> failwith "There was an error!"
    |> validateUser


let john = "John|john@test.com|2015-01-23|0.1"

let data1 = import john

printfn "%A" data1
