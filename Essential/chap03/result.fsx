// Handling Exception

open System


let tryDivide1 (x: decimal) (y: decimal) =
    try
        x/y
    with
    | :? DivideByZeroException as ex -> raise ex



let tryDivide (x: decimal) (y: decimal) =
    try
        Ok (x/y)
    with
    | :? DivideByZeroException as ex -> raise ex

// :? - casting operator

let badDivide : Result<decimal,DivideByZeroException>  = tryDivide 1M 0M

let goodDivide : Result<decimal,DivideByZeroException>  = tryDivide 1M 1M
