// Lesson 1 - Modeling Customer Discounts - ver 1.5
// Make isEligible explisit


type Customer =
    | Eligible of Id:string
    | Registered of Id:string
    | Unregistered of Name:string

type Spend = decimal
type Total = decimal

type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer with
        | Eligible _  when spend >= 100m-> spend * 0.1m
        | Eligible _  ->  0m
        | Registered _ -> 0m 
        | Unregistered _ -> 0m

    spend - discount


// Data definitions
let john    = Eligible "John"
let mary    = Eligible "Mary"
let richard = Registered "Richard"
let sarah   = Unregistered "Sarah"


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
