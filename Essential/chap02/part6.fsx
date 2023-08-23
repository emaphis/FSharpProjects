// Multiple Parameters
// Partial Application (Part 1)

type RegisteredCustomer = {
    Id : string
    IsEligible : bool
}

type UnregisteredCustomer = {
    Id : string
}

type Customer =
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer

// A function that takes a Customer and returns an anonymous function
let calculateTotal customer = 
    fun spend -> 
        let discount =
            match customer with
            | Registered c when c.IsEligible && spend >= 100.0M -> spend * 0.1M
            |  _ -> 0.0M
        spend - discount

let john = Registered { Id = "John"; IsEligible = true }

// Should return true.
let assertJohn1 = (calculateTotal john 100.0M = 90.0M)

// Partial Application.

// Decimal -> Decimal
let partial = calculateTotal john

// Decimal
let complete1 = partial 100.0M

// should be true
complete1 = 90.0M


// The Forward Pipe Operator

let complete2 = 100.0M |> calculateTotal john


// Make the asserts more readable by using '|>' operator

let areEqual expected actual =
    expected = actual

let assertJohn2 = areEqual 90.0M (calculateTotal john 100.0M)

// with pipe operator
let isEqualTo expected actual =
    expected = actual

let assertJohn3 = calculateTotal john 100.0M |> isEqualTo 90.0M

// calling the higher-order pipe as a function
let assertJohn4 = (|>) (calculateTotal john 100.0M) (isEqualTo 90.0M)

// so...
let assertJohn5 = (calculateTotal john 100.0M) |> (isEqualTo 90.0M)
