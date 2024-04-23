// Lesson 1 - Modeling Customer Discounts - ver 1.4
// Enforce data assumptions with a Discriminated Union


type Customer =
    | Registered of Id:string * isEligible:bool
    | Unregistered of Name:string

type Spend = decimal
type Total = decimal

type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer with
        | Registered (_, true) when spend >= 100m -> spend * 0.1m
        | Registered (_, _) -> 0m 
        | Unregistered _ -> 0m

    spend - discount


// Data definitions
let john    = Registered ("John", true)
let mary    = Registered ("Mary",  true)
let richard = Registered ("Richard", false)
let sarah   = Unregistered "Sarah"


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
