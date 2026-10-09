(* F# - Arrays *)

(* Creating Arrays
    By listing consecutive values between [| and |] and separated by semicolons.
    By putting each element on a separate line, in which case the semicolon separator is optional.
    By using sequence expressions.
*)

// using semicolon separator
let array1 = [| 1; 2; 3; 4; 5; 6 |]

for i in 0 .. array1.Length - 1 do
    printf $"%d{array1[i]} "
printfn ""

// without semicolon separator
let array2 =
    [|
      1
      2
      3
      4
      5
    |]

for i in 0 .. array2.Length - 1 do
    printf $"%d{array2[i]} "
printfn ""


// using sequences
let array3 = [| for i in 1 .. 10 -> i * i |]

for i in 0 .. array3.Length - 1 do
    printf $"%d{array3[i]} "
printfn ""


(* Basic Operations on Arrays *)

(* Creating *)

(* using create and set *)
let array4 = Array.create 10 ""
for i in 0 .. array4.Length - 1 do
    Array.set array4 i (i.ToString())

for i in 0 .. array4.Length - 1 do
    printf $"%s{Array.get array4 i} "
printfn ""

(* empty array *)
let array5 = Array.empty
printfn $"Length of empty array: %d{array5.Length}"

let array6 = Array.create 10 7.0
printfn $"Float Array: %A{array6}"

(* using the init and zeroCrate *)
let array7 = Array.init 10 (fun index -> index * index)
printfn $"Array of squares: %A{array7}"

let array8 : float array = Array.zeroCreate 10
printfn $"Float Array: %A{array8}"

let myZeroArray : float array = Array.zeroCreate 10
for i in 0 .. myZeroArray.Length - 1 do
    printf $"%g{myZeroArray[i]} "
printfn ""


(* Example 2 *)

(* creating subarray from element 5 *)
(* containing 15 elements thereon *)

let array10 = [| 1 .. 50 |]
let array11 = Array.sub array10 5 15
printfn $"Sub Array:"
printfn $"%A{array11}"

(* appending two arrays *)
let array12 = [| 1; 2; 3; 4 |]
let array13 = [| 5 .. 9 |]
printfn "Append Array:"
let array14 = Array.append array12 array13
printfn $"%A{array14}"


(* using the Choose function *)
let array15 = [| 1 .. 20 |]

let array16 = (Array.choose (fun elem ->
   if elem % 3 = 0 then
        Some(float elem)
   else
        None) array15)

printfn "Array with Chosen elements:"
printfn $"%A{array16}"


(*using the Collect function *)
let array18 = [| 2 .. 5 |]
let array19 = Array.collect (fun elem -> [| 0 .. elem - 1 |]) array18
printfn "Array with collected elements:"
printfn $"%A{array19}"


(* Searching Arrays *)

let array21 = [| 2 .. 100 |]
let delta = 1.0e-10

let isPerfectSquare (x:int) =
   let y = sqrt (float x)
   abs(y - round y) < delta

let isPerfectCube (x:int) =
   let y = System.Math.Pow(float x, 1.0/3.0)
   abs(y - round y) < delta

let element = Array.find (fun elem -> isPerfectSquare elem && isPerfectCube elem) array21

let index = Array.findIndex (fun elem -> isPerfectSquare elem && isPerfectCube elem) array21

printfn "The first element that is both a square and a cube is %d and its index is %d." element index
