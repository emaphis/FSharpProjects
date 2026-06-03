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


let getPriceTR taxRate = function
    | "banana" -> 0.79 * (1.0 + taxRate)
    | "watermelon" -> 3.49 * (1.0 + taxRate)
    | "tofu" -> 1.09 * (1.0 + taxRate)
    | _ -> nan (* nan is a special value meaning "not a number" *)

getPriceTR 0.10 "tofu"
getPriceTR 0.10 "banana"
getPriceTR 0.10 "apple"


// Binding Variables with Pattern Matching

// F# can automatically bind values to identifiers if they match certain patterns. 
let rec factorial = function
    | 0 | 1 -> 1
    | n -> n * factorial (n - 1)

factorial 7


// Using Guards within Patterns

let sign = function
    | 0  -> 0
    | x when x < 0 -> -1
    | x when x > 0 -> 1

sign -55
sign 108
sign 0

