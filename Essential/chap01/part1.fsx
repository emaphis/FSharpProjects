// Gettign Started


// See model.txt ...

// Record type
type Customer = {
    Id : string;
    IsEligible : bool;
    IsRegistered : bool
}

/// calculate the total that takes a Customer and a Spend (decimal)
///   as input parameters and returns the Total (decimal) as output
let calculateTotal customer spend =
    let discount =
        if customer.IsEligible && spend >= 100.0M
        then (spend * 0.1M)
        else 0.0M
    spend - discount
 
// Create Customers from the specification
let john = { Id = "John"; IsEligible = true; IsRegistered = true }
let mary = { Id = "Mary"; IsEligible = true; IsRegistered = true }
let richard = { Id = "Richard"; IsEligible = false; IsRegistered = true }
let sarah = { Id = "Sarah"; IsEligible = false; IsRegistered = false }

// Informal assertions as tests - should all evaluate to true
let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.0M = 99.0M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
