// Working with Functions
// Recursion and Recursive Functions

(*
    let rec someFunction = ...
*)

// Examples

// Factorial in F#

open System

let rec fact x =
    if x < 1 then 1
    else x * fact (x - 1)

(* 
    // can also be written using pattern matching syntax:
    let rec fact = function
        | n when n < 1 -> 1
        | n -> n * fact (n - 1)
*)

Console.WriteLine(fact 6)


// Greatest Common Divisor (GCD)

let rec gcd x y =
    if y = 0 then x
    else gcd y (x % y)

Console.WriteLine(gcd 259 111)


// Tail Recursion

let rec count' n =
    if n = 1000000 then
        printfn "done"
    else
        if n % 1000 = 0 then
            printfn $"n: {n}"

        count' (n + 1)  (* recursive call *)
        ()


// properly tail-recursive:

let rec count n =
    if n = 1000000 then
        printfn "done"
    else
        if n % 1000 = 0 then
            printfn $"n: {n}"

        count (n + 1) (* recursive call *)


// How to Write Tail-Recursive Functions

let rec slowMultiply' a b =
    if b > 1 then
        a + slowMultiply' a (b - 1)
    else a


let rec slowMultiply'' a b =
    if b > 1 then
        let intermediate = slowMultiply'' a (b - 1)  (* recursion *)
        let result = a + intermediate  (* <-- additional operations *)
        result
    else a


// tail recursive

let slowMultiply a b =
    let rec loop acc counter =
        if counter > 1 then
           loop (acc + a) (counter - 1)  (* tail recursive *)
        else acc
    loop a b


open System.Numerics

let fib n =
    let rec loop a b i =
        match i with
        | n when n=0I -> a
        | n when n=1I -> b
        | n -> loop b (a + b) (i - 1I)
    loop 0I 1I n
