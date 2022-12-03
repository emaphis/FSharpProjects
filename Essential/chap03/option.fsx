// Null and Exception Handling

// Null Handling

open System

let tryParseDateTime (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | false, _ -> None

let isDate = tryParseDateTime "2019-08-01"
let isNotDate = tryParseDateTime "Hello"


// optional middlename
type PersonName = {
    FirstName : string
    MiddleName : string option
    LastName : string
}


let person = { FirstName = "Ian"; MiddleName = None; LastName = "Russell"}

let person2 = { person with MiddleName = Some "????" }


// Interop With .NET

open System

// Reference tyoe
let nullObj: string = null

// Nulllable tyoe
let nullPri = Nullable<int>()

// convert to Option
let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

// convert form an Option type to .Net type
let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

// convert from an Option type to something that doesn't support null
let resultPM input =
    match input with
    | Some value -> value
    | None   -> "_____"

let resultDV = Option.defaultValue "____" fromNullObj

// use the forward pipe operator (|>)
let resultFP = fromNullObj |> Option.defaultValue "_____"

let resultFPA =
    fromNullObj
    |> Option.defaultValue "-----"

// make this easier
let setUnknownAsDefault = Option.defaultValue "????"

let result = setUnknownAsDefault fromNullObj

// forward pipe operator
let result2 = fromNullObj |> setUnknownAsDefault
