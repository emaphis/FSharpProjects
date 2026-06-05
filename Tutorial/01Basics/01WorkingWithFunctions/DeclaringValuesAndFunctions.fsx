// Working with Functions
// Declaring Values and Functions

// Declaring Variables

let x = 5
let y = 10

let z = x + y

printfn $"x: %i{x}"
printfn $"y: %i{y}"
printfn $"z: %i{z}"


// Declaring Functions

let add x y = x + y

let sub x y = x - y

let z' = add 5 10

let printThreeNumbers num1 num2 num3=
    printfn $"num1 : %i{num1}"
    printfn $"num2 : %i{num2}"
    printfn $"num3 : %i{num3}"

printThreeNumbers 5 (add 10 7) (sub 20 8)


// Function Return Values

let sign num =
    if num > 0 then "positive"
    elif num < 0 then "negative"
    else "zero"

let str0 = sign -15

let helloWorld () = printfn "hello world!"
// val helloWorld: unit -> unit

helloWorld ()


// How to Read Arrow Notation

let addAndMakeString x y = (x + y).ToString()
// val addAndMakeString: x: int -> y: int -> string

addAndMakeString x y
// val it: string = "15"


// Partial Function Application

let addTwoNumbers x y = x + y
//  int -> y: int -> int

let add5ToNumber = addTwoNumbers 5

let num0 = add5ToNumber 3
// 8


// Nested Functions

let sumOfDivisors n =
    let rec loop current max acc =
        if current > max then
            acc
        else
            if n % current = 0 then
                loop (current + 1) max (acc + current)
            else
                loop (current + 1) max acc
    let start = 2
    let max = n / 2     (* largest factor, apart from n, cannot be > n / 2 *)
    let minSum = 1 + n (* 1 and n are already facotrs of n *)
    loop start max minSum


printfn $"{sumOfDivisors 10}"
(* prints 18, because the sum of 10's divisors is 1 + 2 + 5 + 10 = 18 *)


// Generic Functions

let giveMeAThree _ = 3
// val giveMeAThree: x: 'a -> int

let throwAwayFirstInput x y = y
// 'a -> y: 'b -> 'b

throwAwayFirstInput "thrownAway" 10.0
//val it: float = 10.0

throwAwayFirstInput 5 30
//val it: int = 30

// Generic functions are strongly typed.
let z'' = add 10 (throwAwayFirstInput "this is a string" 5)
//val z'': int = 15
