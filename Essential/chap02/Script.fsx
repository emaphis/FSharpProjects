// Functions

// Composing functions

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Customer -> (Customer * decimal)
let getPurchases customer =
    let purcheses = if customer.Id % 2 = 0 then 120M else 80M
    (customer, purcheses)

// (Customer * decimal) -> Customer
let tryPromoteToVip purchases =
    let (customer, amount) = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

// Customer -> Customer
let increaseCreditIfVip customer =
    let increase = if customer.IsVip then 100M else 50M
    { customer with Credit = customer.Credit + increase }


// Composition operator
let upgradeCustomerComposed =
    getPurchases >> tryPromoteToVip >> increaseCreditIfVip

// Nested
let upgradeCustomerNested customer =
    increaseCreditIfVip(tryPromoteToVip(getPurchases customer))

// Procedural
let upgradeCustomerProcedural customer =
    let customerWithPurchases = getPurchases customer
    let promotedCustomer = tryPromoteToVip customerWithPurchases
    let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
    increasedCreditCustomer

// Forward pipe operator
let upgradeCustomerPiped customer =
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip


let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }


let assertVIP = 
        upgradeCustomerComposed customerVIP = {Id = 1; IsVip = true; Credit = 100.0M }
let assertSTDtoVIP = 
        upgradeCustomerComposed customerSTD = {Id = 2; IsVip = true; Credit = 200.0M }
let assertSTD = 
        upgradeCustomerComposed { customerSTD with Id = 3; Credit = 50.0M } = {Id = 3; IsVip = false; Credit = 100.0M }


// Unit

open System

let now() = DateTime.UtcNow
// al now: unit -> DateTime
now()

let log msg =
    // log message
    ()

now
// val it: (unit -> DateTime) = <fun:it@75>

// fixed value
let fixedNow = DateTime.UtcNow
let theTimeIs = fixedNow


// Anonumous Functions

let add x y = x + y
let sum = add 1 4

let add' = fun x y -> x + y
let sum' = add' 1 4

let apply f x y = f x y
// val apply: f: ('a -> 'b -> 'c) -> x: 'a -> y: 'b -> 'c

let sum'' = apply add 1 4

// passin an anonymous function
let sum4 = apply (fun x y -> x + y) 1 4

// a function that returns a random number between 0 and 99
open System

let rnd() =
    let rand = Random()
    rand.Next(100)

List.init 50 (fun _ -> rnd())


let rnd' =
    let rand = Random()
    fun () -> rand.Next(100)

List.init 50 (fun _ -> rnd'())


// Multiple Parameter

type LogLevel =
    | Error
    | Warning
    | Info

let log1 (level: LogLevel) message =
    printfn "[%A]: %s" level message
    

let logError = log1 Error

let m1 = log1 Error "Curried function"
let m2 = logError "Partially Applied function"

log1 Error "Curried function" 
logError "Partially Applied function"
