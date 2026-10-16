(* F# - Operators *)

(* Arithmetic Operators
+    Adds tow operand
-    Subtracts second operand from the first
/    Divides numerator by de-nominator
%    Modus Operator and remainder of after an integer division
**   Exponentiation Operator, raises an operand to the power of another
*)

(* Examples *)

let mutable a: int32 = 21
let mutable b: int32 = 10

let mutable c = a + b
printfn $"Line 1 - Value of c is %d{c}"

c <- a - b
printfn $"Line 1 - Value of c is %d{c}"

c <- a * b
printfn $"Line 1 - Value of c is %d{c}"

c <- a / b
printfn $"Line 1 - Value of c is %d{c}"

c <- a % b
printfn $"Line 1 - Value of c is %d{c}"


(* comparison operators

Operator	Description	                                                Example
=	        Checks if the values of two operands are equal or not,      (A == B) is not true.
            if yes then condition becomes true.

*)


a <- 21
b <- 10


if a = b then
    printfn "Line 1 - a is equal to b"
else
    printfn "Line 1 - a is not equal to b"


if a < b then
    printfn "Line 2 - a is less than b"
else
    printfn "Line 2 - a is not less than b"


if a > b then
    printfn "Line 3 - a is greater than b"
else
    printfn  "Line 3 - a is not greater than b"


(* Let's change value of a and b *)
a <- 5
b <- 20


if a <= b then
   printfn "Line 4 - a is either less than or equal to b"
else
   printfn "Line4 - a is a is greater than b"



(* Boolean Operators
Operator        Description
&&  -     Called Boolean AND operator. If both the operands are non-zero, then condition becomes true.Called Boolean AND operator. If both the operands are non-zero, then condition becomes true.
||  -     Called Boolean OR Operator. If any of the two operands is non-zero, then condition becomes true.
not -     Called Boolean NOT Operator. Use to reverses the logical state of its operand. If a condition is
          true then Logical NOT operator will make false.
*)


(* Examples *)

let mutable a1 : bool = true;
let mutable b1 : bool = true;


if a1 && b1 then
    printfn "Line 1 - Condition is true"
else
    printfn "Line 1 - Condition is not true"


if a1 || b1 then
    printfn "Line 2 - Condition is true"
else
    printfn "Line 2 - Condition is not true"


(* let's change the value of a *)

a1 <- false

if a1 && b1 then
    printfn "Line 3 - Condition is true"
else
    printfn "Line 3 - Condition is not true"

if a1 || b1 then
    printfn "Line 4 - Condition is true"
else
    printfn "Line 4 - Condition is not true"


(* Bitwise Operators *)

let a2 : int32 = 60 // 60 = 0011 1100
let b2 : int32 = 13 // 13 = 0000 1101
let mutable c2 : int32 = 0



c2 <- a2 &&& b2 // 12 = 0000 1100
printfn $"Line 1 - Value of c2 is %d{c2}"

c2 <- a2 ||| b2 // 61 = 0011 1101
printfn $"Line 2 - Value of c2 is %d{c2}"

c2 <- a2 ^^^ b2 // 49 = 0011 0001
printfn $"Line 3 - Value of c is %d{c2}"

c2 <- ~~~a2 // -61 = 1100 0011
printfn $"Line 4 - Value of c2 is %d{c2}"

c2 <- a2 <<< 2 // 240 = 1111 0000
printfn $"Line 5 - Value of c2 is %d{c2}"

c2 <- a2 >>> 2 // 15 = 0000 1111
printfn $"Line 6 - Value of c2 is %d{c2}"
