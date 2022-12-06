// 11 - Recursion

// Solving the Problem

// 5! = 5 * 4 * 3 * 2 * 1 = 120

let rec fact n =
    match n with
    | 1 -> 1
    | n -> n * fact (n-1)

120 = fact 5

// Tail Call Optimisation

let fact' n =
    let rec loop n acc=
        match n with
        | 1 -> acc
        | _ -> loop (n-1) (acc*n)
    loop n 1

120 = fact' 5

fact' 30


// Expanding the Accumulator

// fibo - 0, 1, 1, 2, 3, 5, 8, 13, 21, ...

let rec fib (n: int64) =
    match n with
    | 0L -> 0L
    | 1L -> 1L
    | s ->  fib (s-1L) + fib (s-2L)

21L = fib 8L

// with accumulator

let fib' (n: int64) =
    let rec loop n (a, b) =
        match n with
        | 0L -> a
        | 1L -> b
        | n  -> loop (n-1L) (b, b+a)
    loop n (0L, 1L)

21L = fib' 8L
12586269025L = fib' 50L


// Using Recursion to Solve FizzBuzz

let mapping = [ (3, "Fizz"); (5, "Buzz") ]

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


// Using List.fold

let fizzBuzz2 n =
    [ (3, "Fizz"); (5, "Buzz")]
    |> List.fold (fun acc (div, msg) ->
        if n % div = 0 then acc + msg else acc) ""
    |> fun s -> if s = "" then string n else s

[1 .. 105]
|> List.iter (fizzBuzz2 >> printfn "%s")


let fizzBuzz3 n =
    [ (3, "Fizz"); (5, "Buzz") ]
    |> List.fold (fun acc (div, msg) ->
        match (if n % div = 0 then  msg else "") with
        | "" -> acc
        | s  -> if acc = string n then s else acc + s) (string n) 


[1..105] 
|> List.iter (fizzBuzz3 >> printfn "%s")


// Quicksort using recursion

let rec qsort input =
    match input with
    | [] -> []
    | head::tail ->
        let smaller, larger = List.partition (fun n -> head >= n) tail
        List.concat [qsort smaller; [head]; qsort larger]

[5;9;5;2;7;9;1;1;3;5] |> qsort |> printfn "%A"
