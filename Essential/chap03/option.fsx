// Null and Exception Handling

// Null Handling

// you will not have to deal with null in your F# code as it has a built-in type
// called Option that you will use instead.

type Option'<'T> =
    | Some' of 'T
    | None'


// Create a function to try to parse a string as a DateTime:

open System

let tryParseDateTime1 (input: string) =
    let (success, value) = DateTime.TryParse input
    if success then Some value else None

// Pattern match the result of the function with a match expression instead of
// using an if expression:
let tryParseDateTime2 (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | false, _ -> None

// The wildcard symbol _ implies the value can be anything.
let tryParseDateTime3 (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _, _ -> None


// The wildcard match _, _ can be simplified to a single wildcard:
let tryParseDateTime (input: string) =
    match DateTime.TryParse input with
    | true, result -> Some result
    | _ -> None

let isDate = tryParseDateTime "2019-08-01"
let isNotDate = tryParseDateTime "Hello"

//val isDate: DateTime option = Some 8/1/2019 12:00:00 AM
//val isNotDate: DateTime option = None


// optional middlename
type PersonName = {
    FirstName : string
    MiddleName : Option<string>
    LastName : string
}


// If the person doesn't have a middle name, you set it to None and if they do you set it to Some "name"
let person = { FirstName = "Ian"; MiddleName = None; LastName = "Russell"}
let person2 = { person with MiddleName = Some "????" }

// We have used the Option<'T> style but there is an option keyword that we can use instead:
type PersonName2 = {
    FirstName : string
    MiddleName : string option  // option keyword
    LastName : string
}


// Interop With .NET

// nulls can sneak into your codebase and that is through interop with
// code/libraries written in other .Net languages such as C# including most 
// of the .NET platform.

open System

// Reference type
let nullObj: string = null

// Nulllable type
let nullPri = Nullable<int>()

//val nullObj: string = <null>
//val nullPri: Nullable<int> = <null>


// convert to Option using Option.ofObj and Option.ofNullable functions:
let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

//val fromNullObj: string option = None
//val fromNullPri: int option = None

// convert form an Option type to .Net type
let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

//val toNullObj: string = <null>
//val toNullPri: Nullable<int> = <null>


// convert from an Option type to something that doesn't support null using
// pattern matching or the Option.defaultValue function:
let resultPM input =
    match input with
    | Some value -> value
    | None   -> "_____"

let resultDV = Option.defaultValue "____" fromNullObj

//val resultDV: string = "____"

// If the Option value is wrapped by Some otherwise None is returned
// use the forward pipe operator (|>)
let resultFP = fromNullObj |> Option.defaultValue "_____"
//val resultFP: string = "_____"


let resultFPA =
    fromNullObj
    |> Option.defaultValue "-----"
//val resultFPA: string = "-----"


// make this easier
let setUnknownAsDefault = Option.defaultValue "????"

let result = setUnknownAsDefault fromNullObj
//val result: string = "????"

// forward pipe operator
let result2 = fromNullObj |> setUnknownAsDefault
