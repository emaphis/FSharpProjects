(* F# - Functions *)

(* Defining a Function *)

//let doubleIt (x: int) = 2 * x

(* Calling a Function *)

//let dbl = doubleIt 4

(* Example 1 *)

/// The function calculates the volume of
/// a cylinder with radius and length as parameters
let cylinderVolume radius length : float =

   // function body
   let pi = 3.14159
   length * pi * radius * radius


let vol = cylinderVolume 3.0 5.0
printfn $" Volume: %g{vol}"


//  Volume: 141.372


(* Example 2 *)

/// The function returns the larger value of two argument
let max num1 num2 : int32 =
    // function body
    if num1 > num2 then
        num1
    else
        num2

let res1 = max 39 52
printfn $" Max Value: %d{res1}"

//   Max Value: 52


(* Example 3 *)

let doubleIt (x: int) = 2 * x
printfn $"Double 19: %d{doubleIt 19}"


(* Recursive Functions *)

/// Recursive function definition
let rec fib n =
  if n < 2 then 1 else fib (n - 1) + fib (n - 2)

for i = 1 to 15 do
    printfn $"Fibonacci %d{i} %d{fib i}"


(* Example *)

open System

let rec fact x =
    if x < 1 then 1
    else x * fact (x - 1)

Console.WriteLine(fact 8)
// 40320


(* Arrow Notations in F# *)

let myDivFunction x y = (x / y).ToString()
//val myDivFunction: x: int -> y: int -> string


(* Lambda Expressions *)

let applyFunction ( f: int -> int -> int ) x y = f x y
let mul x y = x * y

let res2 = applyFunction mul 5 7
printfn $"%d{res2}"
// 35

// Passing a lambda function

let res3 = applyFunction (fun x y -> x * y) 5 7
printfn $"%d{res2}"


(* Function Composition and Pipelining *)

let function1 x = x + 1
let function2 x = x * 5

let f = function1 >> function2
let res4 = f 10
printfn $"%d{res4}"
// 55

let res5 = 10 |> function1 |> function2
printfn $"%d{res5}"
// 55
