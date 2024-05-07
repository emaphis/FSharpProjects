// Lesson 9 - Improving your Domain Models
// Single-Case Discrimated-Untions
// Enforce data assumptions with a Discriminated Union


type Customer =
    | Registered of Id:string * isEligible:bool
    | Unregistered of Name:string

type Spend = Spend of decimal
type Total = Total of decimal

// How to use:
// let mySpend = Spend 50m       // construct
// let (Spend spend) = mySpend   // deconstruct


type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, Spend spend: Spend) : Total =
    let discount =
        match customer with
        | Registered (_, true) when spend >= 100m -> spend * 0.1m
        | Registered (_, _) -> 0m 
        | Unregistered _ -> 0m

    Total (spend - discount)


// Data definitions
let john    = Registered ("John", true)
let mary    = Registered ("Mary",  true)
let richard = Registered ("Richard", false)
let sarah   = Unregistered "Sarah"


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, Spend 99.0M) = Total 99.0M
let assertJohn    = calculateTotal(john, Spend 100.0M) = Total 90.0M
let assertRichard = calculateTotal(richard, Spend 100.0M) = Total 100.0M
let assertSarah   = calculateTotal(sarah, Spend 100.0M) = Total 100.0M
