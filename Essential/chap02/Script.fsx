// Functions

// Composing functions

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Customer -> (Customer * decimal)
let getPurchases customer =
    let purchases = if customer.Id % 2 = 0 then 120M else 80M
    (customer, purchases)  // Parentheses are optional

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


// Verify the output of the upgrade functions using FSI
let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }

let assertVIP = 
        upgradeCustomerComposed customerVIP = {Id = 1; IsVip = true; Credit = 100.0M }
let assertSTDtoVIP = 
        upgradeCustomerComposed customerSTD = {Id = 2; IsVip = true; Credit = 200.0M }
let assertSTD = 
        upgradeCustomerComposed { customerSTD with Id = 3; Credit = 50.0M } = {Id = 3; IsVip = false; Credit = 100.0M }


// Unit
// A function that doesn't require an input value or produce any output, F# has a special type called unit.

open System

let now () = DateTime.UtcNow

let log msg =
    // log message or similar task that doesn't return a value
    ()

// Call the now function without the unit () parameter
now
// val it: (unit -> DateTime) = <fun:it@75>

// al now: unit -> DateTime
now()


// If you forget to add the unit parameter () when you define the binding, you
// get a fixed value from the first time the line is executed:
let fixedNow = DateTime.UtcNow
let theTimeIs = fixedNow


// Anonymous Functions

// S simple named function:
let add x y = x + y
let sum1 = add 1 4
// 5

// Rewrite using lambda
let add' = fun x y -> x + y
let sum2 = add' 1 4
// 5

// In the following 'f' is a function
let apply f x y = f x y
// val apply: f: ('a -> 'b -> 'c) -> x: 'a -> y: 'b -> 'c

// 'apply' the 'add' function
let sum3 = apply add 1 4
// 5

// pass an anonymous function with the same signature and behavour oas the add function
let sum4 = apply (fun x y -> x + y) 1 4
// 5


// a function that returns a random number between 0 and 99
open System

let rnd() =
    let rand = Random()
    rand.Next(100)

List.init 50 (fun _ -> rnd())


// Next use scoping rules to re-use the same instance of the Random class each time
// you call the rnd function:

let rnd' =
    let rand = Random()
    fun () -> rand.Next(100)

List.init 50 (fun _ -> rnd'())


// Multiple Parameters

// Partial Application (Part 1)
// - see part6.fsx


// Partial Application (Part 2)

type LogLevel =
    | Error
    | Warning
    | Info

let log1 (level: LogLevel) message =
    printfn "[%A]: %s" level message  // side effect

// String interpolation - string -> unit
let log2 (level:LogLevel) message = 
    printfn $"[%A{level}]: %s{message}"

// No type-safety - 'a -> unit
let log3 (level:LogLevel) message = 
    printfn $"[{level}]: {message}"

// Partially apply the log function, define a new function that only takes the log
// function and its level argument but not the message:
// (string -> unit)
let logError = log1 Error

// The name logError is bound to a function that takes a string and returns unit.
// So now, we can use the logError function instead:
let m1 = log1 Error "Curried function"
let m2 = logError "Partially Applied function"
//[Error]: Curried function
//[Error]: Partially Applied function
