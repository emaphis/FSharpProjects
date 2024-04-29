// Lesson 5 - Exceptions

open System

let multiply x y =
    x * y

let divide0 x y =
    if y = 0 then None
    else Some (x / y)


let divide1 x y =
    try
        Some (x / y)
    with
    | :? DivideByZeroException  ->  None 
    | _ -> None


let calculate1 x y =
    divide1 x y
    |> Option.map (multiply x)
    |> Option.bind (fun v -> divide1 v y)

// a custom error
type DivideError =
    | DivideByZero 

let divide x y =
    try
        Ok (x / y)
    with
    | :? DivideByZeroException  ->  Error DivideByZero 
  

let calculate x y =
    divide x y
    |> Result.map (multiply x)
    |> Result.bind (fun v -> divide v y)

calculate 4 0


// Never returns.  not a functions

let fail1: unit = failwith "divide by zero"

let fail2: unit  = invalidArg  "y" (string 0)

let fail3: unit  = invalidOp  "divide by zero is invalid"


// More custom errors
// Result type - see Lesson 3

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

type GetPurchasesError =
    | HasOddId of int

type IncreaseCreditIfVIPError =
    | IsNotVIP

type UpgradeCusomeError =
    | GetPurchases of GetPurchasesError
    | IncreaseCredit of IncreaseCreditIfVIPError
    

// Customer -> Result<(Customer * decimal), exn>
let getPurchases customer =
    // Imagine this function is fetching data from a database
    if customer.Id % 2 = 0 then Ok (customer, 120m)
    else Error (HasOddId customer.Id)


// Customer * decimal -> Customer
let tryPromoteToVIP purchaes =
    let customer, amount = purchaes
    if amount > 100m then { customer with IsVip = true}
    else customer


// Customer -> Result<Customer,exn>
let increaseCreditIfVip customer =
    // Imagine this function could cause an exceptio
    if customer.IsVip then 
        Ok { customer with Credit = customer.Credit + 100.0m }
    else Error IsNotVIP

//let upgradeCustomer customer :  Result<Customer, UpgradeCusomeError> = 
//    customer
//    |> getPurchases
//    |> Result.mapError (fun err -> GetPurchases err)
//    |> Result.map tryPromoteToVIP
//    |> Result.bind increaseCreditIfVip
//    |> Result.mapError (fun err -> IncreaseCredit err)


let upgradeCustomer customer :  Result<Customer, UpgradeCusomeError> = 
    customer
    |> getPurchases
    |> Result.mapError (fun err -> GetPurchases err)
    |> Result.map tryPromoteToVIP
    |> fun result ->
        match result with
        | Ok cust ->
            match increaseCreditIfVip cust with
            | Ok res -> Ok res
            | Error err -> Error (IncreaseCredit err)
        | Error err -> Error err

    //|> Result.bind increaseCreditIfVip
    //|> Result.mapError (fun err -> IncreaseCredit err)


// Result.map      : (('a -> 'b) -> Result<'a,'c> -> Result<'b,'c>)
// Result.mapError : (('a -> 'b) -> Result<'c,'a> -> Result<'c,'b>)

let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }
