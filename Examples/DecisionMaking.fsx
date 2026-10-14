(* F# - Decision-Making *)

(* if/then statement

 Syntax

simple if
if expr then
   expr

*)

let mutable a : int32 = 10
let mutable b : int32 = 20


if a < 20 then
    printfn "a is less than 20\n"

printfn $"Value of a is: %d{a}"


(* if/else statement

Syntax

if expr then
   expr
else
   expr

*)

a <- 100

if a < 20 then
   printfn "a is less than 20\n"
else
   printfn "a is not less than 20\n"

printfn $"Value of a is: %d{a}"


(* if/elif/else statement

Syntax

if expr then
   expr
elif expr then
   expr
elif expr then
   expr
...
else
   expr

*)

(* Example *)

a <- 100

(* check the boolean condition using if statement *)

if (a = 10) then
   printfn "Value of a is 10\n"
elif (a = 20) then
   printfn " Value of a is 20\n"
elif (a = 30) then
   printfn " Value of a is 30\n"
else
   printfn " None of the values are matching\n"

printfn $"Value of a is: %d{a}"


(* Nested if Statement

Syntax

if expr then
   expr
   if expr then
      expr
   else
      expr
else
   expr
*)


(* Example *)

a <- 100
b <- 200

(* check the boolean condition using if statement *)

if a = 100 then
(* if condition is true then check the following *)

    if b = 200 then
        printfn "Value of a is 100 and b is 200\n"

printfn $"Exact value of a is: %d{a}"
printfn $"Exact value of b is: %d{b}"
