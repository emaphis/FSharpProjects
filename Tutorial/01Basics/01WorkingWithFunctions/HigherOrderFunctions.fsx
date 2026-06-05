// Working with Functions
// Higher Order Functions

// Familiar Higher Order Functions


open System

let square x = x * x
//square: x: int -> int

let cube x = x * (square x)
//cube: x: int -> int

let sign x =
    if x > 0 then "positive"
    else if x < 0 then "negative"
    else "zero"
//sign: x: int -> string

let passFive f = (f 5)
// f: (int -> 'a) -> 'a

printfn $"%A{passFive square}"
printfn $"%A{passFive cube}"
printfn $"%A{passFive sign}"


// Creating a Map Function

let map' item converter = converter item
//map': item: 'a -> converter: ('a -> 'b) -> 'b

open System

let map x f = f x
//val map: x: 'a -> f: ('a -> 'b) -> 'b

let cubeAndConvertToString x =
    let temp= (square x) * x
    temp.ToString()
//cubeAndConvertToString: x: int -> string

let answer x =
    if x = true then "yes"
    else "no"
//al answer: x: bool -> string

let first = map 5 square
let second = map 5 cubeAndConvertToString
let third = map true answer


// The Composition Function (<< operator)

let inline (<<) f g x = f (g x)
//val inline (<<) : f: ('a -> 'b) -> g: ('c -> 'a) -> x: 'c -> 'b

open System

let f x = x*x
let g x = -x/2.0 + 5.0

let fog = f << g

Console.WriteLine(fog 0.0) // 25
Console.WriteLine(fog 1.0) // 20.25
Console.WriteLine(fog 2.0) // 16
Console.WriteLine(fog 3.0) // 12.25
Console.WriteLine(fog 4.0) // 9
Console.WriteLine(fog 5.0) // 6.25


let inline (>>) f g x = g (f x)
//val inline (>>) : f: ('a -> 'b) -> g: ('b -> 'c) -> x: 'a -> 'c

let gof = f >> g

Console.WriteLine(gof 0.0) // 5
Console.WriteLine(gof 1.0) // 4.5


// The |> Operator

let inline (|>) x f = f x

//let square x = x * x
let add x y = x + y
let toString x = x.ToString()

let complexFunction' x =
    toString (add 5 (square x))

// improved readability

let complexFunction x =
    x |> square |> add 5 |> toString

let num1 = complexFunction 15


// Anonymous Functions

let complexFunctionAnon =
    2                            (* 2 *)
    |> ( fun x -> x * x)         (* 2 * 2 = 4 *)
    |> ( fun x -> x + 5)         (* 4 + 5 = 9 *)
    |> ( fun x -> x.ToString() ) (* 9.ToString = "9" *)


// A Timer Function

open System

let duration f =
    let timer = new Diagnostics.Stopwatch()
    timer.Start()
    let returnValue = f()
    printfn $"Elapsed Time: %i{timer.ElapsedMilliseconds}"
    returnValue

let rec fib = function
    | 0 -> 0
    | 1 -> 1
    | n -> fib (n - 1) + fib (n - 2)

let main() =
    printfn $"fib 5: %i{duration (fun() -> fib 5)}"
    printfn $"fib 30: %i{duration (fun() -> fib 30)}"

(*
> main();;
Elapsed Time: 0
fib 5: 5
Elapsed Time: 2
fib 30: 832040
val it: unit = ()
*)


// Currying and Partial Functions

open System

// let add x y = x + y

let addFive = add 5

Console.WriteLine(addFive 12)


// How Currying Works

let add' = (fun x -> (fun y -> x + y))
//val add': x: int -> y: int -> int

let add6 = add' 6

let num13 = add6 7


// Two Pattern Matching Syntaxes

// Traditional Syntax
let getPrice food =
    match food with
    | "banana" -> 0.79
    | "watermelon" -> 3.49
    | "tofu" -> 1.09
    | _ -> nan

// Shortcut Syntax
let getPrice2 = function
    | "banana" -> 0.79
    | "watermelon" -> 3.49
    | "tofu" -> 1.09
    | _ -> nan

// Shortcut syntax is converted to
let getPrice3 =
    (fun x ->
        match x with
        | "banana" -> 0.79
        | "watermelon" -> 3.49
        | "tofu" -> 1.09
        | _ -> nan)
