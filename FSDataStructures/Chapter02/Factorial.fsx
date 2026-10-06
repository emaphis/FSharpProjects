// Factorial examples

/// iterative factorial
let factorialIterative x =
    let mutable n = x
    let mutable returnVal = 1
    while n >= 1 do
        returnVal <- returnVal * n
        n <- (n - 1)
    returnVal


/// recursive factorial
let rec factorialRec n =
    if n < 1 then 1
    else n * factorialRec (n - 1)

/// Pattern matching factorial
let rec factorial_PatternMatching n =
    match n with
    | 0 | 1 -> 1
    | _ -> n * factorial_PatternMatching(n-1)

/// tail-recursive factorial
let rec factorialTC n =
    let rec loop n acc =
        if n <= 1 then acc
        else
            loop (n - 1) (n * acc)

    loop n 1


/// Continuation based factorial
let factorialCont n =
    let rec cont n fn =
        if n <= 1 then fn()
        else
            cont (n - 1) (fun () -> n * fn())

    cont n (fun () -> 1)


/// Factorial using fold
let factorialFold n =
    [ 1 .. n]
    |> List.fold ( * ) 1

/// using reduce
let factorialRed n =
    [ 1 .. n ]
    |> List.reduce ( * )
