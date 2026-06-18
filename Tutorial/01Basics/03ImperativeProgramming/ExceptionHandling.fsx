// Imperative Programming
// Exception Handling


// Try/With

let getNumber' msg =
    printf msg
    int32(System.Console.ReadLine())

let x' = getNumber' "x = "
let y' = getNumber' "y = "
printfn $"%i{x'} + %i{y'} = %i{x' + y'}"


// with exceptions

let getNumber msg =
    printf msg;
    try
        int32(System.Console.ReadLine())
    with
        | :? System.FormatException -> System.Int32.MinValue

let x = getNumber "x = "
let y = getNumber "y = "
printfn $"%i{x} + %i{y} = %i{x + y}"

// the System.Int32.Parse(s : string) method will throw three types of exceptions:

// ArgumentNullException
// Occurs when s is a null reference.

// FormatException
// Occurs when s does not represent a numeric input.

// OverflowException
// Occurs when s represents number greater than or less than
// Int32.MaxValue or Int32.MinValue (i.e. the number cannot be
// represented with a 32-bit signed integer).

open System

let getNumber2 msg =
    printf msg;
    try
        int32(Console.ReadLine())
    with
        | :? FormatException  -> -1
        | :? OverflowException -> Int32.MinValue
        | :? ArgumentNullException -> 0


// Raising Exception

(* General failure *)
// val failwith : string -> 'a

(* General failure with formatted message *)
// val failwithf : StringFormat<'a, 'b> -> 'a

(* Raise a specific exception *)
// val raise : #exn -> 'a

(* Bad input *)
// val invalidArg : string -> string -> 'a


// example

type 'a tree =
    | Node of 'a * 'a tree * 'a tree
    | Empty

let rec add x = function
    | Empty  -> Node (x, Empty, Empty)
    | Node(y, left, right) ->
        if x > y then Node (y, left, add x right)
        elif x < y then Node(y, add x left, right)
        else failwith $"item '%A{x} has already been added to tree"



// Try/Finally

let tryWithFinallyExample f =
    try
        printfn "tryWithFinallyExample: outer try block"
        try
            printfn "tryWithFinallyExample: inner try block"
            f()
        with
            | exn ->
                printfn "tryWithFinallyExample: inner with block"
                reraise() (* raises the same exception we just caught *)
    finally
        printfn "tryWithFinally: outer finally block"

let catchAllExceptions f =
    try
        printfn "-------------"
        printfn "catchAllExceptions: try block"
        tryWithFinallyExample f
    with
        | exn ->
            printfn "catchAllExceptions: with block"
            printfn $"Exception message: %s{exn.Message}"


let main1() =
        catchAllExceptions (fun () -> printfn "Function executed successfully")
        catchAllExceptions (fun () -> failwith "Function executed with an error")


main1()
