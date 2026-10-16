(* F# - Exception Handling *)

(*  F# exception handling provides the following constructs −

  Construct                    Description

`raise expr`                   Raises the given exception.
`failwith expr`                Raises the System.Exception exception.
`try expr with rules`          Catches expressions matching the pattern rules.
`try expr finally expr`        Execution the `finally` expression both when the
                               computation is successful and when an exception is
                               raised.
`| :? ArgumentException`       A rule matching the given .NET exception type.
`| :? ArgumentException as e`  A rule matching the given .NET exception type, binding
                               the name e to the exception object value.
`| Failure(msg) → expr`        A rule matching the given data-carrying F# exception.
`| exn → expr`                 A rule matching any exception, binding the name exn
                               to the exception object value.
`| exn when expr → expr`       A rule matching the exception under the given
                               condition, binding the name exn to the exception object
                               value.
*)

(* Syntax
`exception exception-type of argument-type`

Where:

* exception-type is the name of a new F# exception type.

* argument-type represents the type of an argument that can be supplied when you raise an exception to this type.

* Multiple arguments can be specified by using a tuple type for argument-type.

try
   expression1
with
   | pattern1 -> expression2
   | pattern2 -> expression3
...

try
   expression1
finally
   expression2

*)

(* Example of Exception Handling *)

let divisionProg x y =
    try
       Some (x/y)
    with
       | :? System.DivideByZeroException ->
           printfn "Division by zero!"; None


let result1 = divisionProg 100 0


(* Example 2 *)

exception Error1 of string
// Using a tuple type as the argument type.
exception Error2 of string * int

let myFunction x y =
    try
        if x = y then raise (Error1 "Equal Number Error")
        else raise (Error2 ("Error Not detected", 100))
    with
        | Error1 str -> printfn $"Error1 %s{str}"
        | Error2 (str, i) -> printfn $"Error2 %s{str} %d{i}"


myFunction 20 10
myFunction 5 5


(* Example 3 *)

exception InnerError of string
exception OuterError of string

let func1 x y =
    try
       try
           if x = y then raise (InnerError "inner error")
           else raise (OuterError "outer error")
       with
           | InnerError str -> printfn $"Error:%s{str}"
    finally
       printfn "From the finally block."


let func2 x y =
   try
      func1 x y
   with
      | OuterError str -> printfn $"Error: %s{str}"


func2 100 150
func2 100 100
func2 100 120


(* Example 4 *)

let divisionFunc x y =
    if y = 0 then failwith "Divisor cannot be zero."
    else
        x / y

let tryDivisionFunc x y =
    try
        divisionFunc x y
    with
        | Failure msg -> printfn $"%s{msg}"; 0


let result3 = tryDivisionFunc 100 0
let result4 = tryDivisionFunc 100 4
printfn $"%A{result3}"
printfn $"%A{result4}"


(* Example 5
The invalidArg function generates an argument exception.
*)

let days = [| "Sunday"; "Monday"; "Tuesday"; "Wednesday"; "Thursday"; "Friday"; "Saturday" |]
let findDay day  =
    if day > 7 || day < 1
        then invalidArg "day" $"You have entered %d{day}."
    days[day - 1]

printfn $"%s{findDay 1}"
printfn $"%s{findDay 5}"
printfn $"%s{findDay 9}"
