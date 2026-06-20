// Object Oriented Programming
// Operator Overloading

// Using Operators
let x = 3
let y = 4
let sum = x + y

// (+) is an operator


// Operator Overloading

type Complex =
    { Re: double
      Im: double }
    static member (+) (left: Complex, right: Complex) =
        { Re = left.Re + right.Re; Im = left.Im + right.Im }

let first = { Re = 1.0; Im = 7.0 }
let second = { Re = 2.0; Im = -10.5 }

let third = first + second


// Defining New Operators

// ! % & * + - . / <=> ? @ ^ | ~
// infix oporators and prefix operators

(*
let (op) arg1 arg2 = ...
*)


open System.Text.RegularExpressions

let (=~) (input: string) pattern =
    Regex.IsMatch(input, pattern)

let main1() =
    printfn "cat =~ dog: %b" ("cat" =~ "dog")
    printfn "cat =~ cat|dog: %b" ("cat" =~ "cat|dog")
    printfn "monkey =~ monk*: %b" ("monkey" =~ "monk*")

main1()


// Prefix Operators

let ( !+ ) l = List.reduce ( + ) l
let ( !- ) l = List.reduce ( - ) l
let ( !* ) l = List.reduce ( * ) l
let ( !/ ) l = List.reduce ( / ) l

let int1 = !* [2; 3; 5]
let int2 = !+ [2; 3; 5]
let int3 = !- [2; 3; 5]
let int4 = !/ [100; 10; 2]
