// Lession 3 - Options, Results, map, bind


// null handling

type Option1<'T> =
    | Some1 of 'T
    | None1

type Result1<'TSuccess1, 'TFailure1> =
    | Ok1 of 'TSuccess1
    | Error1 of 'TFailure1


open System

// Reference type
let nullObj:string = null  // string is nullable

// Nullable type
let nullPri = Nullable<int>()


// From .NET to F#

let fromNullObj = Option.ofObj nullObj

let fromNullPri = Option.ofNullable nullPri


// From F# to .NET

let toNullObj = Option.toObj fromNullObj

let toNullPri = Option.toNullable fromNullPri

// optionsl data may not exist

type Name = {
    FirstName : string
    MiddleName : Option<string>
    LastName : string
}

let person1 = { FirstName = "Ian"; MiddleName = None; LastName = "Russell"}

let person2 = { person1 with MiddleName = Some "????" }

// other syntax
type Name1 = {
    FirstName : string
    MiddleName : string option
    LastName : string
}


let divide1 x y = x / y

//let bad1 = divide1 10 0

let tryDivide1 x y =
    if y = 0 then None
    else Some (x / y)

let ok1 = tryDivide1 10 0

let good1 = tryDivide1 10 

let dontDoThis = Option.get (Some 2) 

let ok2 =
    match ok1 with
    | Some x -> x
    | None -> -99999


// string -> Option<DateTime>
let tryParseDateTime1 (input: string) =
    let (success, value) = DateTime.TryParse(input)
    if success then Some value else None


// string -> Option<DateTime>
let tryParseDateTime2 (input: string) =
    match DateTime.TryParse input with
    | true, value -> Some value
    | false, _  -> None


// string -> Option<DateTime>
let tryParseDateTime3 (input: string) =
    match DateTime.TryParse input with
    | true, value -> Some value
    | _, _  -> None


let isDate3 = tryParseDateTime3 "2019-08-01" // Some 01/08/2019 00:00:00 

let isNotDate3 = tryParseDateTime3 "Hello" // None


// Result type

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}


// Customer -> Result<(Customer * decimal), exn>
let getPurchases customer =
    try
        // Imagine this function is fetching data from a database
        let purchase =
            if customer.Id % 2 = 0 then (customer, 120m)
            else (customer, 80m)

        Ok purchase
    with
    | ex -> Error ex


// Customer * decimal -> Customer
let tryPromoteToVIP purchaes =
    let customer, amount = purchaes
    if amount > 100m then { customer with IsVip = true}
    else customer


// Customer -> Result<Customer,exn>
let increaseCreditIfVip customer =
    try
        // Imagine this function could cause an exception
        let increase =
            if customer.IsVip then 100.0m else 50.0m
        Ok { customer with Credit = customer.Credit + increase }
    with
    | ex -> Error ex


//let upgradeCustomer1 customer =
//    customer
//    |> getPurchases
//    |> tryPromoteToVIP  // compiler error
//    |> increaseCreditIfVip

//let upgradeCustomer customer =
//    customer
//    |> getPurchases
//    |> fun result ->
//        match result with
//        | Ok x -> Ok (tryPromoteToVIP x)
//        | Error ex -> Error ex
//    |> increaseCreditIfVip  // Compiler error


let upgradeCustomer3 customer =
    customer
    |> getPurchases
    |> fun result ->
        match result with
        | Ok x -> Ok (tryPromoteToVIP x)
        | Error ex -> Error ex
    |> fun result ->
        match result with
        | Ok x -> increaseCreditIfVip x
        | Error ex -> Error ex


let map1 func1 result =
    match result with
    | Ok x -> Ok (func1 x)
    | Error ex -> Error ex


let bind1 func2 result =
    match result with
    | Ok x -> func2 x
    | Error ex -> Error ex


let upgradeCustomer4 customer =
    customer
    |> getPurchases
    |> map1 tryPromoteToVIP
    |> bind1 increaseCreditIfVip


let upgradeCustomer5 customer =
    customer
    |> getPurchases
    |> Result.map tryPromoteToVIP
    |> Result.bind increaseCreditIfVip


let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }


let assertVIP = 
    upgradeCustomer4 customerVIP = Ok {Id = 1; IsVip = true; Credit = 100.0M }

let assertSTDtoVIP = 
    upgradeCustomer4 customerSTD = Ok {Id = 2; IsVip = true; Credit = 200.0M }

let assertSTD = 
    upgradeCustomer4 { customerSTD with Id = 3; Credit = 50.0M } = Ok {Id = 3; IsVip = false; Credit = 100.0M }
