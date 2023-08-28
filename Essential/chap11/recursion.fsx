// 11 - Recursion

// Solving the Problem

// 5! = 5 * 4 * 3 * 2 * 1 = 120

/// A naive implementation of the factorial function (!)
let rec fact n =
    match n with
    | 1 -> 1
    | n -> n * fact (n-1)

120 = fact 5

// Tail Call Optimisation

// We are going to use an accumulator. The accumulator is passed around the
// recursive function on each iteration:

/// Factorial using tail call
let fact' n =
    let rec loop n acc=
        match n with
        | 1 -> acc
        | _ -> loop (n-1) (acc*n)
    loop n 1

120L = fact' 5

1409286144L = fact' 30


// Expanding the Accumulator

// The Fibonacci Sequence
// 0, 1, 1, 2, 3, 5, 8, 13, 21, ...

/// A naive example to calculate the nth item in the sequence
let rec fib (n: int64) =
    match n with
    | 0L -> 0L
    | 1L -> 1L
    | s ->  fib (s-1L) + fib (s-2L)

21L = fib 8L
102334155L = fib 40L
//12586269025L = fib 50L

// with accumulator

/// A more efficient version of fibonacci that uses tail call optimisation:
let fib' (n: int64) =
    let rec loop n (a, b) =
        match n with
        | 0L -> a
        | 1L -> b
        | n  -> loop (n-1L) (b, b+a)
    loop n (0L, 1L)

21L = fib' 8L
102334155L = fib' 40L
12586269025L = fib' 50L


// Using Recursion to Solve FizzBuzz

let mapping = [ (3, "Fizz"); (5, "Buzz") ]

/// fizzbuzz function is using tail call optimisation and has an accumulator
/// that will use string concatenation and an initial value of an empty
/// string ("").
let fizzBuzz initialMapping n =
    let rec loop mapping acc =
        match mapping with
        | []    -> if acc = "" then string n else acc
        | head::tail ->
            let value =
                head |> (fun (div, msg) -> if n % div = 0 then msg else "")
            loop tail (acc + value)
    loop initialMapping ""

[ 1 .. 105 ]
|> List.map (fizzBuzz mapping)
|> List.iter (printfn "%s")


let mapping2 = [ (3, "Fizz"); (5, "Buzz"); (7, "Bazz") ]

[ 1 .. 105 ]
|> List.map (fizzBuzz mapping2)
|> List.iter (printfn "%s")


/// Use the List.fold function to solve FizzBuzz
let fizzBuzz2 n =
    [ (3, "Fizz"); (5, "Buzz")]
    |> List.fold (fun acc (div, msg) ->
        if n % div = 0 then acc + msg else acc) ""
    |> fun s -> if s = "" then string n else s

[1 .. 105]
|> List.iter (fizzBuzz2 >> printfn "%s")


/// Do all of the mapping in the fold function rather than passing the value on
/// to another function:
let fizzBuzz3 n =
    [ (3, "Fizz"); (5, "Buzz") ]
    |> List.fold (fun acc (div, msg) ->
        match (if n % div = 0 then  msg else "") with
        | "" -> acc
        | s  -> if acc = string n then s else acc + s) (string n) 

[1..105] 
|> List.iter (fizzBuzz3 >> printfn "%s")


// Quicksort using recursion

// Quicksort is a nice algorithm to create in F# because of the availability of
// some very useful collection functions in the List module:

/// Quick sort in F#
let rec qsort input =
    match input with
    | [] -> []
    | head::tail ->
        let smaller, larger = List.partition (fun n -> head >= n) tail
        List.concat [qsort smaller; [head]; qsort larger]

[5;9;5;2;7;9;1;1;3;5] |> qsort |> printfn "%A"
// [1; 1; 2; 3; 5; 5; 5; 7; 9; 9]
