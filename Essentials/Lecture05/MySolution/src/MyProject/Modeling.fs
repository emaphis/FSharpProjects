namespace Modeling

type Customer =
    | Registered of Id:string * isEligible:bool
    | Unregistered of Name:string

type Spend = decimal
type Total = decimal

type CalculateTotal = (Customer * Spend) -> Total


module Customer =

    let calculateTotal (customer: Customer, spend: Spend) : Total =
        let discount =
            match customer with
            | Registered (_, true) when spend >= 100m -> spend * 0.1m
            | Registered (_, _) -> 0m
            | Unregistered _ -> 0m

        spend - discount
