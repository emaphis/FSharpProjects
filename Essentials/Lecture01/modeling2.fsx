// Lesson 1 - Modeling Customer Discounts - ver 
// Tuple changed into a Record

type Customer = {
    Name : string
    IsRegistered : bool
    IsEligible : bool
}

type Spend = decimal
type Total = decimal
type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer with
        | { IsRegistered = true; IsEligible = true } when spend >= 100m ->
            spend * 0.10m
        | _ -> 0m
    spend - discount


// Data definitions
let john =    { Name = "John"; IsEligible = true; IsRegistered = true }
let mary =    { Name = "Mary"; IsEligible = true; IsRegistered = true }
let richard = { Name = "Richard"; IsEligible = false; IsRegistered = true }
let sarah =   { Name = "Sarah"; IsEligible = false; IsRegistered = false }


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
