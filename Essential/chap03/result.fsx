// Handling Exception

// reate a function that does simple division but returns an exception if the divisor is 0:

open System

let tryDivide1 (x: decimal) (y: decimal) =
    try
        x/y
    with
    | :? DivideByZeroException as ex -> raise ex


let badDivide1  = tryDivide1 1M 0M
let goodDivide1  = tryDivide1 1M 1M

//exception
//val goodDivide1: decimal = 1M

// The type-system is lying because it doesn't indicate that the function might not return a decimal


// The Result type
type Result'<'TSuccess, 'TFailure> =
    | Success' of 'TSuccess
    | Failure' of 'TFailure


// Use the Result type in our tryDivide function:
let tryDivide (x: decimal) (y: decimal) =
    try
        Ok (x/y)
    with
    | :? DivideByZeroException as ex -> Error ex

// :? - casting operator

let badDivide = tryDivide 1M 0M
let goodDivide  = tryDivide 1M 1M

//exception
//val goodDivide: Result<decimal,DivideByZeroException> = Ok 1M
