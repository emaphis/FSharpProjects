// Lesson 9 - Improving your Domain Models
// Single-case Discrimated Unions

#r "nuget: FsToolkit.ErrorHandling"

open System
open System.Text.RegularExpressions
open FsToolkit.ErrorHandling


type Name = Name of string
type Email = private Email of string
type DateOfBirth = DateOfBirth of DateTime
type Discount = Discount of Decimal

type EmailError =
    | EmailError of string

type UserValidationError =
    | InvalidName of string
    | InvalidEmail of EmailError
    | InvalidDateOfBirth of string
    | InvalidDiscount of string

 
 type UnvalidatedUser = {
     Name : string
     Email : string
     DateOfBirth : string
     Discount : string
 }
 
 type ValidatedUser = {
     Name : Name
     Email : Email
     DateOfBirth : DateOfBirth
     Discount : Discount
 }
 
 type ValidateUser = UnvalidatedUser -> Result<ValidatedUser, UserValidationError list>
 

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success && m.Groups.Count >= 1 then
        [ for x in m.Groups -> x.Value ]
        |> List.tail
        |> Some
    else None


[<RequireQualifiedAccess>]
module Email =

    let value input = input |> fun (Email value) -> value

    let create input =
        match input with
        | ParseRegex ".*?@(.*)" [ _ ] -> Ok (Email input)
        | _ -> Error (InvalidEmail (EmailError input))



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
    | NotEmptyString  -> Ok (Name input)
    | _ -> Error  (InvalidName $"{input} invalid name")  

//let validateEmail (input:string) =
 //   Email.create input

let validateDateOfBirth (input:string) =
    match input with
    | DateTime value  -> Ok (DateOfBirth value)
    | _ -> Error (InvalidDateOfBirth input)

let validateDiscount (input:string) =
    match input with
    | Decimal value  -> Ok (Discount value)
    | _ -> Error (InvalidDiscount input)


let createValidatedUser name email dateOfBirth discount : ValidatedUser =
    { Name = name
      Email = email
      DateOfBirth = dateOfBirth
      Discount = discount
    }


let validateUser (input:UnvalidatedUser) : Result<ValidatedUser, UserValidationError list> =
    validation {
        let! name = validateName input.Name
        and! email = Email.create input.Email
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
