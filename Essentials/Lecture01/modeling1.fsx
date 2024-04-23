// Lesson 1 - Modeling Customer Discounts - ver 1

type Customer = (string * bool * bool)
type Spend = decimal
type Total = decimal

type CalculateTotal = (Customer * Spend) -> Total


let calculateTotal (customer: Customer, spend: Spend) : Total =
    let discount =
        match customer with
        | _, true, true  when spend >= 100m ->
            spend * 0.10m
        | _, true, true -> 0m
        | _ -> 0m

    spend - discount


// Data definitios
let john    = ("John", true, true) 
let mary    = ("Mary", true, true)
let richard = ("Richard", true, false)
let sarah   = ("Sarah", false, false)


// Informal assertions as tests - should all evaluate to true
let assertMary    = calculateTotal(mary, 99.0M) = 99.0M
let assertJohn    = calculateTotal(john, 100.0M) = 90.0M
let assertRichard = calculateTotal(richard, 100.0M) = 100.0M
let assertSarah   = calculateTotal(sarah, 100.0M) = 100.0M
