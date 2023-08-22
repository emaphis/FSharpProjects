// Making the Implicit Explicit

// Firstly, we create specific record types for Registered and Unregistered customers.
// Eliminate the boolean field.
type RegisteredCustomer = {
    Id : string
    IsEligible : bool
}

type UnregisteredCustomer = {
    Id : string
}

// Disctriminated union to represent the two kinds of Customers
type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer


// Guest of UnregisteredCustomer
let sarah = Guest { Id = "Sarah" }

// Registered of RegisteredCustomer
let john = Registered { Id = "John"; IsEligible = true }
let mary = Registered { Id = "Mary"; IsEligible = true }
let richard = Registered { Id = "Richard"; IsEligible = false }

// Pattern Matching using a match expression: to distinguish betweento two types of customer.
let calculateTotal customer spend =
    let discount =
        match customer with
        | Registered c when c.IsEligible && spend >= 100.0M -> spend * 0.1M
        |  _ -> 0.0M
    spend - discount

// All should return true.
let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.0M = 99.0M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
