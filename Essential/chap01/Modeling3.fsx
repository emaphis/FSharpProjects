// Going further - make Elibility a domain concept

type Customer =
    | Eligible of Id : string
    | Registered of Id : string
    | Guest of Id : string

let calculateTotal customer spend =
    let discount =
        match customer with
        | Eligible c when spend >= 100.0M -> spend * 0.1M
        | _ -> 0.0M
    spend - discount


let john = Eligible "John"
let mary = Eligible "Mary"
let richard = Registered "Richard"
let sarah = Guest "Sarah"

let assertJohn = (calculateTotal john 100.0M = 90.0M)
let assertMary = (calculateTotal mary 99.0M = 99.0M)
let assertRichard = (calculateTotal richard 100.0M = 100.0M)
let assertSarah = (calculateTotal sarah 100.0M = 100.0M)
