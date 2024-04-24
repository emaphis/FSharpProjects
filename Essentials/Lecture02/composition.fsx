// Lesson 2 - Compostion

// Composing functions

type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

// Customer -> (Customer * decimal)
let getPurchases (customer: Customer): (Customer * decimal) =
    let purcheses = if customer.Id % 2 = 0 then 120M else 80M
    (customer, purcheses)

// (Customer * decimal) -> Customer
let tryPromoteToVip (purchases: (Customer * decimal)) =
    let (customer, amount) = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

// Customer -> Customer
let increaseCreditIfVip (customer: Customer) : Customer =
    match customer with
    | { IsVip = true} -> { customer with Credit = customer.Credit + 100m }
    | _ -> customer


// Composition

// Customer -> (Customer * decimal)
// (Customer * decimal) -> Customer
// Customer -> Customer


// Nested
let upgradeCustomer (customer: Customer): Customer =
    increaseCreditIfVip (tryPromoteToVip (getPurchases (customer)))

// Procedural
let upgradeCustomerProcedural (customer: Customer): Customer =
    let purchases = getPurchases (customer)
    let promoted = tryPromoteToVip (purchases)
    increaseCreditIfVip (promoted)


// Forward pipe operator
// (|>) :: ('a -> ('a -> 'b) -> 'b)
let upgradeCustomerPiped (customer: Customer): Customer =
    customer
    |> getPurchases
    |> tryPromoteToVip
    |> increaseCreditIfVip

// (|>) :; value: 'a -> func: ('a -> 'b) -> 'b
//let (|>) value func = func value


// Composition operator
// (>>) :: (('a -> 'b) -> ('b -> 'c) -> 'a -> 'c)
let upgradeCustomerComposed (customer: Customer): Customer =
    (getPurchases >> tryPromoteToVip >> increaseCreditIfVip) (customer)


let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }


let assertVIP = 
        upgradeCustomerComposed customerVIP = {Id = 1; IsVip = true; Credit = 100.0M }

let assertSTDtoVIP = 
        upgradeCustomerComposed customerSTD = {Id = 2; IsVip = true; Credit = 200.0M }

let assertSTD = 
        upgradeCustomerComposed { customerSTD with Id = 3; Credit = 50.0M } = {Id = 3; IsVip = false; Credit = 100.0M }
