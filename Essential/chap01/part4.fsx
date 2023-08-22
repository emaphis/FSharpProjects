// Alternative Approaches (1 of 2)

//  An alternative approach would be to use a discriminated union as a type
//  in a property of a record type:

type CustomerType =
    | Registered of IsEligible: bool
    | Guest

type Customer = { Id: string; Type: CustomerType }

let calculateTotal customer spend =
    let discount =
        match customer.Type with
        | Registered (IsEligible = true) when spend >= 100.0M -> spend * 0.1M
        | _ -> 0.0M
    spend - discount



let john = { Id = "John"; Type = Registered (IsEligible = true) }
let mary = { Id = "Mary"; Type = Registered (IsEligible = true) }
let richard = { Id = "Richard"; Type = Registered (IsEligible = false) }
let sarah = { Id = "Sarah"; Type = Guest }


// Should all be true
let assertJohn = calculateTotal john 100.0M = 90.0M
let assertMary = calculateTotal mary 99.0M = 99.0M
let assertRichard = calculateTotal richard 100.0M = 100.0M
let assertSarah = calculateTotal sarah 100.0M = 100.0M
