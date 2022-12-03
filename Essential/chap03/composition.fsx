// Function Composition With Result

open System

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}


let getPurchases customer =
    try
        // Imagein this function is fetching data from a Database
        let purchases =
            if customer.Id % 2 = 0 then (customer, 120M)
            else (customer, 80M)
        Ok purchases
    with
    | ex -> Error ex
// val getPurchases: customer: Customer -> Result<(Customer * decimal),exn>

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 1000M then { customer with IsVip = true }
    else customer
// val tryPromoteToVip: Customer * decimal -> Customer

let increaseCredidIfVip customer =
    try
        // Imagine this function could cause an exception
        let increase =
            if customer.IsVip then 100.0M else 50.0M
        Ok { customer with Credit = customer.Credit + increase }
    with
    | ex -> Error ex
// val increaseCredidIfVip: customer: Customer -> Result<Customer,exn>


let map f result = 
    match result with
    | Ok x -> Ok (f x)
    | Error ex -> Error ex

let bind f result = 
    match result with
    | Ok x -> f x
    | Error ex -> Error ex

let upgradeCustomer customer =
    customer
    |> getPurchases
    |> map tryPromoteToVip
    |> bind increaseCredidIfVip


let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }



let assertVIP =
    upgradeCustomer customerVIP = Ok {Id = 1; IsVip = true; Credit = 100.0M }
let assertSTDtoVIP =
    upgradeCustomer customerSTD = Ok {Id = 2; IsVip = true; Credit = 200.0M }
let assertSTD =
    upgradeCustomer { customerSTD with Id = 3; Credit = 50.0M } = Ok {Id = 3; IsVip = false; Credit = 100.0M }

