(* F# - Pattern Matching *)

(* Syntax

match expr with
| pat1 -> result1
| pat2 -> result2
| pat3 when expr2 -> result3
| _ -> defaultResult

where:

- Each | symbol defines a condition.
- The -> symbol means "if the condition is true, return this value...".
- The _ symbol provides the default pattern, meaning that it matches all other things like a wildcard.
*)


(* Example 1 *)

let rec fib n =
    match n with
    | 0 -> 0
    | 1 -> 1
    | _ -> fib (n - 1)  + fib (n - 2)

for i = 1 to 10 do
    printfn $"Fibonacci %d{i}: %d{fib i}"


(* Example 2 *)

let printSeason month =
   match month with
   | "December" | "January" | "February" -> printfn "Winter"
   | "March" | "April" -> printfn "Spring"
   | "May" | "June" -> printfn "Summer"
   | "July" | "August" -> printfn "Rainy"
   | "September" | "October" | "November" -> printfn "Autumn"
   | _ -> printfn "Season depends on month!"

printSeason "February"
printSeason "April"
printSeason "November"
printSeason "July"


(* Pattern Matching Functions

F# allows you to write pattern matching functions using the 'function' keyword
*)

let getRate = function
   | "potato" -> 10.0
   | "brinjal" -> 20.50
   | "cauliflower" -> 21.00
   | "cabbage" -> 8.75
   | "carrot" -> 15.00
   | _ -> nan

printfn $"""%g{getRate "potato"}"""
printfn $"""%g{getRate "brinjal"}"""
printfn $"""%g{getRate "cauliflower"}"""
printfn $"""%g{getRate "cabbage"}"""
printfn $"""%g{getRate "carrot"}"""


(* Adding Filters or Guards to Patterns

You can add filters, or guards, to patterns using the when keyword.
*)

(* Example 1 *)

let sign = function
   | x when x < 0 -> -1
   | x when x > 0 -> 1
   | _ -> 0

printfn $"%d{sign -20}"
printfn $"%d{sign 20}"
printfn $"%d{sign 0}"


(* Example 2 *)

let compareInt x =
    match x with
    | var1, var2  when var1 > var2 ->
        printfn $"%d{var1} is greater than %d{var2}"
    | var1, var2 when var1 < var2 ->
        printfn $"%d{var1} is less than %d{var2}"
    | var1, var2 ->
        printfn $"%d{var1} equals %d{var2}"

compareInt (11,25)
compareInt (72, 10)
compareInt (0, 0)


(* Pattern Matching with Tuples *)

let greeting (name, subject) =
    match name, subject with
    | "Zara", _ ->
        "Hello, Zara"
    | name, "English" ->
        "Hello, " + name + " from the department of English"
    | name, _ when subject.StartsWith "Comp"  ->
        "Hello, " + name + " from the department of Computer Sc."
    | _, "Accounts and Finance" ->
         "Welcome to the department of Accounts and Finance!"
    | _ -> "You are not registered into the system"


printfn $"""%s{greeting ("Zara", "English")}"""
printfn $"""%s{greeting ("Raman", "Computer Science")}"""
printfn $"""%s{greeting ("Ravi", "Mathematics")}"""


(* Pattern Matching with Records *)

type Point = { x: float; y: float }

let evaluatePoint (point: Point) =
    match point with
    | { x = 0.0; y = 0.0} ->
        printfn "Point is at the origin."
    | { x = xVal; y = 0.0 } ->
        printfn $"Point is on the ax-axis. Value is %f{xVal}."
    | { x = 0.0; y = yVal } ->
        printfn $"Point is on the y-axis. Value is %f{yVal}."
    | { x = xVal; y = yVal } ->
        printfn $"Point is at (%f{xVal}, %f{yVal})."

evaluatePoint { x = 0.0; y = 0.0 }
evaluatePoint { x = 10.0; y = 0.0 }
evaluatePoint { x = 0.0; y = 10.0 }
evaluatePoint { x = 10.0; y = 10.0 }
