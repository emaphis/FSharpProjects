// Lesson 1 - Modeling Customer Discounts - ver 1.3
// Enforce data assumptions with a Discriminated Union


type CustomerType =
    | Registered of isEligible : bool
    | Unregistered

type Customer = {
    Name : string
    Type : CustomerType
}

type Spend = decimal
type Total = decimal


type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer.Type with
        | Registered  (isEligible = true ) when spend >= 100m -> spend * 0.1m
        | Registered _ -> 0m 
        | Unregistered -> 0m

    spend - discount


// Data definitions
let john =    { Name = "John"; Type = Registered (isEligible = true) }
let mary =    { Name = "Mary"; Type = Registered (isEligible = true) }
let richard = { Name = "Richard"; Type = Registered (isEligible = false) }
let sarah =   { Name = "Sarah"; Type = Unregistered }


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
