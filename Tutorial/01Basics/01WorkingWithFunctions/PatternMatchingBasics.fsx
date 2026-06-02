// Working with Functions
// Pattern Matching Basics


// Pattern Matching Syntax

(*

match expr with
| pat1 -> result1
| pat2 -> result2
| pat3 when expr2 -> result3
| _ -> defaultResult

*)

/// calculate the nth Fibonacci number using pattern matching syntax
let rec fib n =
    match n with
    | 0  -> 0
    | 1  -> 1
    | _  -> fib (n - 1) + fib (n - 2)

let f1 = fib 1
let f2 = fib 2
let f5 = fib 5
let f10 = fib 10

// t's possible to chain together multiple conditions which return the same value.

let greeting name =
    match name with
    | "Steve" | "Kristina" | "Matt" -> "Hello!"
    | "Carlos" | "Maria" -> "Hola!"
    | "Worf" -> "nuqneH!"
    | "Pierre" | "Monique" -> "Bonjour!"
    | _ -> "DOES NOT COMPUTE!"

greeting "Monique"
greeting "Pierre"
greeting "Kristina"
greeting "Sakura"


// Alternative Pattern Matching Syntax

(*
let something = function
| test1 -> value1
| test2 -> value2
| test3 -> value3
*)

let getPrice = function
    | "banana" -> 0.79
    | "watermelon" -> 3.49
    | "tofu" -> 1.09
    | _ -> nan (* nan is a special value meaning "not a number" *)

getPrice "tofu"
getPrice "banana"
getPrice "apple"


