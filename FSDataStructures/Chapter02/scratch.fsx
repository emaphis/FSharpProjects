// Scratch for chapter 2


printfn "Hello World"

let square = fun n -> n * n

// Simpler syntax
let square' n = n * n

let out1 = square' 4

let cubeMe x = x * x * x

cubeMe 9

let areaCircle r =
   let square r = r * r
   System.Math.PI * square r

areaCircle 8.0

// Conditionals
let mod10 n =
   if n % 10 = 0 then
      printfn "Number ends in 0"
   else
      printfn "Number does not end in zero"

let res2 = mod10 100

// tuples
let t = ("cats", "dogs", System.Math.PI,42, "C#", "Java")

// arrays
let GuardiansOfGalaxy = [| "Peter Quill";
   "Gamora"; "Drax"; "Groot"; "Rocket";
   "Ronan"; "Yondu Udonta"; "Nebula";
   "Korath"; "Corpsman Dey";"Nova Prime";"The
   Collector";"Meredith Quill" |]

// The individual elements of the array can be accessed as follows:
let iAmGroot = GuardiansOfGalaxy[4]

// Individual elements of a string.
let str = "Lǎo péngyǒu, nǐ kànqǐlái hěn yǒu jīngshén."
printfn $"%c{str[9]}"

// Ranges and slices
let OneToHundred = [|1..100|]
let TopThree = OneToHundred[0..2]
// int array = [|1; 2; 3|]

// functions
let add x y = x + y
(add 10) 4

let Add10 = add 10
Add10 42

// Higher-order functions
let increment n = n + 1
let divideByTwo n = n / 2

let invokeThrice n (f:int->int) =
   f(f(f(n)))

let res3 = invokeThrice 6 increment

let res4 = invokeThrice 80 divideByTwo

// lambda expressions

let invokeThrice1 n (f: double -> double) =
   f(f(f(n)))

let res5 = invokeThrice1 2.0 (fun n -> n ** 3.0)

// map, fold, filter, zip
let nums = [|0..99|]

let squares1 =
   nums
   |> Array.map (fun n -> n * n)

let sum1 = Array.fold(fun acc n -> acc + n) 0 squares1

let castNames = [| "Hofstadter"; "Cooper";
   "Wolowitz"; "Koothrappali"; "Fowler"; "Rostenkowski"; |]

let longNames =
   Array.filter (fun (name: string) -> name.Length > 6) castNames

let firstNames = [| "Leonard"; "Sheldon"; "Howard";
     "Penny"; "Raj"; "Bernadette"; "Amy" |]
let lastNames = [| "Hofstadter"; "Cooper"; "Wolowitz";
   "Koothrappali"; ""; "Rostenkowski"; "Fowler" |]

let fullNames = Array.zip(firstNames) lastNames

//  another salient feature of F# language is Lazy or delayed evaluation.

let divide x y =
   printfn "dividing %d by %d" x y
   x / y

let answer = lazy(divide 8 2)

printfn $"%d{answer.Force()}"


//* Syntactical similarities and differences *

let square1 x = x * x
let sumOfSquares n =
   [ 1 .. n ]
   |> List.map square'
   |> List.sum

sumOfSquares 2
sumOfSquares 3

(*
Following is
problem #1 from Project Euler:

   If we list all the natural numbers below 10 that
   are multiples of 3 or 5, we get 3, 5, 6 and 9.
   The sum of these multiples is 23 Find the sum
   all the multiples of 3 or 5 below 1000.
 *)

let total1 =
   [ 1 .. 999 ]
   |> List.map (fun i ->
      if i % 5 = 0 || i % 3 = 0 then i else 0)
   |> List.sum

// Or

let total2 =
   [1..999]
   |> List.filter (fun i ->
      i % 5 = 0 || i % 3 = 0)
   |> List.sum
