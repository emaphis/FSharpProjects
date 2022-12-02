(*
Feature: Applying a discount
Scenario: Eligible Registered Customers get 10% discount when they
          spend £100 or more

Given the following Registered Customers
|Customer Id|Is Eligible|
|John       |true       |
|Mary       |true       |
|Richard    |false      |

When [Customer Id] spends [Spend]
Then their order total will be [Total]

Examples:
|Customer Id|   Spend|   Total|
|Mary       |   99.00|   99.00|
|John       |  100.00|   90.00|
|Richard    |  100.00|  100.00|
|Sarah      |  100.00|  100.00|

Notes:
Sarah is not a Registered Customer
Only Registered Customers can be Eligible
*)

// GettignStarted

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
 

let john = { Id = "John"; IsEligible = true; IsRegistered = true }
let mary = { Id = "Mary"; IsEligible = true; IsRegistered = true }
let richard = { Id = "Richard"; IsEligible = false; IsRegistered = true }
let sarah = { Id = "Sarah"; IsEligible = false; IsRegistered = false }
 
let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.0M = 99.0M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
