(* F# - Tuples *)

// A `tuple` is a comma-separated collection of values.

// Tuple of two integers.
(4, 5)

// Triple of strings
( "one", "two", "three")

// Tuple of unknown type
//( a, b )

// Tuples can have mixed types,
( "Absolute", 1, 2.0 )

// Tuple of integer expressions
let a = 3
let b = 4
( a * 4, b + 7 )
//val it: int * int = (12, 11)


(* Example *)

let averageFour (a, b, c, d) =
    let sum = a + b + c + d
    sum / 4.0

let avg1: float = averageFour (4.0, 5.1, 8.0, 12.0)
printfn $"Avg of four numbers: %f{avg1}"
// Avg of four numbers: 7.275000


(* Accessing Individual Tuple Members *)

(* Example *)

let display tuple1 =
    match tuple1 with
    | a, b, c -> printfn $"Detail Info %A{a} %A{b} %A{c}"

display ("Zara Ali", "Hyderabad", 10)
//Detail Info "Zara Ali" "Hyderabad" 10


(* Example *)
printfn $"First member: %A{fst(23, 30)}"
printfn $"Second member: %A{snd(23, 30)}"

printfn "First member: %A" (fst("Hello", "World!"))
printfn "Second member: %A" (snd("Hello", "World!"))

let nameTuple = ("Zara", "Ali")

printfn $"First Name: %A{fst nameTuple}"
printfn $"Second Name: %A{snd nameTuple}"
