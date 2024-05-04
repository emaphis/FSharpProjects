// Lesson 7 - Modeling Customer Discounts 
// Simplifying matching code with active patterns

type Customer =
    | Registered of Id:string * IsEligible:bool
    | Unregistered of Name:string


type Spend = decimal
type Total = decimal

type CalculateTotal = (Customer * Spend) -> Total

// new Active Pattern
let (|QualifyForDiscount|_|) (customer: Customer, spend: Spend) =
    match customer with
    | Registered (IsEligible = true) when spend >= 100m -> Some ()
    | _ -> None

let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer, spend with
        | QualifyForDiscount  -> spend * 0.1m
        | _ -> 0m

    spend - discount


// Data definitions
let john =   Registered ( Id = "John", IsEligible = true)
let mary =   Registered( Id = "Mary", IsEligible = true)
let richard = Registered (Id = "Richard", IsEligible = false)
let sarah =   Unregistered(Name = "Sarah")


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
