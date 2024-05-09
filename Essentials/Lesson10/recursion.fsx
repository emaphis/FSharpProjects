// Lesson 10  - Recursion

// Basics

let rec sumA n =
    match n with
    | 1 -> 1
    | _ -> n + sumA (n-1)

let total1 = sumA 10
total1 = 55


// Tail Call

[<TailCall>]
let rec sumT n acc =
    match n with
    | 0 -> acc
    | _ -> sumT (n-1) (acc+n)

let totalA = sumT 10 0


let sum n =
    let rec loop n acc =
        match n with
        | 0 -> acc
        | _ -> loop (n-1) (acc+n)
    
    loop n 0

let totalB = sum 10
    

let sumL input =
    let rec loop input acc =
        match input with
        | [] -> acc
        | x::xs -> loop xs (acc + x)
    
    loop input 0

let totalC = sumL [1..10]


// FizzBuzz

let calculate0 n =
    [(3, "Fizz"); (5, "Buzz")]
    |> List.map (fun (v, s) ->
            if n% v = 0 then s else "")
    |> List.reduce (+)
    |> fun s -> if s <> "" then s else string n


let calculate n =
    let rec loop rem str =
        match rem with
        | []  -> if str <> "" then str else string n
        | (div, msg)::tail ->
            loop tail (str + if n % div = 0
                             then msg
                             else "")

    loop [(3, "Fizz"); (5, "Buzz")] ""


let run =
    [1..20]
    |> List.map calculate
