// Immutable Data Structures
// Option Types


// Using Option Types

let div x y = x / y


div 10 5
//div 10 0

let safediv x y =
    match y with
    | 0 -> None
    | _ -> Some (x/y)

safediv 10 5
safediv 10 0

//val div: x: int -> y: int -> int
//val safediv: x: int -> y: int -> int option


// Pattern Matching Option Types

let isFortyTwo = function
    | Some(42)  -> true
    | Some(_) | None -> false


isFortyTwo (Some(43))
isFortyTwo(Some(42))
isFortyTwo None


// Other Functions in the Option Module

None.IsNone
Some(42).IsNone

None.IsSome
Some(42).IsSome
