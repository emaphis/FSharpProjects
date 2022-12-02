// Going further - make Elibility a domain concept

type RegisteredCustomer = {
    Id : string
}

type UnregisteredCustomer = {
    Id : string
}

// Disctriminated union
type Customer =
    | Eligible of RegisteredCustomer
    | Registered of RegisteredCustomer
    | Guest of UnregisteredCustomer




let calculateTotal customer spend =
    let discount =
        match customer with
        | Eligible c when spend >= 100.0M -> spend * 0.1M
        | _ -> 0.0M
    spend - discount


let john = Eligible { Id = "John" }
let mary = Eligible { Id = "Mary"; }
let richard = Registered { Id = "Richard" }
let sarah = Guest { Id = "Sarah" }

let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.0M = 99.0M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
